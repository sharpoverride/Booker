using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Security
{
	public interface IFunctionRightsService
	{
		bool HasFunctionAssigned( string function );
	}
}
