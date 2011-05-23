using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Core.Security
{
	public class EvozonPrincipal : IPrincipal
	{
		IFunctionRightsService functionRight;
		IIdentity identity;

		public EvozonPrincipal(IFunctionRightsService functionRight, IIdentity identity)
		{
			this.functionRight = functionRight;

			this.identity = identity;
		}

		public IIdentity Identity
		{
			get
			{
				return identity;
			}
		}

		public bool IsInRole( string role )
		{
			var isInRole = functionRight.HasFunctionAssigned(role);
			return isInRole;
		}
	}
}
