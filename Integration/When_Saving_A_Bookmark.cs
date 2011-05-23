using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Domain;
using IntegrationTests;
using FluentNHibernate.Testing;
using NUnit.Framework.Constraints;

namespace Integration
{
	[TestFixture]
	public class When_Saving_A_Bookmark : GivenAPersistenceSpecification<Bookmark>
	{
		[Test]
		public void It_Should_Support_Creation_And_Deletion()
		{
			var bookmark = Specs.
				CheckProperty(p=>p.Notes, @"Some values 

Written here

For Convenience").
				CheckProperty(p=>p.Url, "http://resoursce/identifier").
				
				VerifyTheMappings();

			Assert.That(bookmark, Is.Not.Null);
			Assert.That(bookmark.Notes, Is.StringMatching("Written here"));
			Assert.That(bookmark.Url, Is.StringStarting("http://"));

			session.Delete(bookmark);
			session.Flush();

	
		}
	}
}
