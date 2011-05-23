using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Core.Persistence;

namespace Persistence
{
	public class NHibernateUnitOfWork : INHibernateUnitOfWork, IUnitOfWork
	{
		private readonly INHibernateSessionFactory sessionFactory;
		bool isDisposed;
		bool isInitialized;
		private ITransaction transaction;

		public NHibernateUnitOfWork(INHibernateSessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;

		}
		public NHibernate.ISession CurrentSession
		{
			get;
			private set;
		}

		public void Initialize()
		{
			ShouldNotBeDisposed();

			CurrentSession = this.sessionFactory.CreateSession();



			isInitialized = true;

		}


		public void Close()
		{
			ShouldNotBeDisposed();
			ShouldBeInitialized();
			if (!CurrentSession.IsOpen)
			{
				CurrentSession.Flush();
				CurrentSession.Close();
			}
		}


		public void Dispose()
		{
			if (isDisposed || isInitialized == false)
				return;

			CurrentSession.Dispose();

			isDisposed = true;
		}

		void ShouldNotBeDisposed()
		{
			if (isDisposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
		}

		void ShouldBeInitialized()
		{
			if (isInitialized == false)
			{
				throw new InvalidOperationException("NHibernate Unit Of Work is not initialized");

			}
		}

		void BeginNewTransaction()
		{
			if (transaction != null)
				transaction.Dispose();

			transaction = CurrentSession.BeginTransaction();
		}
	}
}
