using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Core.Persistence;

namespace Web.Models.UserManager
{
	public class UserManagerListModel
	{
		private IQueryService<User> queryEmployee;

		public UserManagerListModel( IQueryService<User> queryEmployee )
		{
			this.queryEmployee = queryEmployee;


			Users = queryEmployee.Query().ToList();
		}

		public List<User> Users
		{
			get;
			private set;
		}

		public string InfoMessage { get; set; }
	}
}