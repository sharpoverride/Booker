using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Web.Models.RoleManager;
using Core.Domain;
using Persistence.Commands;
using Persistence.Queries;
using Persistence;
using Core.Persistence;

namespace Web.Controllers
{
	public class RoleManagerController :Controller	{
		[Dependency]
		public RoleManagerListOutputModel ListOutputModel
		{
			get;
			set;
		}

		[Dependency]
		public RoleManagerCreateOutputModel CreateOutputModel
		{
			get;
			set;
		}

		[Dependency]
		public ISaveOrUpdateCommand<Role> SaveOrUpdate
		{
			get;
			set;
		}


		[Dependency]
		public RoleManagerEditOutput EditOutputModel
		{
			get;
			set;
		}

		[Dependency]
		public RoleManagerAssignModel AssignModel
		{
			get;
			set;
		}
	
		[Dependency]
		public RoleManagerUnAssignModel UnAssignModel
		{
			get;
			set;
		}
		//
		// GET: /RoleManager/

		public ActionResult List()
		{

			ListOutputModel.InfoMessage = string.Empty + (string)TempData["info"];
			return View(ListOutputModel);
		}

		public ActionResult Create()
		{

			return View(CreateOutputModel);
		}

		[HttpPost]
		public ActionResult Create( RoleManagerCreateInputModel input )
		{
			if (!this.ModelState.IsValid)
				return View(CreateOutputModel);

			SaveOrUpdate.Execute(new Role
			{
				Name = input.Role.Name,
				Description = input.Role.Description
			});
			TempData.Add("info", "Your Role has been saved");

			return RedirectToAction("List");
		}


		public ActionResult Edit( Guid id )
		{
			if (id == Guid.Empty)
				return RedirectToAction("Index", "Home");

			EditOutputModel.Load(id);

			if (EditOutputModel.Role == null || EditOutputModel.Role.Id == Guid.Empty)
			{

				return RedirectToAction("Index", "Home");

			}

			return View(EditOutputModel);
		}

		[HttpPost]
		public EmptyResult Assign( Guid functionId, Guid roleId )
		{

			AssignModel.LinkFunctionToRole(functionId, roleId);
			return new EmptyResult();
		}



		public EmptyResult UnAssign( Guid functionId, Guid roleId )
		{
			UnAssignModel.RemoveFunctionFromRole(functionId, roleId);

			return new EmptyResult();
		}
	}
}
