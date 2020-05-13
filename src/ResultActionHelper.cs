using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Common.Mvc
{

	/// <summary>
	/// Defines helpers for MVC result actions.
	/// </summary>
	public static class ResultActionHelper
	{
		/// <summary>
		/// Converts a redirect result action into a client side script location change instruction wrapped in HTML.
		/// Useful for delivering a client side redirect action in an HTML payload.
		/// </summary>
		/// <param name="redirectResult"></param>
		/// <returns></returns>
		public static string MakeRedirectHtmlPayload(RedirectResult redirectResult)
		{
			return JavaScriptHelper.BuildJavaScriptTag(null, MakeRedirectJsAction(redirectResult), false).ToString(TagRenderMode.Normal);
		}

		/// <summary>
		/// Converts a redirect result action into a client side script location change instruction.
		/// </summary>
		/// <param name="redirectResult"></param>
		/// <returns></returns>
		public static string MakeRedirectJsAction(RedirectResult redirectResult)
		{
			return string.Format("window.location = \"{0}\";", redirectResult.Url);
		}

	}
}
