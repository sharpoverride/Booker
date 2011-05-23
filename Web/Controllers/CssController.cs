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
	public class CssController 	:Controller{
		#region Constants
		// These two constants specify the group names used 
		// in the configuration for compacting our javascripts

		#endregion

		#region Fields

		private readonly ICssProviderService cssProviderService;

		#endregion

		#region Ctors


		public CssController( ICssProviderService cssProviderServicer )
		{
			this.cssProviderService = cssProviderServicer;
		}

		#endregion

		#region Actions
		/// <summary>
		/// Returns the stylesheet
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public ActionResult Index( string group )
		{
			// and reading everything that follows the version

			var lContent = cssProviderService.GetCss(group);

			return new ContentResult
			{
				Content = lContent,
				ContentEncoding = Encoding.UTF8,
				ContentType = "text/css"
			};
		}

		#endregion Actions

		#region Methods


		#endregion Methods
	}
}
