using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
	/// <summary>
	/// Implementors of this service will ofer the ability to sign
	/// users in and out
	/// </summary>
	public interface IAuthenticationService
	{
		void SignIn( string userName, bool createPersistentCookie );
		void SignOut();
	}
}
