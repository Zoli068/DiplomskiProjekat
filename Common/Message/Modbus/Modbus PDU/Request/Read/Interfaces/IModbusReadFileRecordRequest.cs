using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadFileRecordRequest:IModbusData
    {
        byte ByteCount { get; set; }

        byte[] ReferenceType { get; set; }

        ushort[] FileNumber {  get; set; }

        ushort[] RecordNumber { get; set; }

        ushort[] RecordLength { get; set; }
    }
}
