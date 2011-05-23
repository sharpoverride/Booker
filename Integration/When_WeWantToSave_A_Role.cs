using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using FluentNHibernate.Testing;
using NUnit.Framework;
using Core.Domain;

namespace IntegrationTests
{
	[TestFixture]

	public class When_WeWantToSave_A_Role : GivenAPersistenceSpecification<Role>
	{
		[Test]
		public void It_ShouldBeAbleTo_Save_ThenLoad_ThenDelete_A_Role()
		{
			const string ADMINISTRATOR_ROLE = "Administrator";
			const string ADMINISTRATOR_DESCRIPTION = "Administrative role";

			var role = Specs
				.CheckProperty(p => p.Name, ADMINISTRATOR_ROLE)
				.CheckProperty(p => p.Description, ADMINISTRATOR_DESCRIPTION)
				.CheckList(p => p.Functions, new List<Permission>
				{ new Permission{ Name="TestFunction"}
				})
				.VerifyTheMappings();

			Assert.IsNotNull(role);
			Assert.IsInstanceOf<Guid>(role.Id);
			Assert.AreNotEqual(role.Id, Guid.Empty);

			Assert.AreEqual(ADMINISTRATOR_ROLE, role.Name);
			Assert.AreEqual(ADMINISTRATOR_DESCRIPTION, role.Description);

			Assert.IsNotNull(role.Functions);
			Assert.AreEqual(1, role.Functions.Count);

			session.Delete(role);
			session.Flush();

		}
	}
}
