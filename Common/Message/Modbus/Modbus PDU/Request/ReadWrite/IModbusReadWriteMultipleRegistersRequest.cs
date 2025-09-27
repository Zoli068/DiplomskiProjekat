using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Describes a Modbus Read Write Multiple Registers Request attributes
    /// </summary>
    public interface IModbusReadWriteMultipleRegistersRequest:IModbusData
    {
        ushort ReadStartingAddress { get; set; }

        ushort QuantityToRead { get; set; }

        ushort WriteStartingAddress { get; set; }

        ushort QuantityToWrite { get; set; }

        byte WriteByteCount {  get; set; }  

        short[] WriteRegistersValue { get; set; }
    }
}
