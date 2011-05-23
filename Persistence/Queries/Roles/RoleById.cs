using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistence;

namespace Persistence.Queries.Roles
{
	public class RoleById
		:IDomainQuery<Role>
	{
		public RoleById(Guid roleId)
		{
			Expression = r => r.Id == roleId;
		}
		public System.Linq.Expressions.Expression<Func<Role, bool>> Expression
		{
			get;
			private set;
		}
	}
}
