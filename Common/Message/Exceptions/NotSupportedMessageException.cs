using System;

namespace Common.Message
{
    /// <summary>
    /// The exception that is thrown when can't parse a supported message from the bytes
    /// </summary>
    public class NotSupportedMessageException : Exception
    {
        /// <summary>
        /// The exception that is thrown when can't parse a supported message from the bytes
        /// </summary>
        public NotSupportedMessageException() : base("From the bytes can't parse a supported message") { }

        /// <summary>
        /// The exception that is thrown when can't parse a supported message from the bytes
        /// </summary>
        public NotSupportedMessageException(string message) : base(message) { }
    }
}
