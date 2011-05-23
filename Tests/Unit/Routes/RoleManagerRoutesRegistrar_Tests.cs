using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Web.Controllers;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace UnitTests.Routes
{
	[TestFixture]
	public class RoleManagerRoutesRegistrar_Tests
	{
		[TestFixtureSetUp]
		public void BeforeEach()
		{
			var routes = RouteTable.Routes;
			routes.Clear();
			// Registers default routes.
			Web.Bootstrap.Routes.RoleManagerRegistrar.Register(routes);

		}



		[Test]
		public void MapsRoute_Roles()
		{
			"~/roles".ShouldMapTo<RoleManagerController>(action => action.List());
		}
	}
}
