using Common;
using System.Net;

namespace Slave.Communication
{
    /// <summary>
    /// Contains all the values for a TCP Communicaiton
    /// </summary>
    public class TcpCommunicationOptions : ITcpCommunicationOptions
    {
        private readonly int portNumber;
        private readonly int bufferSize;
        private readonly IPAddress address;
        private readonly CommunicationType communicationType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationOptions"/> class
        /// </summary>
        /// <param name="address">IP Address of the server</param>
        /// <param name="portNumber">Port number of the server</param>
        /// <param name="communicationType">Indicates the communication type which will be used</param>
        /// <param name="bufferSize">Size of the connection buffer in bytes</param>
        public TcpCommunicationOptions(IPAddress address, int portNumber, CommunicationType communicationType, int bufferSize)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationType = communicationType;
            this.bufferSize = bufferSize;
        }

        public IPAddress Address
        {
            get
            {
                return address;
            }
        }

        public CommunicationType CommunicationType
        {
            get
            {
                return communicationType;
            }
        }

        public int PortNumber
        {
            get
            {
                return portNumber;
            }
        }

        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
        }
    }
}
