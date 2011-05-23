using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using NUnit.Framework;
using NUnit.Framework.Constraints;

using Web.Controllers;
using Core.Services;
using FakeItEasy;

namespace UnitTests.Controllers
{
	[TestFixture]
	public class JavascriptControllerTests
	{
		const string FRAMEWORK_SCRIPT = "framework";

		IJavaScriptProviderService fakeProviderService;

		[Test]
		public void Index_Get_Returns()
		{
			var controller = GetController();

			var result = controller.Index(FRAMEWORK_SCRIPT);

			Assert.IsInstanceOf<ContentResult>(result);

			var contentResult = (ContentResult)result;

			Assert.AreEqual(contentResult.Content, "abc");
			Assert.AreEqual(contentResult.ContentEncoding, Encoding.UTF8);

			Assert.AreEqual(contentResult.ContentType, "text/javascript");
		}

		[TestFixtureSetUp]
		public void BeforeAll()
		{
			
			fakeProviderService = A.Fake<IJavaScriptProviderService>();

			A.CallTo(()=> fakeProviderService.GetScript(A<string>.That.IsEqualTo(FRAMEWORK_SCRIPT))).Returns("abc");

		}
		private JavascriptController GetController()
		{
			var controller = new JavascriptController(fakeProviderService);

			return controller;

		}
	}
}
