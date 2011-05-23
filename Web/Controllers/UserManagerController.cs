using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Web.Models.UserManager;
using Core.Domain;
using Core.Persistence;

namespace Web.Controllers
{
	public class UserManagerController :
		Controller
	{
		[Dependency]
		public UserManagerListModel ListModel
		{
			get;
			set;
		}


		[Dependency]
		public UserManagerCreateModel CreateModel
		{
			get;
			set;
		}

		[Dependency]
		public UserManagerEditModel EditModel
		{
			get;
			set;
		}

		[Dependency]
		public UserManagerAssignModel AssignModel
		{
			get;
			set;
		}
		[Dependency]
		public UserManagerUnAssignModel UnAssignModel
		{
			get;
			set;
		}

		[Dependency]
		public ISaveOrUpdateCommand<User> SaveOrUpdate { get; set; }

		public ViewResult List()
		{
			ListModel.InfoMessage = string.Empty + (string)TempData["info"];
			return View(ListModel);
		}

		public ViewResult Create()
		{
			return View(CreateModel);
		}

		[HttpPost]
		public ActionResult Create( [Bind(Exclude = "Id")]User employee )
		{
			if (!this.ModelState.IsValid)
				return View(CreateModel);

			SaveOrUpdate.Execute(employee);
			TempData.Add("info", employee.UserName + " has been saved");


			return RedirectToAction("List");
		}

		public ActionResult Edit( Guid id )
		{
			EditModel.Load(id);


			return View(EditModel);

		}

		public EmptyResult Assign( Guid employeeId, Guid roleId )
		{
			AssignModel.LinkUserToRole(employeeId, roleId);

			return new EmptyResult();
		}



		public EmptyResult UnAssign( Guid employeeId, Guid roleId )
		{
			UnAssignModel.RemoveRole(employeeId, roleId);

			return new EmptyResult();
		}
	}
}