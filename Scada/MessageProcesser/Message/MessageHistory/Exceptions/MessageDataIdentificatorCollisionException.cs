using System;

namespace Master.Message.MessageHistory
{
    /// <summary>
    /// The exception that is thrown when want to save a messageData but with the specified id there is a messagedata
    /// </summary>
    public class MessageDataIdentificatorCollisionException : Exception
    {
        /// <summary>
        /// The exception that is thrown when want to save a messageData but with the specified id there is a messagedata
        /// </summary>
        public MessageDataIdentificatorCollisionException() : base("Collision with the ID when trying to save the messageData") { }

        /// <summary>
        /// The exception that is thrown when want to save a messageData but with the specified id there is a messagedata
        /// </summary>
        public MessageDataIdentificatorCollisionException(string message) : base(message) { }
    }
}
