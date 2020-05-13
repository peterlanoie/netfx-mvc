using System.Collections.Generic;
using System.Linq;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc.Html
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Providers methods for constructing SELECT elements
	/// </summary>
	public static partial class ExtHtmlHelper
	{
		/// <summary>
		/// Creates a standard dropdown list from a string list
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="name"></param>
		/// <param name="list"></param>
		/// <param name="selectedValue"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static MvcHtmlString DropDownListFromStringList(this HtmlHelper helper, string name, IEnumerable<string> list, string selectedValue = null, object htmlAttributes = null)
		{
			return DropDownListFromCustomList(helper, name, list, s => s, s => s, selectedValue, htmlAttributes);
		}

		/// <summary>
		/// Creates a standard dropdown list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="helper"></param>
		/// <param name="name"></param>
		/// <param name="list"></param>
		/// <param name="labelProvider"></param>
		/// <param name="valueProvider"></param>
		/// <param name="selectedValue"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		public static MvcHtmlString DropDownListFromCustomList<T>(this HtmlHelper helper, string name, IEnumerable<T> list, Func<T, string> labelProvider, Func<T, string> valueProvider, string selectedValue = null, object htmlAttributes = null)
		{
			var selectList = list.Select(listItem => new SelectListItem()
			{
				Text = labelProvider(listItem),
				Value = valueProvider(listItem),
				Selected = selectedValue != null && valueProvider(listItem) == selectedValue
			}).ToList();

			return helper.DropDownList(name, selectList, htmlAttributes);
		}

	}
}
