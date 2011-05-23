using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Persistence;
using Domain;
using Microsoft.Practices.Unity;
using Web.Models.Home;
using Core.Persistence;
using Web.Security;
using Core.Security;

namespace Web.Controllers
{
	[HandleError]
	[Requires(FunctionRights=FunctionRights.Employee)]
	public class HomeController : Controller
	{
		[Dependency]
		public IQueryService<Bookmark> QueryBookmarks { get; set; }
		[Dependency]
		public ISaveOrUpdateCommand<Bookmark> SaveBookmark { get; set; }

		[Dependency]
		public IDeleteCommand<Bookmark> DeleteBookmarkCommand { get; set; }


		public ActionResult Index()
		{
			var model = new Models.Home.IndexModel();

			model.Message = "Evozone starter kit";

			model.Bookmarks = QueryBookmarks.Query().ToList();

			return View(model);
		}

		public ActionResult About()
		{
			var model = new Models.Home.AboutModel();
			return View(model);
		}


		public ViewResult AddBookmark()
		{
			var model = new AddBookmark();

			return View(model);

		}

		[HttpPost]
		public ActionResult AddBookmark(AddBookmark input)
		{
			var bookmark = new Bookmark
			{
				Notes = input.Notes,
				Url = input.Url
			};

			SaveBookmark.Execute(bookmark);

			return RedirectToAction("Index");
		}


		public ActionResult EditBookmark(Guid guid)
		{

			var bookmark = QueryBookmarks.Load(guid);

			return View(bookmark);
		}

		[HttpPost]
		public RedirectToRouteResult EditBookmark(EditBookmark input)
		{
			var bookmark = QueryBookmarks.Load(input.Id);

			bookmark.Url = input.Url;
			bookmark.Notes = input.Notes;

			SaveBookmark.Execute(bookmark);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public RedirectToRouteResult DeleteBookmark(Guid bookmarkId)
		{

			var bookmark = QueryBookmarks.Load(bookmarkId);

			DeleteBookmarkCommand.Execute(bookmark);

			return RedirectToAction("Index");

		}
	}
}
