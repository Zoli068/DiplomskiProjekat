using System;

namespace Common.CommunicationExceptions
{
    /// <summary>
    /// The exception that is thrown when trying to use a connection, but an error happened
    /// </summary>
    public class ConnectionErrorException : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to use a connection, but an error happened
        /// </summary>
        public ConnectionErrorException() : base("An error happened while using the connection") { }

        /// <summary>
        /// The exception that is thrown when trying to use a connection, but an error happened
        /// </summary>
        public ConnectionErrorException(string message) : base(message) { }
    }
}
