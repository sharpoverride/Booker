using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Core.Security;
using Web.Bootstrap.Container;

namespace Web.Security
{
	public class RequiresAttribute: FilterAttribute, IAuthorizationFilter
	{
		
		public virtual void OnAuthorization( AuthorizationContext context )
		{
			var unityAccessor = context.HttpContext.ApplicationInstance as IUnityContainerAccessor;

			var functionRightsService = unityAccessor.Container.Resolve<IFunctionRightsService>(new ParameterOverride("httpContext", context.HttpContext));

			if (functionRightsService == null)
			{
				throw new InvalidOperationException("IoC could not find FunctionRightsService implementation ");

			}

			var requiresFunctionRights = SplitString(FunctionRights);

			bool letItPass = context.HttpContext.User.Identity.IsAuthenticated;

			if (requiresFunctionRights.Length > 0)
				letItPass = letItPass && requiresFunctionRights.Any(right => functionRightsService.HasFunctionAssigned(right));

			if (!letItPass)
				context.Result = new HttpUnauthorizedResult();
				
		}

		private string[] SplitString( string @string )
		{
			if (string.IsNullOrWhiteSpace(@string))
				return new string[] { };

			var splits = @string.Split(',');

			return splits;
		}

		public string FunctionRights
		{
			get;
			set;
		}
	}
}
