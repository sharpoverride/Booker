using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using System.Web.Mvc;
using Web;
using Domain;

using FakeItEasy;
using Core.Persistence;
using Web.Models.Home;

namespace Tests.Unit.Controllers
{
	[TestFixture]
	public class HomeControllerTests
	{

		Guid bookmarkId = new Guid("9e249dc3-bccf-484f-8bb2-d56a19006b54");

		HomeController c = new HomeController();

		[Test]
		public void Index_Returns_BookmarkLists()
		{

			c.QueryBookmarks = A.Fake<IQueryService<Bookmark>>();

			var result = c.Index();

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<ViewResult>(result);

			var indexModel = (result as ViewResult).Model;

			Assert.IsInstanceOf<Web.Models.Home.IndexModel>(indexModel);

			List<Bookmark> bookmarks = (indexModel as Web.Models.Home.IndexModel).Bookmarks;


			Assert.IsNotNull(bookmarks);
		}

		[Test]
		public void Index_Should_Retrieve_List_Of_Bookmarks_From_QueryService()
		{

			c.QueryBookmarks = A.Fake<IQueryService<Bookmark>>();

			var call = A.CallTo(() => c.QueryBookmarks.Query());

			call.Returns<IQueryable<Bookmark>>(
				new Bookmark[]{
					new Bookmark{ Url="http://fakeiteasy.org", Notes="This should be found"}
				}.AsQueryable());


			var result = c.Index();


			call.MustHaveHappened();


		}

		[Test]
		public void AddBookmark_Should_Retrieve_View()
		{

			var result = c.AddBookmark();

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<ViewResult>(result);
		}

		[Test]
		public void AddBookmark_WithInput_Saves_Data()
		{
			c.SaveBookmark = A.Fake<ISaveOrUpdateCommand<Bookmark>>();

			var call = A.CallTo(() => c.SaveBookmark.Execute(
				A<Bookmark>.That.Matches(input => input.Url == "http://fakeiteasy.org")));


			var result = c.AddBookmark(new AddBookmark { Url = "http://fakeiteasy.org", Notes = "SOmething interesting" });

			Assert.IsNotNull(result);

			Assert.IsInstanceOf<RedirectToRouteResult>(result);

			Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["Action"]);


			call.MustHaveHappened();
		}

		[Test]
		public void EditBookmark_Should_RetrieveView()
		{

			c.QueryBookmarks = A.Fake<IQueryService<Bookmark>>();

			var loadCall = A.CallTo(() => c.QueryBookmarks.Load(bookmarkId));
			loadCall.Returns(
				new Bookmark { Url = "asdf", Notes = "asdf" });



			var result = c.EditBookmark(bookmarkId);

			loadCall.MustHaveHappened();

			Assert.IsInstanceOf<ViewResult>(result);
		}

		[Test]
		public void EditBookmark_WithInput_SavesData()
		{
			c.QueryBookmarks = A.Fake<IQueryService<Bookmark>>();

			c.SaveBookmark = A.Fake<ISaveOrUpdateCommand<Bookmark>>();

			var updateCall = A.CallTo(() => c.SaveBookmark.Execute(A<Bookmark>.Ignored));

			c.EditBookmark(A.Fake<EditBookmark>());


			updateCall.MustHaveHappened();

		}

		[Test]
		public void DeleteBookmark_RemovesData()
		{
			c.QueryBookmarks = A.Fake<IQueryService<Bookmark>>();

			A.CallTo(() => c.QueryBookmarks.Load(bookmarkId)).Returns(A.Fake<Bookmark>());

			c.DeleteBookmarkCommand = A.Fake<IDeleteCommand<Bookmark>>();

			var call = A.CallTo(() => c.DeleteBookmarkCommand.Execute(A<Bookmark>.Ignored));

			var result = c.DeleteBookmark(bookmarkId);

			Assert.IsInstanceOf<RedirectToRouteResult>(result);


		}


	}
}
