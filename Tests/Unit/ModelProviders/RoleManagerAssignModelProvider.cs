using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Rhino.Mocks;
using NUnit.Framework;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;
using Web.Controllers;


namespace UnitTests.ModelProviders
{
	[TestFixture]
	public class RoleManagerAssignModelProvider
	{
		[Test]
		public void It_LinksA_Function_ToA_Role()
		{
			var permissionGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");

			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			var queryFunction = MockRepository.GenerateMock<IQueryService<Permission>>();
			var updateRole = MockRepository.GenerateMock<ISaveOrUpdateCommand<Role>>();
			
			var fakeRole = MockRepository.GenerateMock<Role>();
			var fakeFunction = MockRepository.GenerateMock<Permission>();


			var assignModel = new RoleManagerAssignModel(
				queryRoles,
				queryFunction,
				updateRole
				);

			queryRoles.Expect(q => q.Load(roleGuid)).Return(fakeRole);
			queryFunction.Expect(q => q.Load(permissionGuid)).Return(fakeFunction);

			fakeRole.Expect(f => f.AddFunction(Arg<Permission>.Matches(p => p.Id == permissionGuid)));

			fakeFunction.Expect(f => f.Id).Return(permissionGuid);

			updateRole.Expect(s => s.Execute(fakeRole));
			// act

			assignModel.LinkFunctionToRole(permissionGuid, roleGuid);

			// assert
			updateRole.VerifyAllExpectations();
			fakeRole.VerifyAllExpectations();
			fakeFunction.VerifyAllExpectations();

			queryRoles.VerifyAllExpectations();
			queryFunction.VerifyAllExpectations();

		}
	}
}
