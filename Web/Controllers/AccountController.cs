using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Core.Domain;
using Persistence;
using Core.Services;
using Microsoft.Practices.Unity;
using Persistence.Queries.Employees;
using Web.Models.Account;
using Core.Persistence;

namespace Web.Controllers
{

	[HandleError]
	public class AccountController :Controller	{
		[Dependency]
		public IAuthenticationService FormsService
		{
			get;
			set;
		}
		[Dependency]
		public IMembershipService MembershipService
		{
			get;
			set;
		}

		[Dependency]
		public IQueryService<User> QueryUser
		{
			get;
			set;
		}

		[Dependency]

		public ISaveOrUpdateCommand<User> SaveUser
		{
			get;
			set;
		}

		[Dependency]
		public Models.Account.LogOnModel LogOnOutput
		{
			get;
			set;
		}

		// **************************************
		// URL: /Account/LogOn
		// **************************************

		public ActionResult LogOn()
		{
			return View(LogOnOutput);
		}

		[HttpPost]
		public ActionResult LogOn( LogOnModel model, string returnUrl )
		{
			if (ModelState.IsValid)
			{
				if (
				AllowDemo(model.UserName) || 	
					MembershipService.ValidateUser(model.UserName, model.Password))
				{
					FormsService.SignIn(model.UserName, model.RememberMe);

					//CreateUserIfNotFound(model.UserName);

					if (!String.IsNullOrEmpty(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError("", "The user name or password provided is incorrect.");
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		private bool AllowDemo(string username)
		{
			if (username == "admin")
			{
				return true;
			}
			return false;
	
		}

		private void CreateUserIfNotFound( string userName )
		{
			var user = QueryUser.Query(new UserByUserName(userName)).SingleOrDefault();

			if (user == default(User))
			{
				/// this is just a stub at this time, 
				/// the real workflow should involve
				/// someone from ITSupport or HR.
				SaveUser.Execute(new User
				{
					UserName = userName

				});			
			
			}
			
		}

		// **************************************
		// URL: /Account/LogOff
		// **************************************

		public ActionResult LogOff()
		{
			FormsService.SignOut();

			return RedirectToAction("Index", "Home");
		}

		


	}
}
