using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.Persistence{
	public interface IQueryService<ENTITY> where ENTITY : DomainEntity
	{
		ENTITY Load(Guid id);

		IQueryable<ENTITY> Query();

		IQueryable<ENTITY> Query( IDomainQuery<ENTITY> whereQuery );
	}
}
