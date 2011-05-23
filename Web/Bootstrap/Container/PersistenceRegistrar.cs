using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using Persistence.Commands;
using Persistence.Queries;
using Persistence.Conventions;
using FluentNHibernate.Automapping;
using Core.Persistence;
using Persistence;


namespace Web.Bootstrap.Container
{
	public class PersistenceRegistrar
	{
		public static void Register( IUnityContainer container )
		{
			container.RegisterType<IAutomappingConfiguration, DomainEntityMappingConvention>();

			container.RegisterType( typeof(ISaveOrUpdateCommand<>),typeof( NHibernateSaveOrUpdateCommand<>));

			container.RegisterType(typeof(IDeleteCommand<>), typeof(NHibernateDeleteCommand<>));

			container.RegisterType<INHibernateUnitOfWork, NHibernateUnitOfWork>(new PerThreadLifetimeManager());
			
			container.RegisterInstance<INHibernateSessionFactory>(new NHibernateSessionFactory(container.Resolve<IAutomappingConfiguration>()),
				new ContainerControlledLifetimeManager());
			
			container.RegisterType(typeof(IQueryService<>), typeof(NHibernateQueryService<>));
		}
	}
}