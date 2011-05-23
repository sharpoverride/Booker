using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain
{
	public class User : DomainEntity
	{
		public virtual string UserName
		{
			get;
			set;
		}

		public virtual IList<Role> Roles
		{
			get;
			set;
		}

		public User()
		{
			Roles = new List<Role>();
		}

		public virtual void AddRole( Role role )
		{
			Roles.Add(role);
		}

		public virtual void RemoveRole( Role role )
		{
			role.Employees.Remove(this);
			this.Roles.Remove(role);
		}

		public override string ToString()
		{
			return "" +Id;
		}
	}
}
