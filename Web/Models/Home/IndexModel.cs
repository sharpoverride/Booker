using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace Web.Models.Home
{
	/// <summary>
	/// This is the model used by the Index view of the HomeController
	/// </summary>
	public class IndexModel
	{

		List<Bookmark> _bookmarks;

		public IndexModel()
		{
			Bookmarks = new List<Domain.Bookmark>();
		}
		public string Message
		{
			get;
			set;
		}

		public List<Domain.Bookmark> Bookmarks
		{
			get
			{
				return _bookmarks;

			}
			set
			{
				if (value == null) return;

				_bookmarks = value;
			}
		}


	}
}