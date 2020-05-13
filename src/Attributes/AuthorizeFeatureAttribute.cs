using System;
using System.Web.Mvc;
using Common.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Defines an enhanced authorize action filter.  
	/// This extends the standard Authorize filter to incorporate automatic AJAX response when needed.
	/// When not authorized, the filter will force the return of either the specified view 
	/// or a JSON formatted <see cref="JsonErrorResult"/> with the specified error message.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeFeatureAttribute : AuthorizeAttribute
	{
		private string _jsonMessage = "You are not currently authorized to access to the requested feature.";
		private string _errorView;

		/// <summary>
		/// Message to return in a JsonErrorResult when the response is to an Ajax request.
		/// </summary>
		public string JsonErrorMessage
		{
			get { return _jsonMessage; }
			set { _jsonMessage = value; }
		}

		/// <summary>
		/// The view to return.
		/// </summary>
		public string ErrorView
		{
			get { return _errorView; }
			set { _errorView = value; }
		}

		/// <summary>
		/// Handler override.
		/// </summary>
		/// <param name="filterContext"></param>
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			// We are NOT authenticated
			if(filterContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.Result = new JsonResult()
				{
					Data = JsonErrorResult.Create(new UnauthorizedAccessException(), _jsonMessage),
					JsonRequestBehavior = JsonRequestBehavior.AllowGet
				};
			}
			else
			{
				if(!string.IsNullOrEmpty(_errorView))
				{
					filterContext.Result = new ViewResult() {
						ViewName = _errorView,
						ViewData = filterContext.Controller.ViewData,
						TempData = filterContext.Controller.TempData,
					};
				}
			}
		}
	}
}