using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

// ReSharper disable CheckNamespace
namespace System.Web.Mvc
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Defines extension methods for an MVC controller.
	/// <remarks>
	/// Adopted from http://learningdds.com/public/ControllerExtension.cs
	/// </remarks>
	/// </summary>
	public static class ExtController
	{

		/// <summary>
		/// Renders a view to a string.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <param name="masterName"></param>
		/// <returns></returns>
		public static string RenderViewToString(this Controller controller, string viewName = null, object model = null, string masterName = null)
		{
			return RenderPartialViewToString(controller, (c, v) => ViewEngines.Engines.FindView(c.ControllerContext, v, masterName), viewName, model);
		}

		/// <summary>
		/// Renders a view as a partial to a string using defaults.
		/// </summary>
		/// <param name="controller"></param>
		/// <returns></returns>
		public static string RenderPartialViewToString(this Controller controller)
		{
			return controller.RenderPartialViewToString(null, null);
		}

		/// <summary>
		/// Renders the specified view as a partial to a string.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="viewName"></param>
		/// <returns></returns>
		public static string RenderPartialViewToString(this Controller controller, string viewName)
		{
			return controller.RenderPartialViewToString(viewName, null);
		}

		/// <summary>
		/// Renders a partial view to a string using the specified model.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public static string RenderPartialViewToString(this Controller controller, object model)
		{
			return controller.RenderPartialViewToString(null, model);
		}

		/// <summary>
		/// Renders a view as a partial to a string using the specified view and model.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="viewName"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		public static string RenderPartialViewToString(this Controller controller, string viewName = null, object model = null)
		{
			return RenderPartialViewToString(controller, (c, v) => ViewEngines.Engines.FindPartialView(c.ControllerContext, v), viewName, model);
		}

		private static string RenderPartialViewToString(Controller controller, Func<Controller, string, ViewEngineResult> viewFinder, string viewName = null, object model = null)
		{
			if(string.IsNullOrEmpty(viewName))
			{
				viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
			}

			controller.ViewData.Model = model;

			using(StringWriter sw = new StringWriter())
			{
				ViewEngineResult viewResult = viewFinder(controller, viewName);
				ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
				viewResult.View.Render(viewContext, sw);
				return sw.GetStringBuilder().ToString();
			}
		}

		/// <summary>
		/// Returns the <paramref name="ajaxResult"/> if the call is being made by an AJAX request, otherwise returns <paramref name="nonAjaxResult"/>.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="ajaxResult">The result for an AJAX request.</param>
		/// <param name="nonAjaxResult">The alternate result.</param>
		/// <returns></returns>
		public static ActionResult IsAjax(this Controller controller, ActionResult ajaxResult, ActionResult nonAjaxResult)
		{
			return controller.ControllerContext.HttpContext.Request.IsAjaxRequest() ? ajaxResult : nonAjaxResult;
		}

	}
}
