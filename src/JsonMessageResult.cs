using System;

namespace Common.Mvc
{
	/// <summary>
	/// Defines a basic JSON result with a message.
	/// </summary>
	public class JsonMessageResult
	{
		/// <summary>
		/// A custom error message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Create a new instance with default member values.
		/// </summary>
		protected JsonMessageResult()
		{
		}

		/// <summary>
		/// Create a new instance with the provided message.
		/// </summary>
		/// <param name="message"></param>
		protected JsonMessageResult(string message)
		{
			Message = message;
		}

		/// <summary>
		/// Creates a new <see cref="JsonMessageResult"/> instance containing a <paramref name="message"/>.
		/// </summary>
		/// <param name="message">A message to include in the result. Can be null.</param>
		public static JsonMessageResult Create(string message)
		{
			return new JsonMessageResult(message);
		}
	}
}