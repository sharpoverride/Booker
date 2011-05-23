using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Web.Bootstrap.Container;

namespace Web.Bootstrap.Container
{
	public class ContainerRegistrar 
	{

		public static void Register(IUnityContainer container)
		{
			EnterpriseLibraryRegistrar.RegisterWith(container);

			MinificationRegistrar.RegisterWith(container);

			AuthRegistrar.Register(container);

			PersistenceRegistrar.Register(container);

			SecurityRegistrar.Register(container);
		}
	}
}