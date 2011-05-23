using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Rhino.Mocks;
using NUnit.Framework;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;

namespace UnitTests.ModelProviders
{
	[TestFixture]
	public class RoleManagerEditOutput_Tests
	{
		[Test]
		public void Foobar()
		{
			Guid id = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7918}");
			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			var queryFunctions = MockRepository.GenerateMock<IQueryService<Permission>>();

			var role = MockRepository.GenerateMock<Role>();

			var functions = new Permission[]{
						 MockRepository.GenerateMock<Permission>(),
						 MockRepository.GenerateMock<Permission>()
			};

			var roleFunctions = new Permission[]{ functions[1] };

			functions[0].Expect(f=>f.Id).Return(Guid.Parse("{B4F707B3-D020-4B1C-9874-BA4C8FD259C3}"));
			functions[1].Expect(f=>f.Id).Return(Guid.Parse("{C372EFCC-CC01-4B29-8421-873A2B69BDF3}"));

			functions[0].Expect(f=>f.Name).Return("Slayer");
			functions[1].Expect(f=>f.Name).Return("Slave");

			role.Expect(p=>p.Id).Return(id);
			role.Expect(p=>p.Name).Return("Glorious");
			role.Expect(p=>p.Functions).Return(roleFunctions.ToList());

			queryRoles.Expect(r=>r.Load(id)).Return(role);

			queryFunctions.Expect(r => r.Query()).Return(functions.AsQueryable());

			var outputModel = new RoleManagerEditOutput(
				queryRoles, queryFunctions);

			outputModel.Load(id);

			Assert.IsNotNull(outputModel.Functions);
			Assert.IsNotNull(outputModel.Role);
			Assert.AreEqual(outputModel.Role.Name, "Glorious");
			Assert.AreEqual(outputModel.Functions.Count, functions.Count());

			Assert.AreEqual(true, outputModel.Functions[1].IsLinked);
			Assert.AreEqual(false, outputModel.Functions[0].IsLinked);

		
		}
	}
}
