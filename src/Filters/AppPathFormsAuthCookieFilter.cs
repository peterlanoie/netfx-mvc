using System.Web.Mvc;
using System.Web.Security;

namespace Common.Mvc.Filters
{
	/// <summary>
	/// Defines an action filter that sets the path of an outgoing forms authentication cookie 
	/// to the running application's URL path.
	/// </summary>
	public class AppPathFormsAuthCookieFilter : AppPathCookieFilter
	{
		/// <summary>
		/// Returns the forms authentication cookie name.
		/// </summary>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		protected override string GetCookieName(ResultExecutingContext filterContext)
		{
			return FormsAuthentication.FormsCookieName;
		}
	}
}