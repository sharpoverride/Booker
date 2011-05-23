using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using Core.Domain;

namespace Persistence.Conventions
{
	public class DomainEntityMappingConvention : DefaultAutomappingConfiguration
	{
		public override bool ShouldMap( Type type )
		{
			var isValid = 
				typeof(DomainEntity) != type &&
				typeof(DomainEntity).IsAssignableFrom(type);

			return isValid;
		}
	
	}
}
