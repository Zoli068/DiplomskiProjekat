using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusWriteFileRecordResponse:IModbusData
    {
        byte ResponseDataLength { get; set; }

        byte[] ReferenceType { get; set; }

        ushort[] FileNumber { get; set; }

        ushort[] RecordNumber { get; set; }

        ushort[] RecordLength { get; set; }

        short[][] RecordData { get; set; }
    }
}
