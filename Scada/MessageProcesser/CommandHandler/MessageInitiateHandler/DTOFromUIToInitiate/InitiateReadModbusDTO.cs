using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// DTO object for Read Modbus, this object is sent from UI layer to the Message layer
    /// Contains just the most important values for a request
    /// </summary>
    public class InitiateReadModbusDTO : IMessageDTO
    {
        /// <summary>
        /// Address from where to read
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Number of values to read
        /// </summary>
        public ushort Quantity { get; set; }
    }
}
