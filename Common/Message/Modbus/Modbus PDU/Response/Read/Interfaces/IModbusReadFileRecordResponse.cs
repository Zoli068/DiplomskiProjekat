using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public interface IModbusReadFileRecordResponse:IModbusData
    {
        byte ResponseDataLength { get; set; }

        byte[] FileResponseLength { get; set; }

        byte[] ReferenceType { get; set; }

        short[][] RecordData { get; set; }
    }
}
