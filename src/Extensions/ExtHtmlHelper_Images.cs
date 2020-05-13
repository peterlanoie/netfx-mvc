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
		/// Returns a self closed image element with the specified <paramref name="src"/> and alternate text.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="src">Source Url of the image. A relative value will be resolved.</param>
		/// <param name="altText">Alternate display text.</param>
		/// <returns></returns>
		public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText)
		{
			return Image(helper, src, altText, new RouteValueDictionary());
		}

		/// <summary>
		/// Returns a self closed image element with the specified <paramref name="src"/> and alternate text.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="src">Source Url of the image. A relative value will be resolved.</param>
		/// <param name="altText">Alternate display text.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns></returns>
		public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, object htmlAttributes)
		{
			return Image(helper, src, altText, new RouteValueDictionary(htmlAttributes));
		}

		private static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, RouteValueDictionary htmlAttributes)
		{
			TagBuilder builder = new TagBuilder("img");
			UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

			builder.MergeAttribute("src", urlHelper.Content(src));
			if(htmlAttributes != null)
			{
				builder.MergeAttributes(htmlAttributes);
			}
			builder.MergeAttribute("alt", altText);
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
		}

	}
}
