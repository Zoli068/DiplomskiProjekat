using System;

namespace Common.Utilities
{
    /// <summary>
    /// The exception that is thrown when an we got an error in the process of serialization/deserialization
    /// </summary>
    public class SerializationException : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public SerializationException() : base("Something went wrong in the serialization process") { }

        /// <summary>
        /// The exception that is thrown when trying to use a connection, but that connection doesn't exists
        /// </summary>
        public SerializationException(string message) : base(message) { }
    }
}
