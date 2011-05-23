using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Core.Persistence;

namespace Persistence
{
	/// <summary>
	/// Offers support for NHibernate as a unit of work
	/// </summary>
	public interface INHibernateUnitOfWork:IUnitOfWork
	{
		ISession CurrentSession
		{
			get;
		}
	}
}
