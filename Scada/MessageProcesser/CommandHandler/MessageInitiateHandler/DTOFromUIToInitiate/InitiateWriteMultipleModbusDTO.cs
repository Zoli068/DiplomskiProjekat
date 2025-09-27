using Common.Utilities;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// DTO object for Write Multiple Modbus, this object is sent from UI layer to the Message layer
    /// Contains just the most important values for a request
    /// </summary>
    public class InitiateWriteMultipleModbusDTO : IMessageDTO
    {
        /// <summary>
        /// Address where to start the writing
        /// </summary>
        public ushort Address { get; set; }
        
        /// <summary>
        /// Number of values to be written
        /// </summary>
        public ushort Quantity { get; set; }

        /// <summary>
        /// The values that will be written
        /// </summary>
        public short[] Values { get; set; }
    }
}
