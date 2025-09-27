using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusReadDiscreteInputsResponse"/> interface
    /// </summary>
    public class ModbusReadDiscreteInputsResponse : IModbusReadDiscreteInputsResponse
    {
        private byte byteCount;
        private byte[] inputStatus;

        public ModbusReadDiscreteInputsResponse() { }

        public ModbusReadDiscreteInputsResponse(byte byteCount, byte[] inputStatus)
        {
            this.byteCount = byteCount;
            this.inputStatus = inputStatus;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            inputStatus = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                ByteValueConverter.GetValue(out inputStatus[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.Add(byteCount);

            for (int i = 0; i < byteCount; i++)
            {
                data.Add(inputStatus[i]);
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

        public byte[] InputStatus
        {
            get
            {
                return inputStatus;
            }
            set
            {
                inputStatus = value;
            }
        }
    }
}
