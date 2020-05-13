using Common.Mvc;
using Common.Web.Helpers;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	internal abstract class BasePageResource
	{
		protected BasePageResource(string url, UrlUniquificationType uniquification)
		{
			Url = url;
			Uniquification = uniquification;
		}

		public string Url { get; private set; }
		protected UrlUniquificationType Uniquification { get; private set; }

		public abstract string GetHtmlFragment();
	}

	internal class CssFileResource : BasePageResource
	{
		private readonly string _media;

		public CssFileResource(string url, string media, UrlUniquificationType uniquification)
			: base(url, uniquification)
		{
			_media = media;
		}

		public override string GetHtmlFragment()
		{
			return ExtHtmlHelper.BuildCssTag(UrlUniquifier.UniquifyUrl(Url, Uniquification), _media).ToString(TagRenderMode.Normal);
		}
	}

	internal class JsFileResource : BasePageResource
	{
		private readonly bool _defer;

		public JsFileResource(string url, bool defer, UrlUniquificationType uniquification)
			: base(url, uniquification)
		{
			_defer = defer;
		}

		public override string GetHtmlFragment()
		{
			return JavaScriptHelper.BuildJavaScriptTag(UrlUniquifier.UniquifyUrl(Url, Uniquification), null, _defer).ToString(TagRenderMode.Normal);
		}
	}

	internal class JsTemplateFileResource : JsFileResource
	{
		public JsTemplateFileResource(string url, string id, UrlUniquificationType uniquification)
			: base(url, false, uniquification)
		{
		}
	}

	internal class JsFragmentResource
	{
		public string Key { get; set; }
		public string JsFragment { get; set; }
		public bool Defer { get; set; }
		public string Type { get; set; }
	}

}