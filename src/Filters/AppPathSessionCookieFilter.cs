using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Common.Mvc.Filters
{
	/// <summary>
	/// Defines an action filter that sets the path of an outgoing ASP.NET session cookie 
	/// to the running application's URL path.
	/// </summary>
	public class AppPathSessionCookieFilter : AppPathCookieFilter
	{
		/// <summary>
		/// Return the name of the session cookie as provided by the session state configuration.
		/// </summary>
		/// <param name="filterContext"></param>
		/// <returns></returns>
		protected override string GetCookieName(ResultExecutingContext filterContext)
		{
			var sessionSettings = (SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
			return sessionSettings.CookieName;
		}
	}
}