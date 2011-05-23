using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace Web.Bootstrap.Routes
{
	public class RoleManagerRegistrar
	{
		public static void Register( RouteCollection routes )
		{
			routes.MapRoute("RolesManager",
				"roles/{action}/{id}",
				new
				{
					controller = "RoleManager",
					action = "List",
					id = UrlParameter.Optional
				});
		}
	}
}