using Common;
using Common.Communication;
using System.Net;

namespace Master.Communication
{
    /// <summary>
    /// Interface which describes the important values for TCP communication
    /// </summary>
    public interface ITCPCommunicationStreamOptions : ICommunicationStreamOptions
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
        /// Security mode of the tcp stream
        /// </summary>
        SecurityMode SecurityMode { get; }
    }
}