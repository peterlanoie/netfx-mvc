using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Common.Mvc
{
    /// <summary>
    /// Create a Dynamic RedirectResults Object with a custom Status Code for the redirect
    /// </summary>
    public class DynamicRedirectResult : RedirectResult
    {
        private int _statusCode = 302;

        /// <summary>
        /// Custom Redirect Status Code
        /// </summary>
        public int StatusCode
        {
            get { return _statusCode; }
            set { _statusCode = value; }
        }

        /// <summary>
        /// Standard URL and Status Code Constructor
        /// </summary>
        /// <param name="url">Redirect URL</param>
        /// <param name="statusCode">Redirect Status Code</param>
        public DynamicRedirectResult(string url, int statusCode)
            : base(url)
        {
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// Execute the RedirectResult with the custom Status Code passed
        /// </summary>
        /// <param name="context">Standard MVC Context</param>
        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = StatusCode;
        }
    }
}
