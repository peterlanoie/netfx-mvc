using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;
using Common.Mvc;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{

	/// <summary>
	/// Defines extension methods for a <see cref="ViewUserControl"/>.
	/// </summary>
	public static class ExtViewUserControl
	{

		/// <summary>
		/// Appends the current request's query string values to the object's values as a <see cref="RouteValueDictionary"/>.
		/// </summary>
		/// <param name="userControl">Current user control.</param>
		/// <param name="routeValues">Object values to which the query string values will be combined.</param>
		/// <param name="excludeProperties">List of properties to exclude from combining.</param>
		/// <returns></returns>
		public static RouteValueDictionary MergeQSValues(this ViewUserControl userControl, object routeValues, params string[] excludeProperties)
		{
			return RouteValueHelper.Combine(routeValues, excludeProperties, userControl.Request.QueryString);
		}

		/// <summary>
		/// Appends the current request's query string values to the object's values as a <see cref="RouteValueDictionary"/>.
		/// </summary>
		/// <param name="userControl"></param>
		/// <param name="routeValues"></param>
		/// <param name="excludeProperties"></param>
		/// <returns></returns>
		public static RouteValueDictionary MergeFormValues(this ViewUserControl userControl, object routeValues, params string[] excludeProperties)
		{
			return RouteValueHelper.Combine(routeValues, excludeProperties, userControl.Request.Form);
		}

		/// <summary>
		/// Appends the current request's query string and form values to the object's values as a <see cref="RouteValueDictionary"/>.
		/// </summary>
		/// <param name="userControl"></param>
		/// <param name="routeValues"></param>
		/// <param name="excludeProperties"></param>
		/// <returns></returns>
		public static RouteValueDictionary MergeRequestValues(this ViewUserControl userControl, object routeValues, params string[] excludeProperties)
		{
			return RouteValueHelper.MergeRequestValues(routeValues, userControl.Request, excludeProperties);
		}
	}
}
