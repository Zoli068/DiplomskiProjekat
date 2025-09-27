namespace Common.Communication
{
    /// <summary>
    /// Interface which contains the important values for a communication stream
    /// </summary>
    public interface ICommunicationStreamOptions
    {
        /// <summary>
        /// Indicates the communication type which will be used.
        /// </summary>
        CommunicationStreamType CommunicationStreamType { get; }

        /// <summary>
        /// Time period after which the current command will be interrupted
        /// </summary>
        int TimeOut { get; }

        /// <summary>
        /// Size of the connection buffer in bytes
        /// </summary>
        int BufferSize { get; }
    }
}