using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Bootstrap.Container;
using Web.Bootstrap.Routes;
using Persistence;
using Microsoft.Practices.Unity;

namespace Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication, IUnityContainerAccessor
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}


		protected void Application_Start()
		{
			InitializeContainer();


			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RoutesRegistrar.Register();
		}



		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			container.Resolve<INHibernateUnitOfWork>().Initialize();
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			container.Resolve<INHibernateUnitOfWork>().Close();
		}

		private static UnityContainer container;



		IUnityContainer IUnityContainerAccessor.Container
		{
			get
			{
				return container;
			}
		}

		/// <summary>
		/// Instantiate the container and add all Controllers that derive from 
		/// UnityController to the container.  Also associate the Controller 
		/// with the UnityContainer ControllerFactory.
		/// </summary>
		protected virtual void InitializeContainer()
		{
			if (container == null)
			{
				container = new UnityContainer();

				ContainerRegistrar.Register(container);

				ControllerBuilder.Current.SetControllerFactory(typeof(UnityControllerFactory));
			}
		}
	}
}