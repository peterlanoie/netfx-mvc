using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Common.Mvc
{
	/// <summary>
	/// Defines common functionality for MVC controllers.
	/// </summary>
	public abstract class ControllerBase : Controller
	{

		/// <summary>
		/// Generates a <see cref="JsonResult"/> containing a Json serialized instance
		/// of <see cref="JsonErrorResult"/> that includes details from the <paramref name="exception"/>.
		/// </summary>
		/// <param name="exception">An exception. Can be null.</param>
		/// <returns></returns>
		protected JsonResult JsonError(Exception exception)
		{
			return JsonError(null, exception);
		}

		/// <summary>
		/// Generates a <see cref="JsonResult"/> containing a Json serialized instance
		/// of <see cref="JsonErrorResult"/> that includes the <paramref name="message"/>.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <returns></returns>
		protected JsonResult JsonError(string message)
		{
			return JsonError(message, null);
		}

		/// <summary>
		/// Generates a <see cref="JsonResult"/> containing a Json serialized instance
		/// of <see cref="JsonErrorResult"/> that includes the <paramref name="message"/> and 
		/// details from the <paramref name="exception"/>.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="exception">An exception. Can be null.</param>
		/// <returns></returns>
		protected JsonResult JsonError(string message, Exception exception)
		{
			return Json(JsonErrorResult.Create(exception, message), JsonRequestBehavior.AllowGet);
		}

		/// <summary>
		/// Calls the supplied <paramref name="action"/> expecting a <see cref="JsonResult"/>.
		/// If the action throws an exception the method will return <see cref="JsonResult"/> 
		/// with an instance of <see cref="JsonErrorResult"/> for serialization
		/// back to javascript. Use this for consistent error handling in the client.
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		protected JsonResult AttemptJsonAction(JsonActionDelegate action)
		{
			try
			{
				return action();
			}
			catch(Exception ex)
			{
				return JsonError(ex);
			}
		}

	}

	/// <summary>
	/// Encapsulates a method that takes no arguments and returns a <see cref="ActionResult"/>.
	/// </summary>
	/// <returns><see cref="ActionResult"/></returns>
	public delegate ActionResult MvcActionDelegate();
	
	/// <summary>
	/// Encapsulates a method that takes no arguments and returns a <see cref="JsonResult"/>.
	/// </summary>
	/// <returns><see cref="JsonResult"/></returns>
	public delegate JsonResult JsonActionDelegate();

}
