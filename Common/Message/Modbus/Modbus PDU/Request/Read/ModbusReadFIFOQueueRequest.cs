using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadFIFOQueueRequest : IModbusReadFIFOQueueRequest
    {
        private ushort pointAddress;

        public ModbusReadFIFOQueueRequest() { }

        public ModbusReadFIFOQueueRequest(ushort pointAddress)
        {
            this.pointAddress = pointAddress;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out pointAddress, data, ref startIndex);
        }

        public byte[] Serialize()
        {
          return ByteValueConverter.ExtractBytes(pointAddress);
        }

        public ushort PointerAddress
        {
            get
            {
                return pointAddress;
            }
            set
            {
                pointAddress = value;
            }
        }
    }
}
