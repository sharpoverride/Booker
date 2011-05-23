using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Persistence.Queries;
using Persistence;
using Persistence.Commands;
using Core.Persistence;

namespace Web.Models.RoleManager
{
	public class RoleManagerAssignModel
	{
		private readonly IQueryService<Role> queryRole;
		private readonly IQueryService<Permission> queryFunction;
		private readonly ISaveOrUpdateCommand<Role> updateRole;

		public RoleManagerAssignModel(IQueryService<Role> queryRole,
			IQueryService<Permission> queryFunction,
			ISaveOrUpdateCommand<Role> updateRole)
		{
			this.queryFunction = queryFunction;
			this.queryRole = queryRole;

			this.updateRole = updateRole;
		}

		public virtual void LinkFunctionToRole( Guid functionId, Guid roleId )
		{
			var function = queryFunction.Load(functionId);
			var role = queryRole.Load(roleId);

			role.AddFunction(function);

			updateRole.Execute(role);
		}
	}
}