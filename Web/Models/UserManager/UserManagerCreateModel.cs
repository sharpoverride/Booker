using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;

namespace Web.Models.UserManager
{
	public class UserManagerCreateModel
	{
		public  User Employee
		{
			get;
			set;
		}
		public string InfoMessage
		{
			get;
			set;
		}
	}
}