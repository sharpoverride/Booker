using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistence;

namespace Persistence.Queries.Employees
{
	public class UserByUserName : IDomainQuery<User>
	{
		public UserByUserName(string name)
		{
			Expression = emp => emp.UserName == name; 
		}

		public System.Linq.Expressions.Expression<Func<User, bool>> Expression
		{
			get;
			private set;
		}
	}
}
