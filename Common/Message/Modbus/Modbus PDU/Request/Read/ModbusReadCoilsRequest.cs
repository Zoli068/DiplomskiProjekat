using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusReadCoilsRequest"/> interface
    /// </summary>
    public class ModbusReadCoilsRequest : IModbusReadCoilsRequest
    {
        private ushort startingAddress;
        private ushort quantityOfCoils;

        public ModbusReadCoilsRequest() { }

        public ModbusReadCoilsRequest(ushort startingAddress, ushort quantityOfCoils)
        {
            this.startingAddress = startingAddress;
            this.quantityOfCoils = quantityOfCoils;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfCoils, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>(4);
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfCoils));

            return data.ToArray();
        }

        public ushort StartingAddress
        {
            get
            {
                return startingAddress;
            }
            set
            {
                startingAddress = value;
            }
        }

        public ushort QuantityOfCoils
        {
            get
            {
                return quantityOfCoils;
            }
            set
            {
                quantityOfCoils = value;
            }
        }

    }
}
