using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Home
{
	public class EditBookmark
	{
		public Guid Id { get; set; }

		public string Notes { get; set; }

		public string Url { get; set; }
	}
	
}