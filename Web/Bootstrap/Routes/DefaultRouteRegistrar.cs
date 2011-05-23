using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Bootstrap.Routes
{
	public class DefaultRouteRegistrar
	{
		public static void Register( RouteCollection routes )
		{
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new
				{
					controller = "Home",
					action = "Index",
					id = UrlParameter.Optional
				} // Parameter defaults
			);
		}
	}
}