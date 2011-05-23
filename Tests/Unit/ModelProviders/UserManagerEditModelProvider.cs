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
	public class UserManagerEditModelProvider
	{
		[Test]
		public void UserEditModel_Loads_UserById_AndRoles()
		{
			Guid id = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7918}");
			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			var queryUsers = MockRepository.GenerateMock<IQueryService<User>>();

			var emp = MockRepository.GenerateMock<User>();

			var roles = new Role[] { MockRepository.GenerateMock<Role>(),
			 MockRepository.GenerateMock<Role>() };

			// expectations
			emp.Expect(c=>c.Roles).Return(new Role[]{roles[0]}.ToList());

			roles[0].Expect(f => f.Id).Return(Guid.Parse("{B4F707B3-D020-4B1C-9874-BA4C8FD259C3}"));
			roles[1].Expect(f => f.Id).Return(Guid.Parse("{C372EFCC-CC01-4B29-8421-873A2B69BDF3}"));

			queryUsers.Expect(f => f.Load(id)).Return(emp);

			queryRoles.Expect(f => f.Query()).Return(roles.AsQueryable());


			var model = new UserManagerEditModel(queryUsers, queryRoles);

			// act

			model.Load(id);


			queryUsers.VerifyAllExpectations();
			queryRoles.VerifyAllExpectations();

			Assert.IsNotNull(model.User);
			Assert.IsNotNull(model.Roles);

			Assert.IsTrue(model.Roles[0].IsLinked);
			Assert.IsFalse(model.Roles[1].IsLinked);
		}
	}
}
