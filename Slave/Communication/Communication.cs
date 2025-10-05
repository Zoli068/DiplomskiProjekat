using Common;
using Common.Communication;
using System;
using System.Net;

namespace Slave.Communication
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
            ICommunicationStreamOptions options = new TcpCommunicationOptions(IPAddress.Loopback, 502, CommunicationStreamType.TCP, 8192,0);
            ICommunicationHandlerOptions handlerOptions = new CommunicationHandlerOptions(SecurityMode.INSECURE, MessageType.TCPModbus);
            communicationHandler = new CommunicationHandler(handlerOptions, options, RaiseBytesRecvied);
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