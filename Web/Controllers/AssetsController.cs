using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Core.Services;
using Web.Bootstrap;

namespace Web.Controllers
{
	/// <summary>
	/// Has the following responsabilities:
	/// - Handles all static content delivered by the website by returning the content and the correct status code
	/// - Caches files on server memory
	/// - Adds caching headers
	/// </summary>
	public class AssetsController : Controller
	{
		#region Constants
		private const string IF_NONE_MATCH_HEADER = "If-None-Match";
		private const string LAST_MODIFIED_SINCE_HEADER = "If-Modified-Since";
		private const string SHARED_FOLDER = "~/Assets/";
		#endregion

		[Dependency]
		public MimeTypeResolverService MimeTypes
		{
			get;
			set;
		}

		#region Constructor

		public AssetsController()
		{
		}

		#endregion

		#region Actions

		/// <summary>
		/// Handles the request for static files by returning the correct content and status code
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
		public ActionResult Shared(string file)
		{
			string relativePath = SHARED_FOLDER + file;
			string absolutePath = Server.MapPath(relativePath);

			// 404 (NotFound)
			if (!System.IO.File.Exists(absolutePath))
			{
				Response.StatusCode = (int)HttpStatusCode.NotFound;
				Response.SuppressContent = true;
				return new EmptyResult();
			}

			// 304 (If-None-Match), a better test of uniqueness than modified date
			string lEtag = GenerateETag(absolutePath);
			if (BrowserIsRequestingFileIdentifiedBy(lEtag))
			{
				Response.StatusCode = (int)HttpStatusCode.NotModified;
				Response.SuppressContent = true;
				return new EmptyResult();
			}

			// 304 (If-Last-Modified)
			DateTime lLastModified = new FileInfo(absolutePath).LastWriteTime;
			if (BrowserIsRequestingFileUnmodifiedSince(lLastModified))
			{
				Response.StatusCode = (int)HttpStatusCode.NotModified;
				Response.SuppressContent = true;
				return new EmptyResult();
			}

			// 200 - OK
			AddCachingHeaders(lEtag, lLastModified, AppSettings.StaticFileHttpMaxAge);

			return ReturnContent(relativePath, absolutePath);
		}

		#endregion

		#region Private Members


		private ActionResult ReturnContent(string relativePath, string absolutePath)
		{
			string contentType = MimeTypes.GetMIMEType(absolutePath);
			byte[] content = GetFileContent(relativePath, absolutePath);

			return new FileContentResult(content, contentType);
		}

		private static byte[] GetFileContent(string relativePath, string absolutePath)
		{
			Cache cache = HttpRuntime.Cache;
			byte[] fileContent;
			if (cache[relativePath] == null)
			{
				var cacheDependency = new CacheDependency(absolutePath);
				var fileInfo = new FileInfo(absolutePath);

				using (FileStream fileStream = System.IO.File.OpenRead(absolutePath))
				{
					fileContent = new byte[(int)fileInfo.Length];
					fileStream.Read(fileContent, 0, fileContent.Length);
				}

				cache.Insert(relativePath, fileContent, cacheDependency);
			}
			else
			{
				fileContent = (byte[])cache[relativePath];
			}

			return fileContent;
		}

		private bool BrowserIsRequestingFileUnmodifiedSince(DateTime lastModified)
		{
			if (Request.Headers[LAST_MODIFIED_SINCE_HEADER] == null)
			{
				return false;
			}

			// Header values may have additional attributes separated by semi-colons
			string ifModifiedSince = Request.Headers[LAST_MODIFIED_SINCE_HEADER];
			if (ifModifiedSince.IndexOf(";") > -1)
			{
				ifModifiedSince = ifModifiedSince.Split(';').First();
			}

			// Get the dates for comparison; truncate milliseconds in date if needed
			DateTime sinceDate = Convert.ToDateTime(ifModifiedSince).ToUniversalTime();
			DateTime fileDate = lastModified.ToUniversalTime();
			if (sinceDate.Millisecond.Equals(0))
			{
				fileDate = new DateTime(fileDate.Year,
					fileDate.Month,
					fileDate.Day,
					fileDate.Hour,
					fileDate.Minute,
					fileDate.Second,
					0);
			}

			return fileDate.CompareTo(sinceDate) <= 0;
		}

		private bool BrowserIsRequestingFileIdentifiedBy(string etag)
		{
			if (Request.Headers[IF_NONE_MATCH_HEADER] == null)
			{
				return false;
			}

			string lIfNoneMatch = Request.Headers[IF_NONE_MATCH_HEADER];

			return lIfNoneMatch.Equals(etag, StringComparison.InvariantCultureIgnoreCase);
		}

		private void AddCachingHeaders(string etag, DateTime lastModified, TimeSpan maxAge)
		{
			// Cacheability must be set to public for SetETag to work; you could also
			// add the ETag header yourself with AppendHeader or AddHeader methods
			Response.Cache.SetCacheability(HttpCacheability.Public);
			Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
			Response.Cache.SetExpires(DateTime.UtcNow.Add(maxAge));
			Response.Cache.SetMaxAge(maxAge);
			Response.Cache.SetLastModified(lastModified);
			Response.Cache.SetETag(etag);
		}

		/// <summary>
		/// Generates an ETag for the file by making a MD5 hash from the file content
		/// </summary>
		private static string GenerateETag(string absolutePath)
		{
			var stringBuilder = new StringBuilder();

			using (var lFileStream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read))
			{
				using (var lBinaryReader = new BinaryReader(lFileStream))
				{
					var cryptService = new MD5CryptoServiceProvider();
					byte[] hash = cryptService.ComputeHash(lBinaryReader.BaseStream);
					foreach (byte hex in hash)
					{
						stringBuilder.Append(hex.ToString("x2"));
					}

					return stringBuilder.ToString();
				}
			}
		}

		#endregion
	}
}

