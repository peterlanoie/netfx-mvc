using System;
using System.Reflection;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Restricts the decorated method to a request made via AJAX.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AjaxAttribute : ActionMethodSelectorAttribute
	{
		/// <summary>
		/// The HTTP verbs (if any) use to restrict the method.  Use bitwise OR to list multiple values.
		/// </summary>
		public HttpVerbs Verbs { get; set; }

		/// <summary>
		/// Creates a new attribute instance with the specified verbs.
		/// </summary>
		/// <param name="verbs"></param>
		public AjaxAttribute(HttpVerbs verbs)
		{
			Verbs = verbs;
		}

		/// <summary>
		/// Determines whether a request is an AJAX request.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return new AcceptVerbsAttribute(Verbs).IsValidForRequest(controllerContext, methodInfo) &&
				controllerContext.HttpContext.Request.IsAjaxRequest();
		}

	}
}
