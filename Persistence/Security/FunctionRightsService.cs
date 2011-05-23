using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.Persistence;
using System.Web;
using Core.Security;
using Persistence.Queries.Functions;
using Persistence.Queries.Employees;

namespace Persistence.Security
{
	/// <summary>
	/// Based on the current logged in user, will determine
	/// function right access
	/// </summary>
	public class FunctionRightsService : IFunctionRightsService
	{
		IQueryService<Permission> queryFunctions;
		IQueryService<User> queryEmployees;

		string employeeName;
		
		public FunctionRightsService( HttpContextBase httpContext,
				IQueryService<Permission> queryFunctions,
				IQueryService<User> queryEmployees)
		{
			this.queryFunctions = queryFunctions;
			this.queryEmployees = queryEmployees;

			this.employeeName = httpContext.User.Identity.Name;
		}
		/// <summary>
		/// Checks wether or not this function has been assigned
		/// to the current employee
		/// </summary>
		/// <param name="function"></param>
		/// <returns></returns>
		public bool HasFunctionAssigned( string function )
		{

			var functionEntity = queryFunctions.Query(new FunctionByName(function)).FirstOrDefault();
			if (functionEntity == null) // no such function found in the system
				return false;

			var empl = queryEmployees.Query(new UserByUserName(this.employeeName)).FirstOrDefault();
			if (empl == null) // no employee logged in
				return false;

			var rolesAssignedToThisFunction = functionEntity.Roles.ToList();
			var rolesAssignedToThisEmployee = empl.Roles.ToList();


			foreach (var functionRight in rolesAssignedToThisFunction)
			{
				if (rolesAssignedToThisEmployee.Exists(role => role == functionRight))
					return true;
			}
			return false;
		}
	}

	// TODO REFACTOR TO OWN FILES 

	
	
}
