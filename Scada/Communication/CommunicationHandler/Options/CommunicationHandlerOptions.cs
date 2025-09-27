namespace Master.Communication
{
    /// <summary>
    /// Contains all the option values for a <see cref="Master.CommunicationHandler"/> class
    /// </summary>
    public class CommunicationHandlerOptions : ICommunicationHandlerOptions
    {
        private readonly int reconnectInterval;

        public CommunicationHandlerOptions(int reconnectInterval)
        {
            this.reconnectInterval = reconnectInterval;
        }

        public int ReconnectInterval
        {
            get
            {
                return reconnectInterval;
            }
        }
    }
}