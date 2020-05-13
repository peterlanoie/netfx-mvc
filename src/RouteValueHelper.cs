using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web;

namespace Common.Mvc
{
	/// <summary>
	/// Defines methods to help with routing related tasks.
	/// </summary>
	public static class RouteValueHelper
	{

		/// <summary>
		/// Combines all the values from all the <paramref name="collections"/> with fields from the <paramref name="values"/> object into 
		/// a single <see cref="RouteValueDictionary"/>. Collection keys that match object value keys will be ignored.
		/// </summary>
		/// <param name="values">Initial source of values to translate into the route value dictionary. Properties from this object take precedence over any subsequent properties.</param>
		/// <param name="excludeProperties">List of properties to exclude from combination.</param>
		/// <param name="collections">List of collections containing values to be combined.</param>
		/// <returns></returns>
		public static RouteValueDictionary Combine(object values, string[] excludeProperties, params NameValueCollection[] collections)
		{
			RouteValueDictionary routeValues = new RouteValueDictionary(values);
			foreach(var property in excludeProperties)
			{
				routeValues.Remove(property);
			}
			foreach(var collection in collections)
			{
				foreach(var key in collection.AllKeys.Where(x => !routeValues.ContainsKey(x) && !excludeProperties.Contains(x)))
				{
					routeValues.Add(key, collection[key]);
				}
			}
			return routeValues;
		}

		/// <summary>
		/// Appends the request's query string and form values to the object's values as a <see cref="RouteValueDictionary"/>.
		/// </summary>
		/// <param name="routeValues"></param>
		/// <param name="request"></param>
		/// <param name="excludeProperties"></param>
		/// <returns></returns>
		public static RouteValueDictionary MergeRequestValues(object routeValues, HttpRequest request, params string[] excludeProperties)
		{
			return RouteValueHelper.Combine(routeValues, excludeProperties, request.QueryString, request.Form);
		}

		/// <summary>
		/// Appends the request's query string and form values to the object's values as a <see cref="RouteValueDictionary"/>.
		/// </summary>
		/// <param name="routeValues"></param>
		/// <param name="request"></param>
		/// <param name="excludeProperties"></param>
		/// <returns></returns>
		public static RouteValueDictionary MergeRequestValues(object routeValues, HttpRequestBase request, params string[] excludeProperties)
		{
			return RouteValueHelper.Combine(routeValues, excludeProperties, request.QueryString, request.Form);
		}


	}
}
