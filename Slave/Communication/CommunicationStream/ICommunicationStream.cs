using System;
using System.IO;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// Describes all the required methods for a communication stream
    /// </summary>
    public interface ICommunicationStream : IDisposable
    {
        /// <summary>
        /// Async accepting an incoming connection
        /// </summary>
        /// <returns>Task object, which is representing the waiting task for an incoming connection</returns>
        Task Accept();

        /// <summary>
        /// Close the connection with the accepted client
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
