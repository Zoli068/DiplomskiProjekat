using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadFIFOQueueResponse : IModbusData
    {
        ushort ByteCount { get; set; }

        ushort FIFOCount { get; set; }

        short[] FIFOValueRegister { get; set; }
    }
}
