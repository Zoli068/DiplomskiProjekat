using Common.Communication;
using Common.CommunicationExceptions;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Master.Communication
{
    /// <summary>
    /// The <see cref="TcpCommunicationStream"/> responsible for the TCP Communication
    /// </summary>
    public class TcpCommunicationStream : ICommunicationStream
    {
        private Stream stream;
        private TcpClient client;
        private readonly ITcpCommunicationOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpCommunicationStream"/> class
        /// </summary>
        /// <param name="options"><see cref="TcpCommunicationOptions"/> which holds all the option values</param>
        public TcpCommunicationStream(ICommunicationOptions options)
        {
            this.options = options as ITcpCommunicationOptions;
            client = new TcpClient();
        }

        /// <summary>
        /// Async Connect to the server
        /// </summary>
        /// <returns>Task object, which is representing the async Connect</returns>
        public async Task Connect()
        {
            Task connectAsyncTask = null;
            Task completedTask = null;

            if (client == null)
            {
                client = new TcpClient();
            }
            else
            {
                if (client.Connected)
                {
                    throw new ConnectionAlreadyExisting();
                }
            }

            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                connectAsyncTask = client.ConnectAsync(options.Address, options.PortNumber);
                cts.CancelAfter(options.TimeOut);
                completedTask = await Task.WhenAny(connectAsyncTask, Task.Delay(Timeout.Infinite, cts.Token));
            }
            catch (Exception)
            {
                Close();
                throw new UnsuccessfullConnectionException();
            }

            if (completedTask == null || connectAsyncTask == null || completedTask != connectAsyncTask || client == null || !client.Connected)
            {
                Close();
                throw new UnsuccessfullConnectionException();
            }
            else
            {
                stream = client.GetStream();
            }
        }

        /// <summary>
        /// Closing the communication stream
        /// </summary>
        public void Close()
        {
            if (stream != null)
            {
                stream.Close();
                stream = null;
            }

            if (client != null)
            {
                client.Close();
                client = null;
            }
        }

        /// <summary>
        /// Async reciving the bytes from the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte reciving</returns>
        public async Task<byte[]> Receive()
        {
            byte[] recvData = new byte[options.BufferSize];
            int readedBytes = 0;

            if (stream != null && client != null && client.Connected)
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

        /// <summary>
        /// Async sending the bytes to the server
        /// </summary>
        /// <returns>Task object, which is representing the async byte sending</returns>
        public async Task Send(byte[] data)
        {
            if (stream != null && client != null && client.Connected)
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

        /// <inheritdoc/>
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

                    if (client != null)
                    {
                        client.Close();
                    }
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
