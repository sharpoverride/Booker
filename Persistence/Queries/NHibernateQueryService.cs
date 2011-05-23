using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using NHibernate.Linq;
using NHibernate;
using Core.Persistence;

namespace Persistence.Queries
{
	public class NHibernateQueryService<ENTITY> : IQueryService<ENTITY> where ENTITY: DomainEntity
	{
		INHibernateUnitOfWork unitOfWork;

		public NHibernateQueryService(INHibernateUnitOfWork unitOfWork)
		{

			this.unitOfWork = unitOfWork;

		}
		public ENTITY Load(Guid id)
		{
			var entity = unitOfWork.CurrentSession.Load<ENTITY>(id);

			return entity;
		}

		public IQueryable<ENTITY> Query()
		{
			IQueryable<ENTITY> query = unitOfWork.CurrentSession.Query<ENTITY>();
				
				//new NhQueryable<ENTITY>(unitOfWork.CurrentSession.GetSessionImplementation());			

			return query;
		}

		public IQueryable<ENTITY> Query( IDomainQuery<ENTITY> whereQuery )
		{
			//IQueryable<ENTITY> query = new NhQueryable<ENTITY>(unitOfWork.CurrentSession.GetSessionImplementation());				
			var query = Query().Where(whereQuery.Expression);

			return query;
		}
	}
}
