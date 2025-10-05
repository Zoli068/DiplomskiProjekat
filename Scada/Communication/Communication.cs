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
        public CommunicationHandler communicationHandler;

        public Communication()
        {
            ICommunicationStreamOptions tcpCommunicationStreamOptions = new TCPCommunicationOptions(IPAddress.Loopback, 502, CommunicationStreamType.TCP,SecurityMode.INSECURE, 2000, 8192);
            ICommunicationHandlerOptions communicationHandlerOptions = new CommunicationHandlerOptions(20000);

            communicationHandler = new CommunicationHandler(communicationHandlerOptions, tcpCommunicationStreamOptions, RaiseBytesRecvied);
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