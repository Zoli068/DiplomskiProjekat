using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.CommandHandler.MessageInitiateHandler
{
    /// <summary>
    /// DTO object for Mask write Modbus, this object is sent from UI layer to the Message layer
    /// Contains just the most important values for a request
    /// </summary>
    public class InitiateMaskWriteModbusDTO:IMessageDTO
    {
        /// <summary>
        /// Address where to write
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// And mask for write
        /// </summary>
        public ushort AndMask { get; set; }

        /// <summary>
        /// Or mask for write
        /// </summary>
        public ushort OrMask { get; set; }
    }
}
