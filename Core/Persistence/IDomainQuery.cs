using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Core.Domain;

namespace Core.Persistence
{
	public interface IDomainQuery<ENTITY>
		where ENTITY: DomainEntity
	{
		Expression<Func<ENTITY, bool>> Expression
		{
			get;
		}
	}
}
