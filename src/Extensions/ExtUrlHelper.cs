using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Routing;
using Common.Mvc;
using Common.Web.Helpers;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{

	/// <summary>
	/// Adds extension methods for the <see cref="System.Web.Mvc.UrlHelper"/> class.
	/// </summary>
	public static class ExtUrlHelper
	{
		/// <summary>
		/// Generates a fully qualified URL for an action method by using the specified action name, controller name and area name.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="areaName"></param>
		/// <returns></returns>
		public static string AreaAction(this UrlHelper helper, string actionName, string controllerName, string areaName)
		{
			return helper.AreaAction(actionName, controllerName, areaName, null, null, null);
		}

		/// <summary>
		/// Generates a fully qualified URL for an action method by using the specified action name, controller name, area name and route values.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="areaName"></param>
		/// <param name="routeValues"></param>
		/// <returns></returns>
		public static string AreaAction(this UrlHelper helper, string actionName, string controllerName, string areaName, object routeValues)
		{
			return helper.AreaAction(actionName, controllerName, areaName, routeValues, null, null);
		}

		/// <summary>
		/// Generates a fully qualified URL for an action method by using the specified action name, controller name, area name and route values.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="areaName"></param>
		/// <param name="routeValues"></param>
		/// <returns></returns>
		public static string AreaAction(this UrlHelper helper, string actionName, string controllerName, string areaName, RouteValueDictionary routeValues)
		{
			return helper.AreaAction(actionName, controllerName, areaName, routeValues, null, null);
		}

		/// <summary>
		/// Generates a fully qualified URL for an action method by using the specified action name, controller name, area name, route values, protocol to use, and host name.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="actionName"></param>
		/// <param name="controllerName"></param>
		/// <param name="areaName"></param>
		/// <param name="routeValues"></param>
		/// <param name="protocol"></param>
		/// <param name="hostName"></param>
		/// <returns></returns>
		public static string AreaAction(this UrlHelper helper, string actionName, string controllerName, string areaName, object routeValues, string protocol, string hostName)
		{
			var dictRouteValues = new RouteValueDictionary(routeValues);
			dictRouteValues.Add("area", areaName);
			return helper.Action(actionName, controllerName, dictRouteValues, protocol, hostName);
		}

		/// <summary>
		/// Adds a string to the URL to enforce uniqueness based on the <paramref name="uniquification"/> rule provided.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="url"></param>
		/// <param name="uniquification"></param>
		/// <returns></returns>
		public static string UniquifyUrl(this UrlHelper helper, string url, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			return UrlUniquifier.UniquifyUrl(url, uniquification);
		}


	}
}
