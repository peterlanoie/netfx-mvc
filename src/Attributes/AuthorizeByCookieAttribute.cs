using System;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using System.Configuration;
using Common.Web;

namespace System.Web.Mvc
{
	/// <summary>
	/// Specifies a cookie value to match to authorize the decorated member.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class AuthorizeByCookieAttribute : AuthorizeAttribute
	{
		/// <summary>
		/// The name of the cookie to check for a matching value.
		/// This takes precendence over <see cref="CookieNameAppSettingKey"/>.
		/// </summary>
		public string CookieName { get; set; }

		/// <summary>
		/// The value of cookie that should match to pass authorization.
		/// This takes precendence over <see cref="CookieValueAppSettingKey"/>.
		/// </summary>
		public string CookieValue { get; set; }

		/// <summary>
		/// Creates a new instance with the required <paramref name="cookieName"/> and <paramref name="cookieValue"/>.
		/// </summary>
		/// <param name="cookieName"></param>
		/// <param name="cookieValue"></param>
		public AuthorizeByCookieAttribute(string cookieName, string cookieValue)
		{
			CookieName = cookieName;
			CookieValue = cookieValue;
		}

		/// <summary>
		/// Creates a new instance using default settings.
		/// </summary>
		public AuthorizeByCookieAttribute()
		{
			AuthorizeInDebugMode = false;
		}

		/// <summary>
		/// The key of the application setting entry that contains the cookie name to use.
		/// The value of <see cref="CookieName"/> (if present) takes precendence over this.
		/// </summary>
		public string CookieNameAppSettingKey { get; set; }

		/// <summary>
		/// The key of the application setting entry that contains the cookie value that must match.
		/// The value of <see cref="CookieValue"/> (if present) takes precendence over this.
		/// </summary>
		public string CookieValueAppSettingKey { get; set; }

		/// <summary>
		/// Whether or not to authorize when in DEBUG mode. 
		/// If true and in DEBUG mode, no additional check is made, the request is authorized.
		/// </summary>
		public bool AuthorizeInDebugMode { get; set; }

		/// <summary>
		/// Evaluates whether the request is authorized based on the status of the specified cookie and value.
		/// </summary>
		/// <param name="httpContext"></param>
		/// <returns></returns>
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			string name, value;

#if DEBUG
			if(AuthorizeInDebugMode)
			{
				return true;
			}
#endif

			name = !string.IsNullOrEmpty(CookieName) ? CookieName : ConfigurationManager.AppSettings[CookieNameAppSettingKey];
			value = !string.IsNullOrEmpty(CookieValue) ? CookieValue : ConfigurationManager.AppSettings[CookieValueAppSettingKey];

			if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
			{
				throw new InvalidOperationException(string.Format("A '{0}' requires a cookie name and cookie value. Either use the explicit value properties or the app setting key properties.", this.GetType().ToString()));
			}

			return Cookies.CookieHasValue(name, value);
		}

	}
}
