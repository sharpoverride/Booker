using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Core.Persistence;

namespace Web.Models.UserManager
{
	public class UserManagerAssignModel
	{
		private readonly IQueryService<Role> queryRole;
		private readonly IQueryService<User> queryEmployee;
		private readonly ISaveOrUpdateCommand<User> updateEmployee;

		public UserManagerAssignModel( IQueryService<Role> queryRole,
			IQueryService<User> queryEmployee,
			ISaveOrUpdateCommand<User> updateRole)
		{
			this.queryEmployee = queryEmployee;
			this.queryRole = queryRole;

			this.updateEmployee = updateRole;
		}

		public virtual void LinkUserToRole( Guid userId, Guid roleId )
		{
			var employee = queryEmployee.Load(userId);
			var role = queryRole.Load(roleId);

			employee.AddRole(role);

			updateEmployee.Execute(employee);
		}
	}
}