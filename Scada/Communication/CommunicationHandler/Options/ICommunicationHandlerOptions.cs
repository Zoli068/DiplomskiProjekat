namespace Master.Communication
{
    /// <summary>
    /// Describes all the must have values for the <see cref="CommunicationHandler"/> class
    /// </summary>
    public interface ICommunicationHandlerOptions
    {
        /// <summary>
        /// The interval between two connection attempts. If the value is set to 0, no reconnect attempt will be made after a failed connection.
        /// </summary>
        int ReconnectInterval { get; }
    }
}