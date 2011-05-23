using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Security;
using System.Web.Mvc;

using FakeItEasy;
using System.Web;
using Core.Security;
using MvcContrib.TestHelper.Fakes;
using Web.Bootstrap.Container;
using Microsoft.Practices.Unity;

namespace Tests.Unit.Security
{
	[TestFixture]
	public class RequiresAttributeTests
	{
		const string DOCUMENT_UPLOADER = "DocumentUploader";

		[Test]
		public void OnAuthorization_FailsWhen_UserSignedIn_And_DoesNotHaveFunction_Assigned()
		{
			// arrange
			var authContext = FakeAuthorizationContext_UserSignedIn_DOCUMENTUPLOADER_NotAssigned();

			var requiresAttribute = new RequiresAttribute();

			requiresAttribute.FunctionRights = DOCUMENT_UPLOADER;// simulating [Requires(FunctionRights= "DocumentUploader")] ActionResult OnSomeMethod(){ }

			// act : NOTE that Asp.NET MVC will automatically call this employee
			requiresAttribute.OnAuthorization(authContext);

			// assert
			Assert.IsInstanceOf<HttpUnauthorizedResult>(authContext.Result);

		}

		[Test]
		public void OnAuthorization_FailsWhen_UserNotSignedIn()
		{

			var authContext = FakeAuthorizationContext_UserNotSignedIn();

			var requiresAttribute = new RequiresAttribute();

			requiresAttribute.FunctionRights = DOCUMENT_UPLOADER;// simulating [Requires(FunctionRights= "DocumentUploader")] ActionResult OnSomeMethod(){ }

			// act
			requiresAttribute.OnAuthorization(authContext);

			// assert
			Assert.IsInstanceOf<HttpUnauthorizedResult>(authContext.Result);

		}
		[Test]
		public void OnAuthorization_Works_When_UserSignedIn_And_HasFunctionRight()
		{
			var authContext = FakeAuthorizationContext_UserSignedIn_And_DOCUMENTUPLOADER_FunctionRightAssigned();

			var requiresAttribute = new RequiresAttribute();

			requiresAttribute.FunctionRights = DOCUMENT_UPLOADER;// simulating [Requires(FunctionRights= "DocumentUploader")] ActionResult OnSomeMethod(){ }
			// act
			requiresAttribute.OnAuthorization(authContext);

			// assert
			Assert.IsNull(authContext.Result);

		}

		private static AuthorizationContext FakeAuthorizationContext_UserSignedIn_And_DOCUMENTUPLOADER_FunctionRightAssigned()
		{
			return FakeAuthorizationContext(
				(httpContext, functionRightsService) =>
				{
					var principal = new FakePrincipal(new FakeIdentity("mihai.lazar"), new string[] { });
					httpContext.User = principal;

					A.CallTo(() => functionRightsService.HasFunctionAssigned(A<string>.That.IsEqualTo(DOCUMENT_UPLOADER))).Returns(true);
				}
				);
		}

		private static AuthorizationContext FakeAuthorizationContext_UserNotSignedIn()
		{
			Action<HttpContextBase, IFunctionRightsService> assign_unsigned_user = (httpContext, functionRightsService) =>
				{
					var principal = new FakePrincipal(new FakeIdentity(null), new string[] { });
					httpContext.User = principal;
				};

			return FakeAuthorizationContext(
				assign_unsigned_user
								);
		}

		private static AuthorizationContext FakeAuthorizationContext_UserSignedIn_DOCUMENTUPLOADER_NotAssigned()
		{

			Action<HttpContextBase, IFunctionRightsService> assign_a_signed_in_user = (httpContext, functionRightsService) =>
				{

					var principal = new FakePrincipal(new FakeIdentity("mihai.lazar"), new string[] { });
					httpContext.User = principal;

				};


			return FakeAuthorizationContext(
					assign_a_signed_in_user
				);
		}

		private static AuthorizationContext FakeAuthorizationContext(Action<HttpContextBase, IFunctionRightsService> establishWorkingParameters)
		{

			var authContext = A.Fake<AuthorizationContext>();

			var httpContext = A.Fake<HttpContextBase>();

			var stubAppInstance = A.Fake<StubHttpApplication>();

			var container = A.Fake<IUnityContainer>();

			var functionRightsService = A.Fake<IFunctionRightsService>();


			stubAppInstance.Container = container;

			httpContext.ApplicationInstance = stubAppInstance;

			A.CallTo(() => functionRightsService.HasFunctionAssigned(A<string>.That.IsEqualTo(DOCUMENT_UPLOADER))).Returns(false);

			A.CallTo(() => container.Resolve(typeof(IFunctionRightsService), null)).WithAnyArguments().Returns(functionRightsService);

			establishWorkingParameters(httpContext,  functionRightsService);

			authContext.HttpContext = httpContext;
			return authContext;
		}

		/// <summary>
		/// This class is only used as a stub for the real application.
		/// It is used by the HttpContext as the application instance
		/// </summary>
		public class StubHttpApplication : System.Web.HttpApplication, IUnityContainerAccessor
		{

			public virtual Microsoft.Practices.Unity.IUnityContainer Container
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{

				}
			}
		}

	}
}
