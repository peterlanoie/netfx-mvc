using System.Web.Mvc;
using System.Web.Routing;

namespace Common.Mvc
{
	/// <summary>
	/// Defines helpers for the TagBuilder class.
	/// </summary>
	public static class TagBuilderHelper
	{
		/// <summary>
		/// Creates a TagBuilder instance using the optionally provided arguments.
		/// </summary>
		/// <param name="tagName"></param>
		/// <param name="options">Options that define the behavior of tag rendering.</param>
		/// <returns></returns>
		public static TagBuilder Make(string tagName, TagBuilderOptions options)
		{
			var tag = new TagBuilder(tagName);
			if(!string.IsNullOrEmpty(options.ElementId))
			{
				tag.Attributes["id"] = options.ElementId;
			}
			if(options.InnerText != null)
			{
				tag.SetInnerText(options.InnerText.ToString());
			}
			if (!string.IsNullOrEmpty(options.CssClasses))
			{
				tag.AddCssClass(options.CssClasses);
			}
			if(options.HtmlAttributes != null)
			{
				tag.MergeAttributes(new RouteValueDictionary(options.HtmlAttributes));
			}
			if (options.H5Data != null)
			{
				foreach (var h5DataItem in new RouteValueDictionary(options.H5Data))
				{
					tag.MergeAttribute(string.Format("data-{0}", h5DataItem.Key.Replace("data-", "")), h5DataItem.Value.ToString(), true);
				}
			}
			return tag;
		}
	}

	/// <summary>
	/// Defines various options that can be used when creating a TagBuilder
	/// </summary>
	public class TagBuilderOptions
	{
		/// <summary>
		/// The ID of the tag.
		/// </summary>
		public string ElementId { get; set; }

		/// <summary>
		/// One or more CSS classes to write to the tag.
		/// </summary>
		public string CssClasses { get; set; }

		/// <summary>
		/// The inner text of the element. Uses the value's ToString() method to get it.
		/// </summary>
		public object InnerText { get; set; }

		/// <summary>
		/// Additional arbitrary HTML attributes to write in the tag.
		/// </summary>
		public object HtmlAttributes { get; set; }

		/// <summary>
		/// Html5 Data items. (Omit the 'data-' prefix)
		/// </summary>
		public object H5Data { get; set; }
	}
}
