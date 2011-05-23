using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistence.Queries;
using Core.Domain;
using Persistence.Commands;
using Core.Persistence;

namespace Web.Models.UserManager
{
	public class UserManagerUnAssignModel
	{
		private readonly IQueryService<Role> queryRole;
		private readonly IQueryService<User> queryEmployee;
		private readonly ISaveOrUpdateCommand<User> updateEmployee;

		public UserManagerUnAssignModel( IQueryService<Role> queryRole,
			IQueryService<User> queryFunction,
			ISaveOrUpdateCommand<User> updateEmployee )
		{
			this.queryEmployee = queryFunction;
			this.queryRole = queryRole;

			this.updateEmployee = updateEmployee;
		}

		public virtual void RemoveRole( Guid employeeId, Guid roleId )
		{
			var employee = queryEmployee.Load(employeeId);
			var role = queryRole.Load(roleId);

			employee.RemoveRole(role);


			updateEmployee.Execute(employee);
		}


	}
}