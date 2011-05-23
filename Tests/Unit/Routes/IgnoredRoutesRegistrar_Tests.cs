using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Web.Bootstrap.Routes;
using MvcContrib.TestHelper;
using NUnit.Framework;

namespace UnitTests.Routes
{
	[TestFixture]
	public class IgnoredRoutesRegistrar_Tests
	{
		[TestFixtureSetUp]
		public static void BeforeEach( )
		{
			var routes = RouteTable.Routes;
			routes.Clear();

			IgnoredRoutesRegistrar.Register(routes);


		}
		[Test]
		public void IgoresRoute_Favicon()
		{
			"~/favicon.ico".ShouldBeIgnored();
		}

		[Test]
		public void IgnoresRoute_AssetsPath()
		{
			"~/Assets/some/path/here/to/some/file.xxx".ShouldBeIgnored();
		}

		[Test]
		public void IgnoresRoute_ForHttpWebHandlers()
		{
			"NoNameHandler.axd/followed/by/Path/To/Resource".ShouldBeIgnored();
		}
	}
}
