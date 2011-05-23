using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Core.Persistence;

namespace Web.Models.UserManager
{
	public class UserManagerEditModel 	{
		private IQueryService<User> queryEmployees;
		private IQueryService<Role> queryRoles;

		public UserManagerEditModel(IQueryService<User> queryEmployees,
			IQueryService<Role> queryRoles )
		{
			this.queryEmployees = queryEmployees;
			this.queryRoles = queryRoles;
		}

		public virtual void Load( Guid id )
		{
			Roles = new List<EmployeeRole>();

			 User = queryEmployees.Load(id);
			if(User == null ) return;

			var empRoles = User.Roles.ToList();
			 var roles = queryRoles.Query().ToList();

			roles.ForEach(role => Roles.Add(
				new EmployeeRole{
					Role=role,
					IsLinked = empRoles.Exists(r => r== role)
				}));
		}

		public User User
		{
			get;
			private set;
		}

		public List<EmployeeRole> Roles
		{
			get;
			private set;
		}
		public class EmployeeRole
		{
			public Role Role
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