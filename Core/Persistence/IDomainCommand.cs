using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.Persistence
{
	public interface IDomainCommand<ENTITY> where ENTITY:DomainEntity
	{
		void Exec(ENTITY entity);
	}
}
