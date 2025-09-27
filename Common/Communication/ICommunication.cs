using System;

namespace Common.Communication
{
    /// <summary>
    /// Describes all the methods that a Communication Class have to have
    /// </summary>
    public interface ICommunication
    {
        /// <summary>
        /// The event which will be raised when we recived bytes
        /// </summary>
        event Action<byte[]> BytesRecived;

        /// <summary>
        /// The method with we can send bytes 
        /// </summary>
        /// <param name="bytes"></param>
        void SendBytes(byte[] bytes);


        /// <summary>
        /// For proper freeing resources which used inside the communication
        /// </summary>
        void Dispose();
    }
}
