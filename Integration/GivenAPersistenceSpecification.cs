using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistence;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using Persistence;
using NUnit.Framework;
using Persistence.Conventions;
using FluentNHibernate.Cfg;
using System.IO;
using System.Data.SqlServerCe;
using NHibernate.Tool.hbm2ddl;

namespace IntegrationTests
{
	public class GivenAPersistenceSpecification<ENTITY>
		where ENTITY : DomainEntity
	{
		protected NHibernateSessionFactory _sessionFactory;
		protected ISession session;


		private static string _databaseFilename = "database.sdf";
		private static string _connectionstring = string.Format(@"DataSource=""{0}"";", _databaseFilename);
		
		[TestFixtureSetUp]
		public void BeforeAll()
		{
			_sessionFactory = new NHibernateSessionFactory(new DomainEntityMappingConvention(), ConnectWithSqlCe);
			session = _sessionFactory.CreateSession();
		}

		private static void ConnectWithSqlCe(FluentConfiguration config)
		{
			if (!File.Exists(_databaseFilename))
			{
				var engine = new SqlCeEngine(_connectionstring);

				engine.CreateDatabase();
			}

			config.Database(
				FluentNHibernate.Cfg.Db.MsSqlCeConfiguration.
				Standard.
				ConnectionString(c => c.Is(_connectionstring)
			).ShowSql());

			config.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true));
		}

		public PersistenceSpecification<ENTITY> Specs
		{
			get
			{
				return BuildPersistenceSpec();
			}
		}

		public virtual PersistenceSpecification<ENTITY> BuildPersistenceSpec()
		{
			return new PersistenceSpecification<ENTITY>(session);
		}
	}
}
