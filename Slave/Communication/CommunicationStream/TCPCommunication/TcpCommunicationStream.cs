using Common.Communication;
using Common.CommunicationExceptions;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Slave.Communication
{
    /// <summary>
    /// The <see cref="TcpCommunicationStream"/> responsible for the TCP Communication
    /// </summary>
    public class TcpCommunicationStream : ICommunicationStream
    {
        private Stream stream;
        private TcpClient tcpClient;
        private TcpListener tcpListener;
        private readonly ITcpCommunicationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationStream"/> class
        /// </summary>
        /// <param name="options"><see cref="TcpCommunicationOptions"/> which holds all the option values</param
        public TcpCommunicationStream(ICommunicationOptions communicationOwptions)
        {
            options = communicationOwptions as ITcpCommunicationOptions;
            IPEndPoint endPoint = new IPEndPoint(options.Address, options.PortNumber);

            try
            {
                tcpListener = new TcpListener(endPoint);
                tcpListener.Start(2);
            }
            catch (Exception)
            {
                throw new ListeningNotSuccessedException();
            }
        }

        /// <summary>
        /// Async Listening for connection
        /// </summary>
        /// <returns>Task object, which is representing the async listening</returns>
        public async Task Accept()
        {
            //TODO in the future, if we wan't to drop the old communication when we got a new one, the we have to change
            //the definitions 
            if (tcpClient == null || !tcpClient.Connected)
            {
                try
                {
                    tcpClient = await tcpListener.AcceptTcpClientAsync();
                    stream = tcpClient.GetStream();
                }
                catch (Exception)
                {
                    throw new UnsuccessfullConnectionException();
                }
            }
        }

        /// <summary>
        /// Close the connection with the accepted client
        /// </summary>
        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }
        }

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        public async Task Send(byte[] data)
        {
            if (stream != null && tcpClient != null && tcpClient.Connected)
            {
                try
                {
                    await stream.WriteAsync(data, 0, data.Count());
                }
                catch (Exception)
                {
                    throw new ConnectionErrorException();
                }
            }
            else
            {
                throw new ConnectionNotExisting();
            }
        }

        /// <summary>
        /// Async reciving the bytes from the client
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
        public async Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && tcpClient != null && tcpClient.Connected)
            {
                try
                {
                    readedBytes = await stream.ReadAsync(recvData, 0, options.BufferSize);

                }
                catch (Exception)
                {
                    throw new ConnectionErrorException();
                }
            }
            else
            {
                throw new ConnectionNotExisting();
            }

            byte[] data = new byte[readedBytes];

            if (readedBytes > 0)
            {
                Array.Copy(recvData, data, readedBytes);
            }
            else
            {
                throw new ConnectionNotExisting();
            }

            return data;
        }

        public Stream Stream
        {
            get
            {
                return stream;
            }

            set
            {
                stream = value;
            }
        }

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }

                    if (tcpClient != null)
                    {
                        tcpClient.Close();
                    }

                    if (tcpListener != null)
                    {
                        //important to do bcs after a Slave restart, otherwise the socket would be still in use
                        tcpListener.Stop();
                    }
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
