using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration.Unity;

namespace Web.Bootstrap.Container
{
	public class EnterpriseLibraryRegistrar
	{
		public static void RegisterWith( IUnityContainer container )
		{
			container.AddNewExtension<EnterpriseLibraryCoreExtension>();
			//container.AddNewExtension<CachingBlockExtension>();
		}
	}
}