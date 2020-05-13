// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines MVC HTML helper methods for generating icons.
	/// (Mostly based on Font-Awesome gliphs.)
	/// </summary>
	public static class ExtHtmlHelper_Icons
	{

		/// <summary>
		/// Returns a fragment that renders the FontAwesome checkmark gliph if the given condition is true.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="condition">Condition that determines whether the fragment is returned or not.</param>
		/// <param name="modifiers">Additional FontAwesome modifiers to change appearance.</param>
		/// <returns></returns>
		public static MvcHtmlString FontAwesomeCheckIf(this HtmlHelper helper, bool? condition, string modifiers = null)
		{
			return (condition.HasValue && (bool)condition) ? helper.FontAwesomeIcon("fa-check-circle", modifiers) : MvcHtmlString.Empty;
		}

		/// <summary>
		/// Returns a fragment that renders the FontAwesome checkmark gliph.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="modifiers">Additional FontAwesome modifiers to change appearance.</param>
		/// <returns></returns>
		public static MvcHtmlString FontAwesomeCheck(this HtmlHelper helper, string modifiers = null)
		{
			return helper.FontAwesomeIcon("fa-check-circle", modifiers);
		}

		/// <summary>
		/// Returns a fragment that renders a FontAwesome gliph based on the FontAwesome class provided.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="faClass">FontAwesome gliph class.</param>
		/// <param name="modifiers">Additional FontAwesome modifiers to change appearance, separated by a space. Can be NULL.</param>
		/// <param name="id">Optional ID for the created element. Can be NULL.</param>
		/// <returns></returns>
		public static MvcHtmlString FontAwesomeIcon(this HtmlHelper helper, string faClass, string modifiers = null, string id = null)
		{
			return MvcHtmlString.Create(string.Format("<i class=\"fa {0} {1}\"{2}></i>", faClass, modifiers, id != null ? string.Format(" id=\"{0}\"", id) : ""));
		}
	}
}
