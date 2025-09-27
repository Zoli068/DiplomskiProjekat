using System;
using System.IO;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// Describes all the required methods and attributes for a communication stream
    /// </summary>
    public interface ICommunicationStream : IDisposable
    {
        /// <summary>
        /// Async Connect to the server
        /// </summary>
        /// <returns>Task object, which is representing the async Connect</returns>
        Task Connect();

        /// <summary>
        /// Closing the connection
        /// </summary>
        void Close();

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        Task Send(byte[] data);

        /// <summary>
        /// Async reciving the bytes from the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
        Task<byte[]> Receive();

        /// <summary>
        /// For accessing the <see cref="Stream"/> object
        /// </summary>
        Stream Stream { get; set; }
    }
}
