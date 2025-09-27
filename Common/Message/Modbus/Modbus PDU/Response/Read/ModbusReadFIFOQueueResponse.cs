using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadFIFOQueueResponse : IModbusReadFIFOQueueResponse
    {
        private ushort byteCount;
        private ushort fifoCount;
        private short[] fifoValueRegister;

        public ModbusReadFIFOQueueResponse() { }

        public ModbusReadFIFOQueueResponse(ushort byteCount, ushort fifoCount, short[] fifoValueRegister)
        {
            this.byteCount = byteCount;
            this.fifoCount = fifoCount;
            this.fifoValueRegister = fifoValueRegister;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);
            ByteValueConverter.GetValue(out fifoCount, data, ref startIndex);

            fifoValueRegister=new short[fifoCount];

            for(int i=0;i< fifoCount;i++)
            {
                ByteValueConverter.GetValue(out fifoValueRegister[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> bytes= new List<byte>();

            bytes.AddRange(ByteValueConverter.ExtractBytes(byteCount));
            bytes.AddRange(ByteValueConverter.ExtractBytes(fifoCount));

            for(int i=0; i< fifoCount; i++) 
            {
                bytes.AddRange(ByteValueConverter.ExtractBytes(fifoValueRegister[i]));           
            }
        
            return  bytes.ToArray();
        }

        public ushort ByteCount
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

        public ushort FIFOCount
        {
            get
            {
                return fifoCount;
            }
            set
            {
                fifoCount = value;
            }
        }

        public short[] FIFOValueRegister
        {
            get
            {
                return fifoValueRegister;
            }
            set
            {
                fifoValueRegister = value;
            }
        }
    }
}
