using System;

namespace Common.PointsDataBase
{
    /// <summary>
    /// The exception that is thrown when 1 type of point got specified but at the specified adress is another type of point
    /// </summary>
    public class PointTypeDifferenceException : Exception
    {
        /// <summary>
        /// The exception that is thrown when 1 type of point got specified but at the specified adress is another type of point
        /// </summary>  
        public PointTypeDifferenceException() : base("Difference between the specified and the readl point type") { }

        /// <summary>
        /// The exception that is thrown when 1 type of point got specified but at the specified adress is another type of point
        /// </summary>
        public PointTypeDifferenceException(string message) : base(message) { }
    }

}
