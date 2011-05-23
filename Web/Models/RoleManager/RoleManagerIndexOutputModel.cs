using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Persistence.Queries;
using Persistence;
using Core.Persistence;
namespace Web.Models.RoleManager
{
	public class RoleManagerListOutputModel
	{
		IQueryService<Role> queryRoles;

		public RoleManagerListOutputModel(IQueryService<Role> queryRoles)
		{
			this.queryRoles = queryRoles;


			Roles = this.queryRoles.Query().ToList(); // ToList actually does the DB call !!
		}


		public List<Role> Roles
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