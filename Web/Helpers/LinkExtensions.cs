using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Microsoft.Web.Mvc;

namespace Web.Helpers
{
	public static class LinkExtensions
	{
		public static void BeginLink<TController>( this HtmlHelper helper,
		  Expression<Action<TController>> action) where TController : Controller
		{
			BeginLink(helper,
			 action,
			 new
			 {
			 });
		}


		public static void BeginLink<TController>( this HtmlHelper helper,
		 Expression<Action<TController>> action,
		 object htmlAttributes ) where TController : Controller
		{
			BeginLink(helper,
			 action,
			  new RouteValueDictionary(htmlAttributes));
		}

		public static void BeginLink<TController>( this HtmlHelper helper,
		 Expression<Action<TController>> action,
		 IDictionary<string, object> htmlAttributes ) where TController : Controller
		{
			TagBuilder builder = new TagBuilder("a");
			builder.MergeAttributes(htmlAttributes);
			string href = Microsoft.Web.Mvc.LinkExtensions.BuildUrlFromExpression(helper, action);
			builder.MergeAttribute("href", href);

			HttpResponseBase httpResponse = helper.ViewContext.HttpContext.Response;
			httpResponse.Write(builder.ToString(TagRenderMode.StartTag));

		}

		public static void EndLink( this HtmlHelper helper )
		{
			TagBuilder tagBuilder = new TagBuilder("a");
			HttpResponseBase httpResponse = helper.ViewContext.HttpContext.Response;
			httpResponse.Write(tagBuilder.ToString(TagRenderMode.EndTag));
		}
	}
}