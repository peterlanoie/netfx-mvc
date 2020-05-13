using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Mvc
{

	/// <summary>
	/// Defines the return data for a Json action that resulted in an error and/or exception.
	/// </summary>
	public class JsonErrorResult : JsonMessageResult
	{
		/// <summary>
		/// Whether or not the result is due to an error.
		/// Generally, this will always be true, otherwise you wouldn't
		/// be seeing this object.
		/// </summary>
		public bool IsServerError { get { return true; } }

		/// <summary>
		/// The name of the exception.
		/// </summary>
		public string Exception { get; set; }

		/// <summary>
		/// The exception's message.
		/// </summary>
		public string ExceptionMessage { get; set; }

		/// <summary>
		/// The exception's stack trace.
		/// </summary>
		public string StackTrace { get; set; }

		/// <summary>
		/// Creates a new <see cref="JsonErrorResult"/> instance containing details from the provided <paramref name="exception"/> and <paramref name="message"/>.
		/// </summary>
		/// <param name="exception">The exception that occurred.</param>
		/// <param name="message">An error message to include in the result. Can be null.</param>
		public static JsonErrorResult Create(Exception exception, string message)
		{
			Exception objExc = exception;
			string strJsonMessage = message;
			string strExcMessage = null;
			JsonErrorResult result = new JsonErrorResult();

			if(objExc != null)
			{
				// construct the exception message as a concatenated string of all inner exceptions
				strExcMessage = objExc.Message;
				while(objExc.InnerException != null)
				{
					objExc = objExc.InnerException;
					strExcMessage = string.Format("{0}: {1}", strExcMessage, objExc.Message);
				}
			}

			// if an explicit error message hasn't been defined
			if(strJsonMessage == null)
			{
				// use the exception message(s) as the error message
				strJsonMessage = strExcMessage;
			}

			// construct the JSON error result package
			if(objExc != null)
			{
				result.Exception = exception.GetType().ToString();
				result.ExceptionMessage = strExcMessage;
				result.StackTrace = exception.StackTrace;
			}
			result.Message = strJsonMessage;

			return result;
		}
	}
}
