using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusReadInputRegistersRequest"/> interface
    /// </summary>
    public class ModbusReadInputRegistersRequest : IModbusReadInputRegistersRequest
    {
        private ushort startingAddress;
        private ushort quantityOfInputRegisters;

        public ModbusReadInputRegistersRequest() { }

        public ModbusReadInputRegistersRequest(ushort startingAddress, ushort quantityOfInputRegisters)
        {
            this.startingAddress = startingAddress;
            this.quantityOfInputRegisters = quantityOfInputRegisters;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfInputRegisters, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>(4);
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfInputRegisters));

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

        public ushort QuantityOfInputRegisters
        {
            get
            {
                return quantityOfInputRegisters;
            }
            set
            {
                quantityOfInputRegisters = value;
            }
        }
    }
}
