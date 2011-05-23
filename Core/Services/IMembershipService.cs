using System;
namespace Core.Services
{
	public interface IMembershipService
	{
		bool ValidateUser( string userName, string password );
	}
}
