using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Rhino.Mocks;
using NUnit.Framework;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;
using Web.Models.UserManager;



namespace UnitTests.ModelProviders
{
	[TestFixture]
	public class UserManagerUnAssignModelProvider
	{
		[Test]
		public void RemoveRole_Updates_User_AndRemoves_Role()
		{

			var UserGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");

			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			var queryUser = MockRepository.GenerateMock<IQueryService<User>>();
			var updateUser = MockRepository.GenerateMock<ISaveOrUpdateCommand<User>>();

			var fakeRole = MockRepository.GenerateMock<Role>();
			var fakeUser = MockRepository.GenerateMock<User>();


			var assignModel = new UserManagerUnAssignModel(
				queryRoles,
				queryUser,
				updateUser
				);

			queryRoles.Expect(q => q.Load(roleGuid)).Return(fakeRole);
			queryUser.Expect(q => q.Load(UserGuid)).Return(fakeUser);


			fakeUser.Expect(f => f.RemoveRole(fakeRole));

			updateUser.Expect(s => s.Execute(fakeUser));
			// act

			assignModel.RemoveRole(UserGuid, roleGuid);

			// assert
			fakeRole.VerifyAllExpectations();
			fakeUser.VerifyAllExpectations();

			queryRoles.VerifyAllExpectations();
			queryUser.VerifyAllExpectations();

			updateUser.VerifyAllExpectations();
		}
	}
}
