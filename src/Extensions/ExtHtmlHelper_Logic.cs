// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for handling simple logic conditions.
	/// </summary>
	public static partial class ExtHtmlHelper
	{

		/// <summary>
		/// Generates a class attribute string with class <paramref name="className"/> if <paramref name="condition"/> is true, 
		/// otherwise returns an empty string.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="className">The class name to emit in the attribute.</param>
		/// <param name="condition">The condition to test that determines whether or not the attribute is emitted.</param>
		/// <returns></returns>
		public static MvcHtmlString ClassAttribIf(this HtmlHelper helper, bool condition, string className)
		{
			return AttribIf(helper, condition, "class", className);
		}


		/// <summary>
		/// Generates an HTML attribute string with the specified <paramref name="attributeName"/> and <paramref name="value"/> if <paramref name="condition"/> is true, 
		/// otherwise returns an empty string.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">The condition to test that determines whether or not the attribute is emitted.</param>
		/// <param name="attributeName">The name of the attribute.</param>
		/// <param name="value">The attribute value to emit.</param>
		/// <returns></returns>
		public static MvcHtmlString AttribIf(this HtmlHelper helper, bool condition, string attributeName, string value)
		{
			return If(helper, condition, string.Format("{0}=\"{1}\"", attributeName, value));
		}


		/// <summary>
		/// Returns the <paramref name="value"/> if <paramref name="condition"/> is true, 
		/// otherwise returns an empty MvcHtmlString.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">The condition to test that determines whether or not the value is emitted.</param>
		/// <param name="value">The value to return.</param>
		/// <returns></returns>
		public static MvcHtmlString If(this HtmlHelper helper, bool? condition, MvcHtmlString value)
		{
			return (condition.HasValue && condition.Value) ? value : MvcHtmlString.Empty;
		}

		/// <summary>
		/// Returns the string representation of the <paramref name="value"/> object if <paramref name="condition"/> is true, 
		/// otherwise returns an empty string.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">The condition to test that determines whether or not the value is emitted.</param>
		/// <param name="value">The value to return.</param>
		/// <returns></returns>
		public static MvcHtmlString If(this HtmlHelper helper, bool? condition, object value)
		{
			return (condition.HasValue && condition.Value) ? MvcHtmlString.Create(value != null ? value.ToString() : "") : MvcHtmlString.Empty;
		}

		/// <summary>
		/// Returns the result of the <paramref name="trueAction"/> when <paramref name="condition"/> is true,
		/// otherwise returns an empty string.  The <paramref name="trueAction"/> is not evaluated when <paramref name="condition"/> is false.
		/// </summary>
		/// <param name="helper">The HTML helper instance that this method extends.</param>
		/// <param name="condition">Condition to test.</param>
		/// <param name="trueAction">Action to execute when <paramref name="condition"/> is true.</param>
		/// <returns></returns>
		public static MvcHtmlString If(this HtmlHelper helper, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction)
		{
			return helper.IfElse(condition, trueAction, (h) => MvcHtmlString.Empty);
		}

		/// <summary>
		/// Returns the <paramref name="trueResult"/> if <paramref name="condition"/> is true, 
		/// otherwise returns <paramref name="falseResult"/>.
		/// </summary>
		/// <param name="helper">The helper instance extended.</param>
		/// <param name="condition">Condition to test.</param>
		/// <param name="trueResult">Result when condition is true.</param>
		/// <param name="falseResult">Result when condition is false.</param>
		/// <returns></returns>
		public static MvcHtmlString IfElse(this HtmlHelper helper, bool condition, MvcHtmlString trueResult, MvcHtmlString falseResult)
		{
			return condition ? trueResult : falseResult;
		}

		/// <summary>
		/// Returns the .ToString() of <paramref name="trueResult"/> if <paramref name="condition"/> is true, 
		/// otherwise returns the .ToString() of <paramref name="falseResult"/>.
		/// </summary>
		/// <param name="helper">The helper instance extended.</param>
		/// <param name="condition">Condition to test.</param>
		/// <param name="trueResult">Result when condition is true.</param>
		/// <param name="falseResult">Result when condition is false.</param>
		/// <returns></returns>
		public static MvcHtmlString IfElse(this HtmlHelper helper, bool condition, object trueResult, object falseResult)
		{
			return MvcHtmlString.Create(condition ? trueResult.ToString() : falseResult.ToString());
		}

		/// <summary>
		/// Returns the result of the <paramref name="trueAction"/> when <paramref name="condition"/> is true,
		/// or <paramref name="falseAction"/> when false.
		/// Only the appropriate action is evaluated.
		/// </summary>
		/// <param name="helper">The helper instance extended.</param>
		/// <param name="condition">Condition to test.</param>
		/// <param name="trueAction">Action to execute when <paramref name="condition"/> is true.</param>
		/// <param name="falseAction">Action to execute when <paramref name="condition"/> is false.</param>
		/// <returns></returns>
		public static MvcHtmlString IfElse(this HtmlHelper helper, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction, Func<HtmlHelper, MvcHtmlString> falseAction)
		{
			return condition ? trueAction.Invoke(helper) : falseAction.Invoke(helper);
		}

		/// <summary>
		/// Returns whether or not the provided key has already been used during response composition.
		/// Typically, this is used to ensure a given logic block only executes once, for example to emit an HTML fragment only once.
		/// Any call with the same key during the scope of a single request will return false.
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="key">The key to check/save.</param>
		/// <returns></returns>
		public static bool KeyUsed(this HtmlHelper helper, string key)
		{
			var list = HttpContext.Current.GetContextItem<List<string>>("Common.MVC-usageKeys");
			if(list.Contains(key))
			{
				return false;
			}
			list.Add(key);
			return true;
		}

	}
}
