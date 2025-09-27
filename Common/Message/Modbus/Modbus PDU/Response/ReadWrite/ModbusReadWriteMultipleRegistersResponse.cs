using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadWriteMultipleRegistersResponse : IModbusReadWriteMultpleRegistersResponse
    {
        private byte byteCount;
        private short[] readRegistersValue;

        public ModbusReadWriteMultipleRegistersResponse() { }

        public ModbusReadWriteMultipleRegistersResponse(byte byteCount, short[] readRegistersValue)
        {
            this.byteCount=byteCount;
            this.readRegistersValue=readRegistersValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount,data,ref startIndex);
            readRegistersValue = new short[(byteCount / 2)];

            for(int i = 0; i < (byteCount / 2); i++){
                ByteValueConverter.GetValue(out readRegistersValue[i],data,ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data=new List<byte>();
            data.Add(byteCount);

            for(int i=0;i< (byteCount / 2); i++)
            {
                data.AddRange(ByteValueConverter.ExtractBytes(readRegistersValue[i]));
            }

            return data.ToArray();
        }

        public byte ByteCount
        {
            get
            {
                return byteCount;
            }
            set
            {
                byteCount = value;
            }
        }

        public short[] ReadRegistersValue
        {
            get
            {
                return readRegistersValue;
            }
            set
            {
                readRegistersValue = value;
            }
        }
    }
}
