using System;
using System.Reflection;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Restricts the decorated method to a GET request made via AJAX.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AjaxGetAttribute : ActionMethodSelectorAttribute
	{
		/// <summary>
		/// Determines whether a request is an AJAX GET request.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return new AjaxAttribute(HttpVerbs.Get).IsValidForRequest(controllerContext, methodInfo);
		}
	}
}
