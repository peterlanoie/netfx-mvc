using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Mvc;
using Common.Web.Helpers;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods that help create script and style tags for referenced resources or inline definitions.
	/// </summary>
	public static partial class ExtHtmlHelper
	{

		/// <summary>
		/// Return a javascript file reference for use in an HTML head section.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="url">Absolute or relative URL to the script file. Relative paths will be resolved.</param>
		/// <param name="defer">Whether or not to defer execution of the script. Useful for files containing immediately runnable code.</param>
		/// <param name="uniquification">The rule for uniquifying the URL.  Uniquified URLs will include the current clock ticks to help encourage the browser to always load it.</param>
		/// <returns></returns>
		public static MvcHtmlString JSFile(this HtmlHelper helper, string url, bool defer = false, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			return MvcHtmlString.Create(new JsFileResource(url, defer, uniquification).GetHtmlFragment());
		}

		/// <summary>
		/// Return a javascript block containing the contents of <paramref name="script" />.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="script">The script to write to the script block.</param>
		/// <returns></returns>
		public static MvcHtmlString JSBlock(this HtmlHelper helper, string script)
		{
			return JSBlock(helper, true, script, false);
		}

		/// <summary>
		/// Return a javascript block containing the contents of <paramref name="script" />
		/// if <paramref name="condition" /> is true.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">The condition that must be met to display the script block.</param>
		/// <param name="script">The script to write to the script block.</param>
		/// <returns></returns>
		public static MvcHtmlString JSBlock(this HtmlHelper helper, bool condition, string script)
		{
			return JSBlock(helper, condition, script, false);
		}

		/// <summary>
		/// Return a javascript block containing the contents of <paramref name="script" />
		/// with an optional execution deferment.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="script">The script to write to the script block.</param>
		/// <param name="defer">Whether or not to defer script execution until after page load completion.</param>
		/// <returns></returns>
		public static MvcHtmlString JSBlock(this HtmlHelper helper, string script, bool defer)
		{
			return JSBlock(helper, true, script, defer);
		}

		/// <summary>
		/// Return a javascript block containing the contents of <paramref name="script" />
		/// if <paramref name="condition" /> is true
		/// with an optional execution deferment.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">The condition that must be met to display the script block.</param>
		/// <param name="script">The script to write to the script block.</param>
		/// <param name="defer">Whether or not to defer script execution until after page load completion.</param>
		/// <returns></returns>
		public static MvcHtmlString JSBlock(this HtmlHelper helper, bool condition, string script, bool defer)
		{
			if(condition)
			{
				return MvcHtmlString.Create(JavaScriptHelper.BuildJavaScriptFragment(null, script, defer));
			}
			else
			{
				return MvcHtmlString.Empty;
			}
		}

		/// <summary>
		/// Generates an opening script tag.  Can be used within a "using" block and the tag will auto-close.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		public static TagBlock BeginJSBlock(this HtmlHelper helper)
		{
			return helper.BeginJSBlock(false);
		}

		/// <summary>
		/// Generates an opening script tag.  Can be used within a "using" block and the tag will auto-close.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="defer">Whether or not to defer script execution until after page load completion.</param>
		/// <returns></returns>
		public static TagBlock BeginJSBlock(this HtmlHelper helper, bool defer)
		{
			var tag = new TagBlock(helper.ViewContext, "script");
			tag.AddAttribute("type", "text/javascript");
			if(defer)
			{
				tag.AddAttribute("defer", "defer");
			}
			tag.OpenBlock();
			return tag;
		}

		/// <summary>
		/// Writes out the closing script tag.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		public static void EndJSBlock(this HtmlHelper helper)
		{
			helper.ViewContext.Writer.Write("</script>");
		}

		//private static TagBuilder BuildJavaScriptTag(string url, string script, bool defer, string type = "text/javascript")
		//{
		//    TagBuilder builder = new TagBuilder("script");
		//    string strUrl = url;

		//    if(!string.IsNullOrEmpty(url))
		//    {
		//        if(!Uri.IsWellFormedUriString(strUrl, UriKind.Absolute))
		//        {
		//            strUrl = VirtualPathUtility.ToAbsolute(strUrl);
		//        }
		//        builder.MergeAttribute("src", strUrl);
		//    }
		//    builder.MergeAttribute("type", type);
		//    if(defer)
		//    {
		//        builder.MergeAttribute("defer", "defer");
		//    }
		//    if(!string.IsNullOrEmpty(script))
		//    {
		//        builder.InnerHtml = script;
		//    }
		//    return builder;
		//}

	}
}
