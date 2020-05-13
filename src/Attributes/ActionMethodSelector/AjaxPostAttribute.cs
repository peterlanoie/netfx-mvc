using System;
using System.Reflection;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Restricts the decorated method to a POST request made via AJAX.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AjaxPostAttribute : ActionMethodSelectorAttribute
	{
		/// <summary>
		/// Determines whether a request is an AJAX POST request.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return new AjaxAttribute(HttpVerbs.Post).IsValidForRequest(controllerContext, methodInfo);
		}
	}
}
