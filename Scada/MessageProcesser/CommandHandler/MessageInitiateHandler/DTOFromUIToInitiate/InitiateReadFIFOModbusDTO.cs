using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadFIFOModbusDTO:IMessageDTO
    {
        public ushort PointAddress { get; set; }
    }
}
