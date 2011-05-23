using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Security.Principal;
using Core.Services;

namespace Web.Bootstrap.Container
{
	public class AuthRegistrar
	{
		public static void Register( IUnityContainer container )
		{
			container.RegisterType<IAuthenticationService, FormsAuthenticationService>();
			container.RegisterType<IMembershipService, LdapMembershipService>();

			
		}
	}
}