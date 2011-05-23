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

	public class When_WeWantToPersist_A_Permission: GivenAPersistenceSpecification<Permission>
	{
		[Test]
		public void It_ShouldBeAbleTo_Save_ThenLoad_ThenDelete_A_Function()
		{
			const string DOCUMENT_UPLOADER = "DocumentUploader";

			var permission = Specs
				.CheckProperty(p => p.Name, DOCUMENT_UPLOADER)
				.VerifyTheMappings();

			Assert.IsNotNull(permission);
			Assert.AreEqual(DOCUMENT_UPLOADER, permission.Name);

			Assert.IsNotNull(permission.Id);
			Assert.IsInstanceOf<Guid>(permission.Id);

			session.Delete(permission);
			session.Flush();
		}

		
	}
}
