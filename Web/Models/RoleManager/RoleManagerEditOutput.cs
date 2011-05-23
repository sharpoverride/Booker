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
	public class RoleManagerEditOutput 
			{

		private readonly IQueryService<Role> queryRoles;
		private readonly IQueryService<Permission> queryFunctions;

		public RoleManagerEditOutput( IQueryService<Role> queryRoles ,
			IQueryService<Permission> queryFunctions)
		{
			this.queryRoles = queryRoles;
			this.queryFunctions = queryFunctions;
		}
		public Role Role
		{
			get;
			private set;
		}

		public List<FunctionRoleModel> Functions
		{
			get;
			set;
		}

		public void Load( Guid id )
		{
			Role = queryRoles.Load(id);
			
			if (Role == null)
				return;

			var roleFunctions = Role.Functions.ToList();

			var functions = queryFunctions.Query().ToList();

			Functions = new List<FunctionRoleModel>();

			foreach (var function in functions)
			{
				Functions.Add(new FunctionRoleModel
				{
					Function = function,
					IsLinked = roleFunctions.Exists(f => f.Id == function.Id)
				});
			
			}

		}

		public class FunctionRoleModel
		{
			public Permission Function
			{
				get;
				set;
			}

			public bool IsLinked
			{
				get;
				set;
			}
		}
	}
}