using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

// ReSharper disable CheckNamespace
namespace System.Web
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for HttpContextBase
	/// </summary>
	public static class ExtHttpContextBase
	{
		/// <summary>
		/// Helper for getting items that are cached across the request scope.
		/// Retrieves an item for the key from the HTTP request items collection.
		/// If item isn't present, the createAction is called to get it and it's added to the collection for future retrieval.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="key"></param>
		/// <param name="createAction"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetItem<T>(this HttpContextBase context, string key, Func<T> createAction) where T : class
		{
			var item = context.Items[key] as T;
			if(item == null)
			{
				context.Items[key] = (item = createAction());
			}
			return item;
		}
	}
}
