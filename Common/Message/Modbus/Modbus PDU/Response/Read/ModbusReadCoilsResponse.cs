using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusReadCoilsResponse"/> interface
    /// </summary>
    public class ModbusReadCoilsResponse : IModbusReadCoilsResponse
    {
        private byte byteCount;
        private byte[] coilStatus;

        public ModbusReadCoilsResponse() { }

        public ModbusReadCoilsResponse(byte byteCount, byte[] coilStatus)
        {
            this.byteCount = byteCount;
            this.coilStatus = coilStatus;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            coilStatus = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                ByteValueConverter.GetValue(out coilStatus[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.Add(byteCount);

            for (int i = 0; i < byteCount; i++)
            {
                data.Add(coilStatus[i]);
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

        public byte[] CoilStatus
        {
            get
            {
                return coilStatus;
            }
            set
            {
                coilStatus = value;
            }
        }
    }
}
