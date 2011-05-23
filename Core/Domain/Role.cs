using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain
{
	public class Role: DomainEntity
	{
		public virtual string Name
		{
			get;
			set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual IList<Permission> Functions
		{
			get;
			set;
		}

		public virtual IList<User> Employees
		{
			get;
			set;
		}

		public Role()
		{
			Functions = new List<Permission>();
			Employees = new List<User>();
		}

		public virtual void AddFunction( Permission function )
		{
			Functions.Add(function);
		}

		public virtual void AddEmployee( User employee )
		{
			Employees.Add(employee);
		}

		public virtual void RemoveFunction( Permission function )
		{
			function.Roles.Remove(this);
			this.Functions.Remove(function);
		}
	}
}
