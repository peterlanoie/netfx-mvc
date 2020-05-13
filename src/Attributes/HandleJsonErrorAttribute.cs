using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Common.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Defines exception filtering behavior that generates a standardized JSON response
	/// when an action results in a thrown exception.  Use this to have an action method
	/// return JSON to an AJAX call that can be checked for error conditions and associated
	/// error information.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class HandleJsonErrorAttribute : FilterAttribute, IExceptionFilter
	{
		/// <summary>
		/// True to limit this filter's execution to only Ajax requests.
		/// The default is true.
		/// </summary>
		public bool AjaxOnlyRequest { get; set; }

		/// <summary>
		/// List of expected accept types by which the execution of this filter will be filtered.
		/// If any accept type is listed, at least one should match the request's list.
		/// </summary>
		public string[] AcceptTypes { get; set; }

		/// <summary>
		/// Specifies the HTTP status code to return in the request. Default is 500.
		/// </summary>
		public int HttpStatusCode { get; set; }

		/// <summary>
		/// Whether or not to include the exception details in the result.
		/// </summary>
		public bool IncludeException { get; set; }

		/// <summary>
		/// The message to return in the JSON response when this filter handles an exception.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// The type of the exception to handle. If not defined, all exceptions will be handled.
		/// </summary>
		public Type ExceptionType { get; set; }

		/// <summary>
		/// Defines the event handler type for when an exception is handled by this filter.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="filterContext"></param>
		public delegate void ExceptionHandledEventHandler(Object sender, ExceptionContext filterContext);

		/// <summary>
		/// Event raised when this filter handles an exception.
		/// </summary>
		public event ExceptionHandledEventHandler ExceptionHandled;

		/// <summary>
		/// Creates a new instance of the filter.
		/// </summary>
		public HandleJsonErrorAttribute()
		{
			AjaxOnlyRequest = true;
			HttpStatusCode = 500;
			IncludeException = false;
		}

		/// <summary>
		/// Creates a new instance of the exception filter with the specified <paramref name="message"/> for all exception types.
		/// </summary>
		/// <param name="message">The message to return in the JSON response when this filter handles an exception.</param>
		public HandleJsonErrorAttribute(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Creates a new instance of the exception filter for the specified <paramref name="exceptionType"/>.
		/// </summary>
		/// <param name="exceptionType">The exception type to handle.</param>
		public HandleJsonErrorAttribute(Type exceptionType)
		{
			this.ExceptionType = exceptionType;
		}

		/// <summary>
		/// Creates a new instance of the exception filter with the specified <paramref name="message"/> for the specified <paramref name="exceptionType"/>.
		/// </summary>
		/// <param name="exceptionType">The exception type to handle.</param>
		/// <param name="message">The message to return in the JSON response when this filter handles an exception of the type specified by <paramref name="exceptionType"/> occurs.</param>
		public HandleJsonErrorAttribute(Type exceptionType, string message)
		{
			this.ExceptionType = exceptionType;
			this.Message = message;
		}

		#region IExceptionFilter Members

		/// <summary>
		/// Handles the standard filter method.
		/// </summary>
		/// <param name="filterContext"></param>
		public void OnException(ExceptionContext filterContext)
		{
			JsonErrorResult errorResult;

			if(
				// has the exception already been handled?
				filterContext.ExceptionHandled
				// there gots to be an exception!
				|| (filterContext.Exception == null)
			)
			{
				// don't process it, just return
				return;
			}

			// if an exception type has been defined and the exception is NOT that type
			if(this.ExceptionType != null && this.ExceptionType != filterContext.Exception.GetType())
			{
				return;
			}

			// if we only care about handling on ajax requests and this is NOT one...
			if(AjaxOnlyRequest && !filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
			{
				// ... then don't do anything.
				return;
			}

			// if 1 or more accept types have been specified,
			if(AcceptTypes != null && AcceptTypes.Length > 0)
			{
				// see that at least 1 of the types exists in both lists.
				if(AcceptTypes.Join(filterContext.HttpContext.Request.AcceptTypes, t => t, t => t, (t, r) => t).Count() == 0)
				{
					return;
				}
			}

			errorResult = JsonErrorResult.Create(IncludeException ? filterContext.Exception : null, this.Message);

			// set the call result to the JSON error result package
			filterContext.Result = new JsonResult() { Data = errorResult, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
			// mark the exception as handled to stop further error filter handling

			filterContext.HttpContext.Response.StatusCode = HttpStatusCode;
			filterContext.ExceptionHandled = true;

			NotifyExceptionHandled(filterContext);
		}

		private void NotifyExceptionHandled(ExceptionContext filterContext)
		{
			if (this.ExceptionHandled != null)
			{
				this.ExceptionHandled(this, filterContext);
			}
		}

		#endregion

	}
}
