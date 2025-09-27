using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class InitiateReadFileRecordModbusDTO:IMessageDTO
    {
        public byte[] ReferenceType { get;set; }

        public ushort[] FileNumber { get; set; }

        public ushort[] RecordNumber { get; set; }

        public ushort[] RecordLength { get; set;}
    }
}
