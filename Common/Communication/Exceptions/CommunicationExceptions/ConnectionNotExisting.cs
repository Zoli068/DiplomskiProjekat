using System;

namespace Common.CommunicationExceptions
{
    /// <summary>
    /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
    /// </summary>
    public class ConnectionNotExisting : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public ConnectionNotExisting() : base("There is no existing connection") { }

        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public ConnectionNotExisting(string message) : base(message) { }
    }
}
