using Common;
using System.Net;

namespace Master.Communication
{
    /// <summary>
    /// Contains all the values for a TCP Communicaiton
    /// </summary>
    public class TCPCommunicationOptions : ITCPCommunicationStreamOptions
    {
        private readonly int timeOut;
        private readonly int bufferSize;
        private readonly int portNumber;
        private readonly IPAddress address;
        private readonly CommunicationStreamType communicationStreamType;
        private readonly SecurityMode securityMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="TCPCommunicationOptions"/> class
        /// </summary>
        /// <param name="address">IP Address of the server</param>
        /// <param name="portNumber">Port number of the server</param>
        /// <param name="communicationType">Indicates the communication type which will be used</param>
        /// <param name="timeOut">Time period after which the current command will be interrupted</param>
        /// <param name="bufferSize">Size of the connection buffer in bytes</param>
        public TCPCommunicationOptions(IPAddress address, int portNumber, CommunicationStreamType communicationStreamType,SecurityMode securityMode, int timeOut, int bufferSize)
        {
            this.address = address;
            this.portNumber = portNumber;
            this.communicationStreamType = communicationStreamType;
            this.securityMode = securityMode;
            this.timeOut = timeOut;
            this.bufferSize = bufferSize;
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

        public int TimeOut
        {
            get
            {
                return timeOut;
            }
        }

        public int BufferSize
        {
            get
            {
                return bufferSize;
            }
        }

        public SecurityMode SecurityMode
        {
            get
            {
                return securityMode;
            }
        }
    }
}