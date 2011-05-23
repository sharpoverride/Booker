using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Core.Persistence;
using Core.Domain;
using Web.Models.UserManager;

namespace Tests.Unit.ModelProviders
{
	[TestFixture]
	public class UserManagerAssignModelProvider
	{
		[Test]
		public void Links_User_To_Role()
		{
			var UserGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");

			var assignModel = new UserManagerAssignModel(
				Mocks.QueryRoles,
				Mocks.QueryUser,
				Mocks.UpdateUser
				);

			Mocks.QueryRoles.Expect(q => q.Load(roleGuid)).Return(Mocks.FakeRole);
			Mocks.QueryUser.Expect(q => q.Load(UserGuid)).Return(Mocks.FakeUser);


			Mocks.FakeUser.Expect(f => f.AddRole(Mocks.FakeRole));

			Mocks.UpdateUser.Expect(s => s.Execute(Mocks.FakeUser));
			// act

			assignModel.LinkUserToRole(UserGuid, roleGuid);

			// assert
			Mocks.FakeRole.VerifyAllExpectations();
			Mocks.FakeUser.VerifyAllExpectations();

			Mocks.QueryRoles.VerifyAllExpectations();
			Mocks.QueryUser.VerifyAllExpectations();

			Mocks.UpdateUser.VerifyAllExpectations();
		}
	}
}
