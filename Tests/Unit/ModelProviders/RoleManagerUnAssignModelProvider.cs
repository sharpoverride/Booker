using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Rhino.Mocks;
using NUnit.Framework;
using Web.Controllers;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;

namespace UnitTests.ModelProviders
{
	[TestFixture]
	public class RoleManagerUnAssignModelProvider
	{
		[Test]
		public void RemoveFunctionFromRole_Updates_Role_AndRemoves_Function()
		{

			var functionGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");
			var roleMan = new RoleManagerController();

			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			var queryFunction = MockRepository.GenerateMock<IQueryService<Permission>>();
			var updateRole = MockRepository.GenerateMock<ISaveOrUpdateCommand<Role>>();

			var fakeRole = MockRepository.GenerateMock<Role>();
			var fakeFunction = MockRepository.GenerateMock<Permission>();


			var assignModel = new RoleManagerUnAssignModel(
				queryRoles,
				queryFunction,
				updateRole
				);

			queryRoles.Expect(q => q.Load(roleGuid)).Return(fakeRole);
			queryFunction.Expect(q => q.Load(functionGuid)).Return(fakeFunction);

			fakeRole.Expect(f => f.RemoveFunction(Arg<Permission>.Matches(p => p.Id == functionGuid)));

			fakeFunction.Expect(f => f.Id).Return(functionGuid);

			updateRole.Expect(s => s.Execute(fakeRole));
			// act

			assignModel.RemoveFunctionFromRole(functionGuid, roleGuid);

			// assert
			fakeRole.VerifyAllExpectations();
			fakeFunction.VerifyAllExpectations();

			queryRoles.VerifyAllExpectations();
			queryFunction.VerifyAllExpectations();

			updateRole.VerifyAllExpectations();
		}
	}
}
