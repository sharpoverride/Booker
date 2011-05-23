using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web.Models.Account
{
	/// <summary>
	/// This is the model used by the LogOn view of the <c ref="Web.Controllers.AccountController">AccountController</c>
	/// </summary>
	public class LogOnModel
	{

		public LogOnModel()
		{

		}
		[Required]
		[DisplayName("User name")]
		public string UserName
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Password")]
		public string Password
		{
			get;
			set;
		}

		[DisplayName("Remember me?")]
		public bool RememberMe
		{
			get;
			set;
		}
	}
}