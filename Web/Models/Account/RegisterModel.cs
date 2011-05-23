using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Web.Validation.Account;

namespace Web.Models.Account
{
	/// <summary>
	/// This is the model used by the Register view of the <c ref="Web.Controllers.AccountController">AccountController</c>
	/// </summary>
	[PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
	public class RegisterModel	{
		public RegisterModel()
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
		[DataType(DataType.EmailAddress)]
		[DisplayName("Email address")]
		public string Email
		{
			get;
			set;
		}

		[Required]
		[ValidatePasswordLength]
		[DataType(DataType.Password)]
		[DisplayName("Password")]
		public string Password
		{
			get;
			set;
		}

		[Required]
		[DataType(DataType.Password)]
		[DisplayName("Confirm password")]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}