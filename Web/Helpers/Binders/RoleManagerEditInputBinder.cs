using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.RoleManager;

namespace Web.Helpers.Binders
{
	public class RoleManagerEditInputBinder : IModelBinder
	{
		public object BindModel( ControllerContext controllerContext, ModelBindingContext bindingContext )
		{
			Guid id = Guid.Empty;

			if (controllerContext.RouteData.Values.ContainsKey("id"))
			{
				var submittedId = controllerContext.RouteData.Values["id"].ToString();
				Guid.TryParse(submittedId, out id);
					
			}

			bindingContext.Model =
			 new RoleManagerEditInput
			{
				Id = id
			};

			return bindingContext.Model;

		}
	}
}