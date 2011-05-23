using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using NHibernate.Exceptions;
using NHibernate;
using Core.Persistence;

namespace Persistence.Commands
{
	public class NHibernateDeleteCommand<ENTITY> : IDeleteCommand<ENTITY> where ENTITY : DomainEntity
	{
		private readonly INHibernateUnitOfWork unitOfWork;

		public NHibernateDeleteCommand(INHibernateUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public void Execute(ENTITY entity)
		{
			ITransaction transaction = unitOfWork.CurrentSession.BeginTransaction();

			try
			{
				this.unitOfWork.CurrentSession.Delete(entity);
				transaction.Commit();
			}
			catch (GenericADOException)
			{
				transaction.Rollback();
			}
			finally
			{
				transaction.Dispose();
				transaction = null;

			}
		}
	}
}
