using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusReadHoldingRegistersResponse"/> interface
    /// </summary>
    public class ModbusReadHoldingRegistersResponse : IModbusReadHoldingRegistersResponse
    {
        private byte byteCount;
        private short[] registerValue;

        public ModbusReadHoldingRegistersResponse() { }

        public ModbusReadHoldingRegistersResponse(byte byteCount, short[] registerValue)
        {
            this.byteCount = byteCount;
            this.registerValue = registerValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            registerValue = new short[byteCount];

            for (int i = 0; i < byteCount / 2; i++)
            {
                ByteValueConverter.GetValue(out registerValue[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.Add(byteCount);

            for (int i = 0; i < byteCount / 2; i++)
            {
                data.AddRange(ByteValueConverter.ExtractBytes(registerValue[i]));
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

        public short[] RegisterValue
        {
            get
            {
                return registerValue;
            }
            set
            {
                registerValue = value;
            }
        }
    }
}
