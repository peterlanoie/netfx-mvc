using System.Web.Routing;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for generating html form elements.
	/// </summary>
	public static partial class ExtHtmlHelper
	{

		/// <summary>
		/// Returns an html submit button with the specified text.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="text">Display text of the submit button.</param>
		/// <returns></returns>
		public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text)
		{
			return SubmitButton(helper, text, null, null);
		}

		/// <summary>
		/// Returns an html submit button with the specified id and text.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="text">Display text of the submit button.</param>
		/// <param name="id">Client-side element id and name of the submit button.</param>
		/// <returns></returns>
		public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text, string id)
		{
			return SubmitButton(helper, text, id, new { name = id });
		}

		/// <summary>
		/// Returns an html submit button with the specified id, text and attributes.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="text">Display text of the submit button.</param>
		/// <param name="id">Client-side element id and name of the submit button.</param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static MvcHtmlString SubmitButton(this HtmlHelper helper, string text, string id, object htmlAttributes)
		{
			TagBuilder builder = new TagBuilder("input");

			builder.MergeAttribute("type", "submit");
			if(!string.IsNullOrEmpty(id))
			{
				builder.MergeAttribute("id", id);
				builder.MergeAttribute("name", id);
			}
			if(!string.IsNullOrEmpty(text))
			{
				builder.MergeAttribute("value", text);
			}
			if(htmlAttributes != null)
			{
				builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			}
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
		}

	}
}
