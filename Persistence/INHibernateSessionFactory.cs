using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Persistence
{
	public interface INHibernateSessionFactory
	{
		ISession CreateSession();
	}
}
