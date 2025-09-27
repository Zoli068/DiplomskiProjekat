using System;

namespace Common.CommunicationExceptions
{
    /// <summary>
    /// The exception that is thrown when the server can't listen for incoming connections
    /// </summary>
    public class ListeningNotSuccessedException : Exception
    {
        /// <summary>
        /// The exception that is thrown when the server can't listen for incoming connections
        /// </summary>
        public ListeningNotSuccessedException() : base("The server can't listening for incoming connections") { }

        /// <summary>
        /// The exception that is thrown when the server can't listen for incoming connections
        /// </summary>
        public ListeningNotSuccessedException(string message) : base(message) { }
    }
}
