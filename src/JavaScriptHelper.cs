using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Common.Mvc
{
	/// <summary>
	/// Providers methods for building javascript tags.
	/// </summary>
	public static class JavaScriptHelper
	{
		/// <summary>
		/// Builds a standard javascript tag using the provided arguments.
		/// </summary>
		/// <param name="url">The URL of the script or NULL for inline script.</param>
		/// <param name="script">The inline script code, or NULL.</param>
		/// <param name="defer">Whether or not to defer script execution.</param>
		/// <param name="type">The type of the script.</param>
		/// <returns></returns>
		public static TagBuilder BuildJavaScriptTag(string url, string script, bool defer, string type = "text/javascript")
		{
			var builder = new TagBuilder("script");
			var strUrl = url;

			if(!string.IsNullOrEmpty(url))
			{
				if(strUrl.StartsWith("~"))
				{
					strUrl = VirtualPathUtility.ToAbsolute(strUrl);
				}
				builder.MergeAttribute("src", strUrl);
			}
			builder.MergeAttribute("type", type);
			if(defer)
			{
				builder.MergeAttribute("defer", "defer");
			}
			if(!string.IsNullOrEmpty(script))
			{
				builder.InnerHtml = script;
			}
			return builder;
		}

		/// <summary>
		/// Creates a JavaScript element fragment.
		/// </summary>
		/// <param name="url">The URL of the script or NULL for inline script.</param>
		/// <param name="script">The inline script code, or NULL.</param>
		/// <param name="defer">Whether or not to defer script execution.</param>
		/// <param name="type">The type of the script.</param>
		/// <returns></returns>
		public static string BuildJavaScriptFragment(string url, string script, bool defer, string type = "text/javascript")
		{
			return BuildJavaScriptTag(null, script, defer).ToString(TagRenderMode.Normal);
		}
	}
}
