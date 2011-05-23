using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Bootstrap.Routes
{
	public static class RoutesRegistrar
	{
		public static void Register()
		{
			var routes = RouteTable.Routes;

			IgnoredRoutesRegistrar.Register(routes);

			// immediately after this comment add any routes registrar call
			ScriptsRoutesRegistrar.Register(routes);
			AssetRoutesRegistrar.Register(routes);

			RoleManagerRegistrar.Register(routes);

			// this is allways the last to be registered
			DefaultRouteRegistrar.Register(routes);
		}
	}
}