using System;
using System.Reflection;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Specifies a submit button form element name to match to the decorated action method.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
	public class AcceptSubmitButtonAttribute : ActionMethodSelectorAttribute
	{
		/// <summary>
		/// The form element name of the submit button that will match to the decorated action method.
		/// </summary>
		public string ButtonName { get; set; }

		/// <summary>
		/// Creates a new instance with the required form submit button name.
		/// </summary>
		/// <param name="buttonName"></param>
		public AcceptSubmitButtonAttribute(string buttonName)
		{
			this.ButtonName = buttonName;
		}

		/// <summary>
		/// Checks whether the specified submit button was activated in the form. 
		/// If it matches, the decorated action method will be accepted as a match.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return !string.IsNullOrEmpty(controllerContext.RequestContext.HttpContext.Request[this.ButtonName]);
		}
	}
}
