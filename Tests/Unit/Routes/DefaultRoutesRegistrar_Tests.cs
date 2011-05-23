using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using MvcContrib.TestHelper;
using NUnit.Framework;
using Web.Controllers;
namespace UnitTests.Routes
{
	[TestFixture]
	public class DefaultRoutesRegistrar_Tests
	{
		[TestFixtureSetUp]
		public void BeforeEach()
		{
			var routes = RouteTable.Routes;
			routes.Clear();
			// Registers default routes.
			Web.Bootstrap.Routes.DefaultRouteRegistrar.Register(routes);

		}



		[Test]
		public void MapsRoute_HomeIndex()
		{
			"~/home/index".ShouldMapTo<HomeController>(action => action.Index());
		}

		[Test]
		public void MapsRoute_HomeAbout()
		{
			"~/home/about".ShouldMapTo<HomeController>(action => action.About());
		}

		[Test]
		public void MapsRoute_AccountLogon()
		{
			"~/account/logon".ShouldMapTo<AccountController>(action => action.LogOn());
		}

		[Test]
		public void MapsRoute_AccountLogOff()
		{
			"~/account/logoff".ShouldMapTo<AccountController>(action => action.LogOff());
		}

	}
}
