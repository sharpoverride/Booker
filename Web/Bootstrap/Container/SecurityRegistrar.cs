using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Core.Security;
using Persistence.Security;

namespace Web.Bootstrap.Container
{
	public class SecurityRegistrar
	{
		public static void Register( IUnityContainer container )
		{
			container.RegisterType<IFunctionRightsService, FunctionRightsService>();
		}
	}
}