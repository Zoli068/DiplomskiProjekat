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
        private readonly CommunicationStreamType communicationStreamType;
        private readonly int timeOut;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationOptions"/> class
        /// </summary>
        /// <param name="address">IP Address of the server</param>
        /// <param name="portNumber">Port number of the server</param>
        /// <param name="communicationType">Indicates the communication type which will be used</param>
        /// <param name="bufferSize">Size of the connection buffer in bytes</param>
        public TcpCommunicationOptions(IPAddress address, int portNumber, CommunicationStreamType communicationStreamType, int bufferSize,int timeOut)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationStreamType = communicationStreamType;
            this.bufferSize = bufferSize;
            this.timeOut = timeOut;
        }

        public IPAddress Address
        {
            get
            {
                return address;
            }
        }

        public CommunicationStreamType CommunicationStreamType
        {
            get
            {
                return communicationStreamType;
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

        public int TimeOut
        {
            get
            {
                return timeOut;
            }
        }
    }
}