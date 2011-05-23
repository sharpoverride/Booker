using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using MvcContrib.TestHelper;
using Rhino.Mocks;
using System.Web.Mvc;
using Web.Controllers;
using Core.Persistence;
using Core.Domain;
using Web.Models.RoleManager;

namespace UnitTests.Controllers
{
	[TestFixture]
	public class RoleManagerTests
	{
		[Test]
		public void GET_List_Has_ListOutputModel_RequestsRoles()
		{
			// Arrange

			var roleManager = new RoleManagerController();

			var builder = new TestControllerBuilder();

			builder.InitializeController(roleManager);

			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();

			queryRoles.Expect(q => q.Query()).Return(new Role[] { }.AsQueryable());


			roleManager.ListOutputModel = new RoleManagerListOutputModel(
				queryRoles				);

			// act
			var result = roleManager.List();

			// Assert
			Assert.IsInstanceOf< ViewResult>(result);

			var viewResult = result as ViewResult;

			var model = viewResult.ViewData.Model;

			Assert.IsInstanceOf<RoleManagerListOutputModel>(model);

			queryRoles.VerifyAllExpectations();

		}

		[Test]
		public void GET_List_ListOutputModel_RetrievesInfoString_From_TempData_InfoKey()
		{
			const string INFORMATION = "Information";
			const string INFO_KEY = "info";
			// Arrange

			var roleManager = new RoleManagerController();

			var builder = new TestControllerBuilder();

			builder.TempDataDictionary.Add(INFO_KEY, INFORMATION);

			builder.InitializeController(roleManager);

			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();

			queryRoles.Expect(q => q.Query()).Return(new Role[] { }.AsQueryable());

			roleManager.ListOutputModel = new RoleManagerListOutputModel(
				queryRoles				);

			// act
			var result = roleManager.List();

			// Assert
			Assert.IsInstanceOf<ViewResult>(result);

			var viewResult = result as ViewResult;

			var model = viewResult.ViewData.Model;

			Assert.IsInstanceOf<RoleManagerListOutputModel>(model);

			var viewModel = model as RoleManagerListOutputModel;

			Assert.AreEqual(viewModel.InfoMessage, INFORMATION);


			queryRoles.VerifyAllExpectations();

		}

		[Test]
		public void GET_Create_UsesCreateOutputModel()
		{
			// Arrange

			var roleManager = new RoleManagerController();

			var builder = new TestControllerBuilder();

			builder.InitializeController(roleManager);



			roleManager.CreateOutputModel = new RoleManagerCreateOutputModel(
				);

			// act
			var result = roleManager.Create();

			// Assert
			Assert.IsInstanceOf<ViewResult>(result);

			var viewResult = result as ViewResult;

			var model = viewResult.ViewData.Model;

			Assert.IsInstanceOf<RoleManagerCreateOutputModel>(model);


		}

		[Test]
		public void POST_Create_Saves_NewRole()
		{
			const string ROLE_DESCRIPTION = " The glorious user role";
			const string ROLE_NAME = "Glorious";

			const string INFO_KEY = "info";


			// Arrange

			var saveOrUpdateCommand = MockRepository.GenerateMock<ISaveOrUpdateCommand<Role>>();

			saveOrUpdateCommand
				.Expect(c => c.Execute(Arg<Role>.Matches(
						p =>
						p.Description == ROLE_DESCRIPTION && p.Name == ROLE_NAME)));


			var roleManager = new RoleManagerController();

			var builder = new TestControllerBuilder();

			builder.InitializeController(roleManager);



			roleManager.CreateOutputModel = new RoleManagerCreateOutputModel(
				);
			roleManager.SaveOrUpdate = saveOrUpdateCommand;

			var inputModel = new RoleManagerCreateInputModel
			{
				Role = new RoleManagerCreateInputModel.RoleModel
				{
					Description = ROLE_DESCRIPTION,
					Name = ROLE_NAME
				}
			};
			// act
			var result = roleManager.Create(inputModel);

			// Assert
			Assert.IsInstanceOf<RedirectToRouteResult>(result);

			var viewResult = result as RedirectToRouteResult;

			Assert.AreEqual("List", viewResult.RouteValues["Action"]);

			saveOrUpdateCommand.VerifyAllExpectations();

			Assert.IsTrue(builder.TempDataDictionary.ContainsKey(INFO_KEY));
			Assert.IsTrue(

				builder.TempDataDictionary[INFO_KEY].ToString().Contains("Role"));

		}

		[Test]
		public void GET_Edit_Loads_Role_By_Id()
		{
			var fakeRole = MockRepository.GenerateMock<Role>();

			fakeRole.Expect(f => f.Description).Return("The glorious role");

			fakeRole.Expect(f => f.Name).Return("glorious");

			fakeRole.Expect(f => f.Id).Return(Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085"));

			fakeRole.Expect(f => f.Functions).Return(new Permission[] { });


			var roleManager = new RoleManagerController();

			var builder = new TestControllerBuilder();

			builder.InitializeController(roleManager);


			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();

			queryRoles.Expect(q => q.Load(fakeRole.Id)).Return(fakeRole);

			var queryFunctions = MockRepository.GenerateMock<IQueryService<Permission>>();

			queryFunctions.Expect(q=>q.Query()).Return(new Permission[]{}.AsQueryable());



			var queryFunction = MockRepository.GenerateMock<IQueryService<Permission>>();
			queryFunction.Expect(f => f.Query()).Return(new Permission[] { }.AsQueryable());

			// setup dependencies
			roleManager.EditOutputModel = new RoleManagerEditOutput(queryRoles, 
				queryFunctions
				);


			// Act

			var result = roleManager.Edit(fakeRole.Id);

			// Assert
			queryRoles.VerifyAllExpectations();
			queryFunctions.VerifyAllExpectations();

			Assert.IsInstanceOf<ViewResult>(result);

			var viewResult = result as ViewResult;

			Assert.IsInstanceOf<RoleManagerEditOutput>(viewResult.ViewData.Model);

			var model = viewResult.ViewData.Model as RoleManagerEditOutput;

			Assert.AreEqual(model.Role, fakeRole);

		}

		[Test]
		public void GET_Edit_RedirectsToHome_WhenNoId_IsSupplied()
		{
			var roleManager = new RoleManagerController();

			var result = roleManager.Edit(Guid.Empty);

			Assert.IsInstanceOf<RedirectToRouteResult>(result);

			var redirect2Root = result as RedirectToRouteResult;

			Assert.AreEqual("Index", redirect2Root.RouteValues["action"]);
			Assert.AreEqual("Home", redirect2Root.RouteValues["controller"]);

		}


		[Test]
		public void GET_Edit_RedirectsToHome_When_IdNotFound()
		{
			var queryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			queryRoles.Expect(c => c.Load(Arg<Guid>.Is.Anything)).IgnoreArguments().Return(null);

			var roleManager = new RoleManagerController();

			roleManager.EditOutputModel = new RoleManagerEditOutput(queryRoles, null);

			var result = roleManager.Edit(
				Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085")
			);

			Assert.IsInstanceOf<RedirectToRouteResult>(result);

			var redirect2Root = result as RedirectToRouteResult;

			Assert.AreEqual("Index", redirect2Root.RouteValues["action"]);
			Assert.AreEqual("Home", redirect2Root.RouteValues["controller"]);

		}

		[Test]
		public void POST_Assign_Adds_A_NewFunctionRight_To_Role()
		{
			var functionGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");
			var roleMan = new RoleManagerController();

			var assignModel = MockRepository.GenerateStub<RoleManagerAssignModel>(
				MockRepository.GenerateStub<IQueryService<Role>>(),
				MockRepository.GenerateStub<IQueryService<Permission>>(),
				MockRepository.GenerateStub<ISaveOrUpdateCommand<Role>>());

			assignModel.Expect(f => f.LinkFunctionToRole(functionGuid, roleGuid));

			roleMan.AssignModel = assignModel;

			var result = roleMan.Assign(functionGuid, roleGuid);

			Assert.IsNotNull(result);
			assignModel.VerifyAllExpectations();
		}

		[Test]
		public void POST_UnAssing_Removes_A_FunctionRight_From_Role()
		{
			var functionGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72085");
			var roleGuid = Guid.Parse("CF6C6EDA-F16B-11DF-A057-E112E0D72084");
			var roleMan = new RoleManagerController();

			var unAssignModel = MockRepository.GenerateStub<RoleManagerUnAssignModel>(
				MockRepository.GenerateStub<IQueryService<Role>>(),
				MockRepository.GenerateStub<IQueryService<Permission>>(),
				MockRepository.GenerateStub<ISaveOrUpdateCommand<Role>>());

			unAssignModel.Expect(f => f.RemoveFunctionFromRole(functionGuid, roleGuid));

			roleMan.UnAssignModel = unAssignModel;

			var result = roleMan.UnAssign(functionGuid, roleGuid);

			Assert.IsNotNull(result);
			unAssignModel.VerifyAllExpectations();
		}

		
	}
}
