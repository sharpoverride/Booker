using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using FakeItEasy;
using Core.Services;
using Web.Controllers;

namespace UnitTests.Controllers
{
	[TestFixture]
	public class CssControllerTests
	{
		ICssProviderService fakeCssProviderService;

		const string SITE = "site";


		[TestFixtureSetUp]
		public void BeforeAll()
		{

			fakeCssProviderService = A.Fake<ICssProviderService>();

			A.CallTo(()=> fakeCssProviderService.GetCss( A<string>.That.IsEqualTo( SITE) )).Returns("abc");

		}

		[Test]
		public void Index_Get_ReturnsContentTypeResult()
		{
			var controller = new CssController(fakeCssProviderService);

			var result = controller.Index(SITE);


			Assert.IsInstanceOf<ContentResult>(result);

			var contentResult = (ContentResult)result;

			Assert.AreEqual("abc", contentResult.Content);
			Assert.AreEqual(Encoding.UTF8, contentResult.ContentEncoding);
			Assert.AreEqual("text/css", contentResult.ContentType);

		}
	}
}
