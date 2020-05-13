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
	/// Defines extension methods that handle required page resources.
	/// Use these methods to specify JS and/or CSS resources that the page needs to load.
	/// You'll also need to include a single call to each "WriteRequired..." method in your 
	/// site's master layout to emit the resource elements to the page output.
	/// </summary>
	public static partial class ExtHtmlHelper
	{
		private const string KEY_CSSFILES = "Common.Mvc-requiredCssFiles";
		private const string KEY_JSFILES = "Common.Mvc-requiredJsFiles";
		private const string KEY_JSFRAGMENTS = "Common.Mvc-requiredJsFragments";

		/// <summary>
		/// Records a JS resource that is required for the current request context. Useful for when partials are widgets that require a JS resource are used several times on a single page.
		/// Use the WriteRequiredJSFiles helper to write them out once (usually in a master layout).
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="url"></param>
		/// <param name="defer"></param>
		/// <param name="uniquification"></param>
		public static MvcHtmlString RequireJS(this HtmlHelper helper, string url, bool defer = false, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			return AddRequires(helper, KEY_JSFILES, url, () => new JsFileResource(url, defer, uniquification));
		}

		/// <summary>
		/// Returns an MVC string with all the required JS file fragments.
		/// Clears the requires list to prevent duplicate emission.
		/// </summary>
		/// <returns></returns>
		public static MvcHtmlString WriteRequiredJSFiles(this HtmlHelper helper)
		{
			return WriteRequiredFiles(helper, KEY_JSFILES);
		}

		/// <summary>
		/// Records a JS resource that is required for the current request context. Useful for when partials are widgets that require a JS resource are used several times on a single page.
		/// Use the WriteRequiredJSFiles helper to write them out once (usually in a master layout).
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="url"></param>
		/// <param name="media">The media type of the CSS, if any. Can be NULL.</param>
		/// <param name="uniquification"></param>
		public static MvcHtmlString RequireCss(this HtmlHelper helper, string url, string media = null, UrlUniquificationType uniquification = UrlUniquificationType.Never)
		{
			return AddRequires(helper, KEY_CSSFILES, url, () => new CssFileResource(url, media, uniquification));
		}

		/// <summary>
		/// Returns an MVC string with all the required JS file element fragments.
		/// Clears the requires list to prevent duplicate emission.
		/// </summary>
		/// <returns></returns>
		public static MvcHtmlString WriteRequiredCssFiles(this HtmlHelper helper)
		{
			return WriteRequiredFiles(helper, KEY_CSSFILES);
		}

		/// <summary>
		/// Adds a required resource to the cached list if necessary.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="key"></param>
		/// <param name="url"></param>
		/// <param name="newFunc"></param>
		/// <returns></returns>
		private static MvcHtmlString AddRequires(this HtmlHelper helper, string key, string url, Func<BasePageResource> newFunc)
		{
			var requires = GetRequires(key, true);
			if(!requires.Any(x => x.Url == url))
			{
				// need to add it, call the create method.
				requires.Add(newFunc());
			}
			return MvcHtmlString.Empty;
		}

		private static MvcHtmlString WriteRequiredFiles(this HtmlHelper helper, string key)
		{
			var requires = GetRequires(key);
			if(requires == null)
			{
				return MvcHtmlString.Empty;
			}
			var result = new StringBuilder();
			foreach(var requiredFile in requires)
			{
				result.AppendLine(requiredFile.GetHtmlFragment());
			}
			requires.Clear();
			return MvcHtmlString.Create(result.ToString());
		}

		private static List<BasePageResource> GetRequires(string key, bool createIfMissing = false)
		{
			return GetRequires<BasePageResource>(key, createIfMissing);
		}

		private static List<TResource> GetRequires<TResource>(string key, bool createIfMissing = false)
		{
			return HttpContext.Current.GetContextItem<List<TResource>>(key, createIfMissing);
		}

		/// <summary>
		/// Adds a required JS fragment to the current request context for later emission to a view.
		/// Use this in reusable output to store fragments that don't need to be repeated themselves (e.g. JS initialization calls for page widgets).
		/// Must be used in conjunction with <see cref="WriteRequiredJsFragments"/>
		/// Duplicate fragments will not be repeated when written to page output using <see cref="WriteRequiredJsFragments"/>
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="jsFragment">The fragment to emit to the page.</param>
		/// <param name="defer">Whether the fragment uses deferred JS execution.</param>
		/// <param name="type">The script element type. (Default is 'text/javascript'.)</param>
		/// <returns></returns>
		public static MvcHtmlString RequireJsFragment(this HtmlHelper helper, string jsFragment, bool defer = false, string type = "text/javascript")
		{
			var requires = GetRequires<JsFragmentResource>(KEY_JSFRAGMENTS, true);
			if(!requires.Any(x => x.JsFragment == jsFragment))
			{
				// need to add it, call the create method.
				requires.Add(new JsFragmentResource
				{
					JsFragment = jsFragment,
					Defer = defer,
					Type = type
				});
			}
			return MvcHtmlString.Empty;
		}

		/// <summary>
		/// Returns an MVC string with all the required JS script block fragments.
		/// Clears the requires list to prevent duplicate emission.
		/// </summary>
		/// <returns></returns>
		public static MvcHtmlString WriteRequiredJsFragments(this HtmlHelper helper)
		{
			var requires = GetRequires<JsFragmentResource>(KEY_JSFRAGMENTS);
			if(requires == null)
			{
				return MvcHtmlString.Empty;
			}
			var result = new StringBuilder();
			foreach(var fragment in requires)
			{
				result.AppendLine(JavaScriptHelper.BuildJavaScriptFragment(null, fragment.JsFragment, fragment.Defer, fragment.Type));
			}
			requires.Clear();
			return MvcHtmlString.Create(result.ToString());
		}


	}

}
