using System;

namespace Common.PointsDataBase
{
    /// <summary>
    /// The exception that is thrown when trying access a point but in the specified address doesn't exists
    /// </summary>
    public class InvalidAddressException : Exception
    {
        /// <summary>
        /// The exception that is thrown when trying access a point but in the specified address doesn't exists
        /// </summary>  
        public InvalidAddressException() : base("There is no point on that address") { }

        /// <summary>
        /// The exception that is thrown when trying access a point but in the specified address doesn't exists
        /// </summary>
        public InvalidAddressException(string message) : base(message) { }
    }
}
