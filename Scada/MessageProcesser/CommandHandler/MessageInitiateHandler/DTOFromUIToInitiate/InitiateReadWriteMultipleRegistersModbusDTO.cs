using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadWriteMultipleRegistersModbusDTO : IMessageDTO
    {
        public ushort ReadStartingAddress { get; set; }

        public ushort QuantityToRead { get; set; }  

        public ushort WriteStartingAddress { get; set; }    

        public ushort QuantityToWrite { get; set; }

        public short[] WriteRegistersValue { get; set; }
    }
}
