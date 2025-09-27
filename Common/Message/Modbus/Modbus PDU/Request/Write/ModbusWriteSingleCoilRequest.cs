using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusWriteSingleCoilRequest"/> interface
    /// </summary>
    public class ModbusWriteSingleCoilRequest : IModbusWriteSingleCoilRequest
    {
        private ushort outputAddress;
        private ushort outputValue;

        public ModbusWriteSingleCoilRequest() { }

        public ModbusWriteSingleCoilRequest(ushort outputAddress, ushort outputValue)
        {
            this.outputAddress = outputAddress;
            this.outputValue = outputValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out outputAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out outputValue, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.AddRange(ByteValueConverter.ExtractBytes(outputAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(outputValue));

            return data.ToArray();
        }

        public ushort OutputAddress
        {
            get
            {
                return outputAddress;
            }
            set
            {
                outputAddress = value;
            }
        }

        public ushort OutputValue
        {
            get
            {
                return outputValue;
            }
            set
            {
                outputValue = value;
            }
        }

    }
}
