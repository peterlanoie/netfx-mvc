using System;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// 
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class HandleAjaxRedirectAttribute : FilterAttribute, IActionFilter
	{
		/// <summary>
		/// Called before an action method executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
		}

		/// <summary>
		/// Called after the action method executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			// is the result a redirect?
			if(!(filterContext.Result is RedirectResult) && !(filterContext.Result is RedirectToRouteResult)) return;
			// is this an AJAX request
			if(!filterContext.RequestContext.HttpContext.Request.IsAjaxRequest()) return;

			// if AJAX and a redirect, reconstruct the response into a standard json content response
			string redirectUrl;
			if(filterContext.Result is RedirectResult)
			{
				var redirResult = (RedirectResult)filterContext.Result;
				redirectUrl =  VirtualPathUtility.ToAbsolute(redirResult.Url);
			}
			else
			{
				var url = new UrlHelper(filterContext.RequestContext);
				var redirResult = (RedirectToRouteResult)filterContext.Result;
				redirectUrl = url.RouteUrl(redirResult.RouteName, redirResult.RouteValues);
			}

			// Currently this library references MVC 2 which doesn't have the "Permanent" property on the redirect results
			// Eventually the lib should be updated to reference later MVC version which do, however, that requires this
			// library to change its target framework since MVC 3+ require fx4.0.

			var jsonResult = new JsonResult
			{
				Data = new
				{
					browserAction = "redirect",
					statusCode = 302,
					location = redirectUrl
				},
				JsonRequestBehavior = JsonRequestBehavior.AllowGet
			};
			filterContext.Result = jsonResult;
		}
	}
}