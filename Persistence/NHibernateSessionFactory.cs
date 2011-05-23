using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cache;
using FluentNHibernate.Automapping;
using Core.Domain;
using Persistence.Conventions;
using System.IO;
using NHibernate.Tool.hbm2ddl;
using Domain;


namespace Persistence
{
	public class NHibernateSessionFactory : INHibernateSessionFactory
	{
		private readonly ISessionFactory sessionFactory;
		private readonly IAutomappingConfiguration mappingConfiguration;

		private readonly Action<FluentConfiguration> databaseCreation;

		public NHibernateSessionFactory(IAutomappingConfiguration mappingConfiguration)
			: this(mappingConfiguration, ConnectWithSqlServer2008)
		{

		}

		public NHibernateSessionFactory(IAutomappingConfiguration mappingConfiguration, Action<FluentConfiguration> databaseCreation)
		{
			this.mappingConfiguration = mappingConfiguration;
			this.databaseCreation = databaseCreation;
			sessionFactory = CreateSessionFactory();
		}

		private static void ConnectWithSqlServer2008(FluentConfiguration config)
		{
			config.Database(

				MsSqlConfiguration.MsSql2008
					.ConnectionString(c => c.FromConnectionStringWithKey("DbConnection"))
					.ShowSql()
					).Cache(c =>
					c.UseQueryCache().ProviderClass<HashtableCacheProvider>());

		}




		public ISession CreateSession()
		{
			return sessionFactory.OpenSession();
		}

		private ISessionFactory CreateSessionFactory()
		{

			var config = Fluently.Configure();

			databaseCreation(config);

			config.Mappings(m =>

				m.AutoMappings.Add(
					AutoMap
					.AssemblyOf<Role>(mappingConfiguration)
					.AddEntityAssembly(typeof(Bookmark).Assembly)
					.IgnoreBase<DomainEntity>()
					.Conventions.Add<TableNamingConvention>()
					.Conventions.Add<ErmForeignKeyConvention>()
					.Conventions.Add<ManyToManyConvention>()
					.Override<Role>(mr =>
						{
							mr.HasManyToMany(roles => roles.Functions).Cascade.All();
							mr.HasManyToMany(roles => roles.Employees).Cascade.All();
						}

					)
					.Override<User>(e =>
					e.HasManyToMany(employee => employee.Roles).Cascade.All())
				)
			);

			var factory = config.BuildSessionFactory();
			return factory;

		}


	}
}
