using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Core.Services
{
	public class WebApplicationFileService
	{
		public virtual FileInfo RetrieveFromPartialPath( string filePath )
		{

			// getting the file path
			var directory = HttpContext.Current.Request.PhysicalApplicationPath;
			var absoluteFilePath = Path.Combine(directory, filePath);
			var fileInfo = new FileInfo(absoluteFilePath);

			return fileInfo;
		}


		public virtual StreamReader GetReaderFor( FileInfo fileInfo )
		{
			return fileInfo.OpenText();
		}
	}
}