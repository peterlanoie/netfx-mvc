using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extention methods for a ModelStateDictionary
	/// </summary>
	public static class ExtModelStateDictionary
	{

		/// <summary>
		/// Removes all the elements except those with keys matching keys in the <paramref name="includes"/> list.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="includes"></param>
		public static void RemoveAllBut(this ModelStateDictionary modelState, params string[] includes)
		{
			modelState.Where(x => !includes.Any(i => String.Compare(i, x.Key, true) == 0)).ToList().ForEach(k => modelState.Remove(k));
		}

		/// <summary>
		/// Removes all the elements with keys in the <paramref name="keys"/> list.
		/// </summary>
		/// <param name="modelState"></param>
		/// <param name="keys"></param>
		public static void Remove(this ModelStateDictionary modelState, params string[] keys)
		{
			keys.ToList().ForEach(k => modelState.Remove(k));
		}

	}
}
