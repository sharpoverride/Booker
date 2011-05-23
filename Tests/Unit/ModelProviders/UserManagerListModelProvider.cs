using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;
using Rhino.Mocks;
using Web.Models.UserManager;



namespace UnitTests.ModelProviders
{
	[TestFixture]
	public class UserManagerListModelProvider
	{
		[Test]
		public void List_Holds_All_Users()
		{
			var queryUser = MockRepository.GenerateMock<IQueryService<User>>();

			queryUser.Expect(q => q.Query()).Return(new User[] {
			new User{
				
			}}.AsQueryable());

			var empList = new UserManagerListModel(queryUser);

			queryUser.VerifyAllExpectations();
			Assert.IsNotNull(empList.Users);
			Assert.AreEqual(1, empList.Users.Count);
		}
	}
}
