using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Conventions;

namespace Persistence.Conventions
{
	public class ManyToManyConvention
		: ManyToManyTableNameConvention
	{
		protected override string GetBiDirectionalTableName( FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection, FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector otherSide )
		{
			return collection.EntityType.Name + otherSide.EntityType.Name + "s";// maybe an inflector would be better here
		}

		protected override string GetUniDirectionalTableName( FluentNHibernate.Conventions.Inspections.IManyToManyCollectionInspector collection )
		{
			return collection.EntityType.Name + collection.ChildType.Name + "s";// inflector any one :D
		}
	}
}
