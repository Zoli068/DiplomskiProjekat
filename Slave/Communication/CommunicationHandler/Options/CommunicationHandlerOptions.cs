using Common;

namespace Slave.Communication
{
    /// <summary>
    /// Contains all the must have values for the <see cref="CommunicationHandler"/> class
    /// </summary>
    public class CommunicationHandlerOptions : ICommunicationHandlerOptions
    {
        private readonly SecurityMode securityMode;
        private readonly MessageType messageType;

        public CommunicationHandlerOptions(SecurityMode securityMode, MessageType messageType)
        {
            this.securityMode = securityMode;
            this.messageType = messageType;
        }

        public SecurityMode SecurityMode
        {
            get
            {
                return securityMode;
            }
        }

        public MessageType MessageType
        {
            get
            {
                return messageType;
            }
        }
    }
}
