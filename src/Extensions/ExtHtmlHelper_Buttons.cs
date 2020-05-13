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
		/// Generates a standard A tag with a NOOP href, the specified <paramref name="id"/> and <paramref name="text"/>.
		/// Useful for building linkbuttons that will be wired to client side actions.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="text">Link display text.</param>
		/// <param name="id">ID of the link.</param>
		/// <param name="cssClass">ID of the link.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns></returns>
		public static MvcHtmlString LinkButton(this HtmlHelper helper, string text, string id = null, string cssClass = null, object htmlAttributes = null)
		{
			var attribs = new RouteValueDictionary(htmlAttributes);
			if(!string.IsNullOrEmpty(id)) attribs.Add("id", id);
			if (!string.IsNullOrEmpty(cssClass))
			{
				attribs["class"] = cssClass;
			}
			return helper.MakeHyperlink("javascript:void(0);", text, attribs);
		}

		/// <summary>
		/// Generates a standard INPUT:BUTTON tag with the specified <paramref name="text"/>.
		/// Useful for building buttons that will be wired to client side actions.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="text">Button display text.</param>
		/// <returns></returns>
		public static MvcHtmlString InputButton(this HtmlHelper helper, string text)
		{
			helper.LinkButton("");
			return InputButton(helper, null, text, null);
		}

		/// <summary>
		/// Generates a standard INPUT:BUTTON tag with the specified <paramref name="id"/> and <paramref name="text"/>.
		/// Useful for building buttons that will be wired to client side actions.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="id">ID of the button.</param>
		/// <param name="text">Button display text.</param>
		/// <returns></returns>
		public static MvcHtmlString InputButton(this HtmlHelper helper, string id, string text)
		{
			return InputButton(helper, id, text, null);
		}

		/// <summary>
		/// Generates a standard INPUT:BUTTON tag with the specified <paramref name="id"/> and <paramref name="text"/>.
		/// Useful for building buttons that will be wired to client side actions.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="id">ID of the button.</param>
		/// <param name="text">Button display text.</param>
		/// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
		/// <returns></returns>
		public static MvcHtmlString InputButton(this HtmlHelper helper, string id, string text, object htmlAttributes)
		{
			RouteValueDictionary attribs = new RouteValueDictionary(htmlAttributes);
			TagBuilder builder = new TagBuilder("input");
			builder.Attributes.Add("type", "button");
			builder.Attributes.Add("value", text);
			if(!string.IsNullOrEmpty(id)) builder.Attributes.Add("id", id);
			if(htmlAttributes != null)
			{
				builder.MergeAttributes(attribs);
			}
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
		}


	}
}
