using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Core.Domain;
using Persistence.Queries;
using Persistence;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.RoleManager
{
	public class RoleManagerCreateInputModel
	{
		public class RoleModel
		{
			[Required]
			public string Name
			{
				get;
				set;
			}
			[Required]
			public string Description
			{
				get;
				set;
			}
		}

		public RoleModel Role
		{
			get;
			set;
		}
	}
}