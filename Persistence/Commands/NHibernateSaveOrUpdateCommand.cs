using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using NHibernate;
using Core.Persistence;

namespace Persistence.Commands
{
	public class NHibernateSaveOrUpdateCommand<ENTITY> : ISaveOrUpdateCommand<ENTITY> where ENTITY : DomainEntity
	{
		INHibernateUnitOfWork unitOfWork;
		public NHibernateSaveOrUpdateCommand(
		INHibernateUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;

		}
		public void Execute(ENTITY entity)
		{
			ITransaction transaction = unitOfWork.CurrentSession.BeginTransaction();

			try
			{

				unitOfWork.CurrentSession.SaveOrUpdate(entity);
				transaction.Commit();
			}
			catch (NHibernate.Exceptions.GenericADOException)
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
