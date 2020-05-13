using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace Common.Mvc.Filters
{
	/// <summary>
	/// Defines a base result filter that sets the path of a cookie 
	/// to the application's runtime URL path. Derived types provide the 
	/// name of the cookie to modify.
	/// </summary>
	public abstract class AppPathCookieFilter : FilterAttribute, IResultFilter
	{
		/// <summary>
		/// Called before an action result executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public void OnResultExecuting(ResultExecutingContext filterContext)
		{
			var cookies = filterContext.HttpContext.Response.Cookies;
			if(cookies.Count <= 0) return;
			var cookieName = GetCookieName(filterContext);
			// if there's not outbound cookie for the specified cookie name, then exit
			if(!cookies.AllKeys.Contains(cookieName)) return;
			// we can't just check the collection using the indexer because it will create a cookie 
			// for the indexer parameter value if not already in the collection. we don't want to do that
			var cookie = filterContext.HttpContext.Response.Cookies[cookieName];
			if(cookie != null)
			{
				cookie.Path = VirtualPathUtility.ToAbsolute("~");
			}
		}

		/// <summary>
		/// Called after an action result executes.
		/// </summary>
		/// <param name="filterContext">The filter context.</param>
		public void OnResultExecuted(ResultExecutedContext filterContext)
		{
		}

		/// <summary>
		/// When implemented by a derived type, returns the name of the cookie to modify.
		/// </summary>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		protected abstract string GetCookieName(ResultExecutingContext filterContext);
	}
}