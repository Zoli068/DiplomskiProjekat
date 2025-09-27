using Common;
using Common.Communication;
using System;
using System.Net;

namespace Master.Communication
{
    /// <summary>
    /// Class which implements the <see cref="ICommunication"/> interface, and his job to make communication possible
    /// </summary>
    public class Communication : ICommunication
    {
        public event Action<byte[]> BytesRecived;
        private CommunicationHandler communicationHandler;

        public Communication()
        {
            //if we want to have configurable options, then static method for reading options
            //and based the readed values creating those objects

            ICommunicationOptions tcpCommunicationOptions = new TcpCommunicationOptions(IPAddress.Loopback, 502, CommunicationType.TCP, 2000, 8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000, SecurityMode.SECURE);

            communicationHandler = new CommunicationHandler(communicationHandlerOptions, tcpCommunicationOptions, RaiseBytesRecvied);
        }

        private void RaiseBytesRecvied(byte[] bytes)
        {
            if (BytesRecived != null)
            {
                BytesRecived.Invoke(bytes);
            }
        }

        public void SendBytes(byte[] bytes)
        {
            communicationHandler.dataToSend.Add(bytes);
        }

        public void Dispose()
        {
            communicationHandler.Dispose();
        }
    }
}
