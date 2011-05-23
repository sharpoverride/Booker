using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Testing;
using NUnit.Framework;
using Core.Domain;
using Persistence;
using Persistence.Queries.Employees;
using Persistence.Conventions;

using NHibernate.Linq;

namespace IntegrationTests
{
	[TestFixture]

	public class When_WeWantToPersist_AnEmployee : GivenAPersistenceSpecification<User>
	{
		[Test]
		public void It_ShouldSuccessfullyPersist_AnEmployee()
		{
			var employee = Specs
				.CheckProperty(e => e.UserName, "mihai.lazar")
				.CheckList(e => e.Roles, new List<Role> { new Role { Name = "Admnistrator", Description = "The users with this role will " } })
				.VerifyTheMappings();

			Assert.IsNotNull(employee);
			Assert.AreEqual("mihai.lazar", employee.UserName);

			Assert.IsNotNull(employee.Roles);
			Assert.AreEqual(1, employee.Roles.Count);


			session.Delete(employee);
			session.Flush();

		}

		[Test]
		public void It_ShouldQueryFor_EmployeeByName()
		{
			var employee = Specs
				.CheckProperty(e => e.UserName, "mihai.lazar")
				.CheckList(e => e.Roles, new List<Role> { new Role { Name = "Admnistrator", Description = "The users with this role will " } })
				.VerifyTheMappings();

			Assert.IsNotNull(employee);
			Assert.AreEqual("mihai.lazar", employee.UserName);

			Assert.IsNotNull(employee.Roles);
			Assert.AreEqual(1, employee.Roles.Count);


			var unitOfWork = new NHibernateUnitOfWork(_sessionFactory);
			unitOfWork.Initialize();


			var s = new Persistence.Queries.NHibernateQueryService<User>(unitOfWork);
			;
			var emplByName = new UserByUserName("mihai.lazar");

			var xx = s.Query(emplByName);
			var x = xx.ToList();
			Assert.IsNotNull(x);

			session.Delete(employee);

			session.Flush();

			unitOfWork.Close();
		}
	}
}
