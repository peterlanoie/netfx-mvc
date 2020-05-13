using System;
using System.Reflection;
using System.Web.Mvc;

namespace System.Web.Mvc
{
	/// <summary>
	/// Similar to "AcceptVerbs", but action match behavior is based on a parameter value match.
	/// Checks the request for a parameter with a matching value. If not a match, then action isn't a match.
	/// Comparison is case insensitive.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AcceptParameterValueAttribute : ActionMethodSelectorAttribute
	{
		/// <summary>
		/// The name of the paramater to check.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The desired value of the parameter to match for this action.
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// Creates a new instance of the attribute with the required <paramref name="name"/> and <paramref name="value"/>.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		public AcceptParameterValueAttribute(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		/// Checks that the request contains the desired value for the named parameter.
		/// </summary>
		/// <param name="controllerContext"></param>
		/// <param name="methodInfo"></param>
		/// <returns></returns>
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			var parameter = controllerContext.RequestContext.HttpContext.Request[this.Name];

			if(string.IsNullOrEmpty(parameter))
			{
				return false;
			}
			return parameter.ToLower() == this.Value.ToLower();
		}
	}
}
