using Common.Mvc;
using System.IO;
using Common.Web.Helpers;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods that help create style tags for referenced resources or inline definitions.
	/// </summary>
	public static partial class ExtHtmlHelper
	{

		/// <summary>
		/// Generates a CSS link tag.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="href">Absolute or relative path to the CSS file. An app relative Href will be resolved.</param>
		/// <param name="uniquification">Type of uniquification.</param>
		/// <returns></returns>
		public static MvcHtmlString CssFile(this HtmlHelper helper, string href, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			return CssFile(helper, href, null, uniquification);
		}

		/// <summary>
		/// Generates a CSS link tag.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="href">Absolute or relative path to the CSS file. An app relative Href will be resolved.</param>
		/// <param name="media">Css file media type.</param>
		/// <param name="uniquification">Type of uniquification.</param>
		/// <returns></returns>
		public static MvcHtmlString CssFile(this HtmlHelper helper, string href, string media, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			if(string.IsNullOrEmpty(href))
			{
				throw new ArgumentNullException("href");
			}
			href = UrlUniquifier.UniquifyUrl(href, uniquification);
			return MvcHtmlString.Create(BuildCssTag(href, media).ToString(TagRenderMode.SelfClosing));
		}

		/// <summary>
		/// Writes out an opening style tag for inline Css. Can be used within a using() block for auto-close closure.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <returns></returns>
		public static TagBlock BeginCssBlock(this HtmlHelper helper)
		{
			return BeginCssBlock(helper, null);
		}

		/// <summary>
		/// Writes out an opening style tag for inline Css. Can be used within a using() block for auto-close closure.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="media">Css media type.</param>
		/// <returns></returns>
		public static TagBlock BeginCssBlock(this HtmlHelper helper, string media)
		{
			var tag = new TagBlock(helper.ViewContext, "style");
			tag.AddAttribute("type", "text/css");
			if(media != null)
			{
				tag.AddAttribute("media", media);
			}
			tag.AddAttribute("type", "text/css");
			tag.OpenBlock();
			return tag;
		}

		/// <summary>
		/// Writes out the closing style tag.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		public static void EndCssBlock(this HtmlHelper helper)
		{
			helper.ViewContext.Writer.Write("</style>");
		}

		/// <summary>
		/// Writes the contents of the specified <paramref name="cssFilePath"/> into an inline CSS style element.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="cssFilePath">Fully qualified local file path to a css file.</param>
		public static MvcHtmlString CssBlockFromFile(this HtmlHelper helper, string cssFilePath)
		{
			var builder = new TagBuilder("style");
			builder.MergeAttribute("type", "text/css");
			builder.MergeAttribute("rel", "stylesheet");
			builder.InnerHtml = File.ReadAllText(cssFilePath);
			return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
		}

		internal static TagBuilder BuildCssTag(string href, string media)
		{
			var builder = new TagBuilder("link");
			if(!string.IsNullOrEmpty(href))
			{
#warning This uses a faulty method for resolving a URI. It should be changed to use the method in JavaScriptHelper.BuildJavaScriptTag.
				builder.MergeAttribute("href", !Uri.IsWellFormedUriString(href, UriKind.Absolute) ? VirtualPathUtility.ToAbsolute(href) : href);
			}
			builder.MergeAttribute("rel", "stylesheet");
			builder.MergeAttribute("type", "text/css");
			if(media != null)
			{
				builder.MergeAttribute("media", media);
			}
			return builder;
		}

	}
}
