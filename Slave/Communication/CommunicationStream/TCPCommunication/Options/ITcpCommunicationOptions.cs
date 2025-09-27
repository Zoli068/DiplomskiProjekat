using Common.Communication;
using System.Net;

namespace Slave.Communication
{
    /// <summary>
    /// Interface which describes the important values for TCP communication
    /// </summary>
    public interface ITcpCommunicationOptions : ICommunicationOptions
    {
        /// <summary>
        /// IP Address of the server
        /// </summary>
        IPAddress Address { get; }

        /// <summary>
        /// Port number of the server
        /// </summary>
        int PortNumber { get; }

        /// <summary>
        /// Size of the connection buffer in bytes
        /// </summary>
        int BufferSize { get; }

    }
}
