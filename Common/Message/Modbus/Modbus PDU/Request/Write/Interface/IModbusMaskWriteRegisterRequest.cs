using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message.Modbus
{
    /// <summary>
    /// Describes a Modbus Mask Write Register Request attributes
    /// </summary>
    public interface IModbusMaskWriteRegisterRequest:IModbusData
    {
        /// <summary>
        /// Address where we want to write
        /// </summary>
        ushort ReferenceAddress { get; set; }
        
        /// <summary>
        /// The AND Mask which will be use for the write
        /// </summary>
        ushort AndMask {  get; set; }

        /// <summary>
        /// The OR Mask which will be use for the write
        /// </summary>
        ushort OrMask { get; set; }
    }
}
