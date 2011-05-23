using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Novell.Directory.Ldap;
using System.Text;

namespace Core.Services
{
	public class LdapMembershipService : Core.Services.IMembershipService
	{

		public bool ValidateUser( string userName, string password )
		{
            return false;
		}

	}
}
