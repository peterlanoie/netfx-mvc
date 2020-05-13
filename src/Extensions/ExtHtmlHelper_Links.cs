using System.Linq;
using System.Web.Mvc.Html;
using System.Web.Routing;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for the standard MVC HtmlHelper.
	/// </summary>
	public static partial class ExtHtmlHelper
	{

		/// <summary>
		/// Generates a standard hyperlink to the <paramref name="url"/> with the specified <paramref name="text"/>.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="url">URL to be followed when the hyperlink is clicked.</param>
		/// <param name="text">Text to display in the link.</param>
		/// <returns></returns>
		public static MvcHtmlString Hyperlink(this HtmlHelper helper, string url, string text)
		{
			return MakeHyperlink(helper, url, text, new RouteValueDictionary());
		}

		/// <summary>
		/// Generates a standard hyperlink to the <paramref name="url"/> with the specified <paramref name="text"/> and <paramref name="htmlAttributes"/>.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="url"></param>
		/// <param name="text"></param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns></returns>
		public static MvcHtmlString Hyperlink(this HtmlHelper helper, string url, string text, object htmlAttributes)
		{
			return MakeHyperlink(helper, url, text, new RouteValueDictionary(htmlAttributes));
		}

		internal static MvcHtmlString MakeHyperlink(this HtmlHelper helper, string url, string text, RouteValueDictionary htmlAttributes)
		{
			TagBuilder builder = new TagBuilder("a");

			if(url != null && url.Contains('~'))
			{
				url = VirtualPathUtility.ToAbsolute(url);
			}

			builder.MergeAttribute("href", url);
			if(htmlAttributes != null)
			{
				builder.MergeAttributes(htmlAttributes);
			}
			builder.InnerHtml = text;
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
		}

		/// <summary>
		/// Returns an anchor element (a element) that contains the virtual path of the specified area action.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="linkText">The inner text of the anchor element.</param>
		/// <param name="actionName">The name of the action.</param>
		/// <param name="controllerName">The name of the controller.</param>
		/// <param name="areaName">The name of the area.</param>
		/// <returns></returns>
		public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, string areaName)
		{
			return helper.ActionLink(linkText, actionName, controllerName, areaName, null);
		}

		/// <summary>
		/// Returns an anchor element (a element) that contains the virtual path of the specified area action.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="linkText">The inner text of the anchor element.</param>
		/// <param name="actionName">The name of the action.</param>
		/// <param name="controllerName">The name of the controller.</param>
		/// <param name="areaName">The name of the area.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns></returns>
		public static MvcHtmlString ActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, string areaName, object htmlAttributes)
		{
			return helper.ActionLink(linkText, actionName, controllerName, new { area = areaName }, htmlAttributes);
		}

		/// <summary>
		/// Creates a non-operational hyperlink (href=javascript:void(0)). Helpful for make client-side action links that don't go anywhere.
		/// Specify the link text.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText">Text of the link.</param>
		/// <returns></returns>
		public static MvcHtmlString NoopHyperlink(this HtmlHelper helper, string linkText)
		{
			return helper.NoopHyperlink(linkText, null, null);
		}

		/// <summary>
		/// Creates a non-operational hyperlink (href=javascript:void(0)). Helpful for make client-side action links that don't go anywhere.
		/// Specify the link text and element ID.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText">Text of the link.</param>
		/// <param name="id">Link element ID.</param>
		/// <returns></returns>
		public static MvcHtmlString NoopHyperlink(this HtmlHelper helper, string linkText, string id)
		{
			return helper.NoopHyperlink(linkText, id, null);
		}

		/// <summary>
		/// Creates a non-operational hyperlink (href=javascript:void(0)). Helpful for make client-side action links that don't go anywhere.
		/// Specify the link text, element ID and class(es).
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText">Text of the link.</param>
		/// <param name="id">Link element ID.</param>
		/// <param name="class"></param>
		/// <returns></returns>
		public static MvcHtmlString NoopHyperlink(this HtmlHelper helper, string linkText, string id, string @class)
		{
			return helper.NoopHyperlink(linkText, new { id, @class });
		}

		/// <summary>
		/// Creates a non-operational hyperlink (href=javascript:void(0)). Helpful for make client-side action links that don't go anywhere.
		/// Specify the link text and any additional HTML attributes.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="linkText">Text of the link.</param>
		/// <param name="htmlAttributes">Arbitrary HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString NoopHyperlink(this HtmlHelper helper, string linkText, object htmlAttributes)
		{
			return helper.Hyperlink("javascript:void(0);", linkText, htmlAttributes);
		}

	}
}
