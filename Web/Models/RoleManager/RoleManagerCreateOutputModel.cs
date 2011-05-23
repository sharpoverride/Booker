using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Persistence.Queries;
using Persistence;

namespace Web.Models.RoleManager
{
	public class RoleManagerCreateOutputModel 
	{
		public Role Role
		{
			get;
			set;
		}
		public RoleManagerCreateOutputModel( )
		{
			Role = new Role();

		}

		public string InfoMessage
		{
			get;
			set;
		}
	}
}