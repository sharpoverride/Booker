using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistence;

namespace Persistence.Queries.Functions
{
	class FunctionByName : IDomainQuery<Permission>
	{
		string name;

		public FunctionByName(string name)
		{
			this.name = name;
			Expression = f => f.Name == this.name;
		}

		public System.Linq.Expressions.Expression<Func<Permission, bool>> Expression
		{
			get;
			private set;
		}
	}
}
