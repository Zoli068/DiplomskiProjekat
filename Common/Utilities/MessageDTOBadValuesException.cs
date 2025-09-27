using System;

namespace Common.Utilities
{
    /// <summary>
    /// The exception that is thrown when inside the DTO class there is a bad value;
    /// </summary> 
    public class MessageDTOBadValuesException : Exception
    {
        /// <summary>
        /// The exception that is thrown when inside the DTO class there is a bad value;
        /// </summary>  
        public MessageDTOBadValuesException() : base("Value is out from the interval") { }

        /// <summary>
        /// The exception that is thrown when inside the DTO class there is a bad value;
        /// </summary> 
        public MessageDTOBadValuesException(string message) : base(message) { }
    }
}
