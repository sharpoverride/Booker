using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using System.Web.Mvc;
using Web.Controllers;
using Core.Persistence;
using Core.Domain;
using Web.Models.UserManager;

namespace UnitTests.Controllers
{
	[TestFixture]
	public class UserManagerTests
	{
		[Test]
		public void GET_List_BringsAllUsers()
		{
			var empMan = new UserManagerController();

			var queryUsers = MockRepository.GenerateMock<IQueryService<User>>();

			queryUsers.Expect(c => c.Query()).Return(new User[] { }.AsQueryable());

			var listModel = MockRepository.GenerateMock<UserManagerListModel>(queryUsers);
			empMan.ListModel = listModel;

			ViewResult result = empMan.List();

			Assert.IsNotNull(result.ViewData.Model);

			Assert.AreEqual(listModel, result.ViewData.Model);

		}

		[Test]
		public void GET_Create_Returns_CreatePage()
		{
			var empMan = new UserManagerController();

			empMan.CreateModel = new UserManagerCreateModel();
			ViewResult result = empMan.Create();

			Assert.IsNotNull(result);

			Assert.AreEqual(empMan.CreateModel, result.ViewData.Model);
		}

		[Test]
		public void GET_Edit_Returns_EditPage()
		{
			Guid id = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7918}");

			var empMan = new UserManagerController();

			empMan.EditModel = MockRepository.GenerateMock<UserManagerEditModel>(
				MockRepository.GenerateStub<IQueryService<User>>(),
				MockRepository.GenerateStub<IQueryService<Role>>()
				);

			empMan.EditModel.Expect(call => call.Load(id));

			var result = empMan.Edit(id);

			empMan.EditModel.VerifyAllExpectations();

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<ViewResult>(result);

			var vr = result as ViewResult;

			Assert.IsNotNull(vr.ViewData.Model);
			Assert.AreEqual(empMan.EditModel, vr.ViewData.Model);

		}

		[Test]
		public void POST_Assign_ReturnsEmptyResult()
		{

			Guid UserId = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7918}");
			Guid roleId = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7917}");
			var empMan = new UserManagerController();

			empMan.AssignModel = MockRepository.GenerateMock<UserManagerAssignModel>(

				MockRepository.GenerateStub<IQueryService<Role>>(),
				MockRepository.GenerateStub<IQueryService<User>>(),
				MockRepository.GenerateStub<ISaveOrUpdateCommand<User>>()

				);

			empMan.AssignModel.Expect(call => call.LinkUserToRole(UserId, roleId));

			var result = empMan.Assign(UserId, roleId);

			empMan.AssignModel.VerifyAllExpectations();

			Assert.IsNotNull(result);
		}

		[Test]
		public void POST_UnAssign_ReturnsEmptyResult()
		{

			Guid UserId = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7918}");
			Guid roleId = Guid.Parse("{0C21D6E8-01D0-4E59-8663-53856EEC7917}");
			var empMan = new UserManagerController();

			empMan.UnAssignModel = MockRepository.GenerateMock<UserManagerUnAssignModel>(
				MockRepository.GenerateStub<IQueryService<Role>>(),
				MockRepository.GenerateStub<IQueryService<User>>(),
				MockRepository.GenerateStub<ISaveOrUpdateCommand<User>>());

			empMan.UnAssignModel.Expect(call => call.RemoveRole(UserId, roleId));

			EmptyResult result = empMan.UnAssign(UserId, roleId);

			empMan.UnAssignModel.VerifyAllExpectations();

			Assert.IsNotNull(result);

		}
	}
}
