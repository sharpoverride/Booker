using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Core.Services;

namespace Web.Controllers
{
	/// <summary>
	/// This controller is responsible for compacting scripts and versioning
	/// </summary>
	public class JavascriptController:Controller
	{
		#region Constants


		#endregion

		#region Fields

		private readonly IJavaScriptProviderService scriptProviderService;

		#endregion

		#region Ctors


		public JavascriptController(IJavaScriptProviderService scriptProviderService)
		{
			this.scriptProviderService = scriptProviderService;
		}

		#endregion

		#region Actions
		/// <summary>
		/// Returns the script
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public ActionResult Index(string group)
		{
			// and reading everything that follows the version

			var content = scriptProviderService.GetScript(group);

			return new ContentResult
			{
				Content = content,
				ContentEncoding = Encoding.UTF8,
				ContentType = "text/javascript"
			};
		}

		#endregion Actions

		#region Methods


		#endregion Methods
	}
}
