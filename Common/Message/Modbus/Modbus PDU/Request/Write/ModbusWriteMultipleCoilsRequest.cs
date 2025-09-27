using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusWriteMultipleCoilsRequest"/> interface
    /// </summary>
    public class ModbusWriteMultipleCoilsRequest : IModbusWriteMultipleCoilsRequest
    {
        private ushort startingAddress;
        private ushort quantityOfOutputs;
        private byte byteCount;
        private byte[] outputsValue;

        public ModbusWriteMultipleCoilsRequest() { }

        public ModbusWriteMultipleCoilsRequest(ushort startingAddress, ushort quantityOfOutputs, byte byteCount, byte[] outputsValue)
        {
            this.startingAddress = startingAddress;
            this.quantityOfOutputs = quantityOfOutputs;
            this.byteCount = byteCount;
            this.outputsValue = outputsValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out startingAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out quantityOfOutputs, data, ref startIndex);
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            outputsValue = new byte[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                ByteValueConverter.GetValue(out outputsValue[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();
            data.AddRange(ByteValueConverter.ExtractBytes(startingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityOfOutputs));
            data.AddRange(ByteValueConverter.ExtractBytes(byteCount));

            for (int i = 0; i < byteCount; i++)
            {
                data.AddRange(ByteValueConverter.ExtractBytes(outputsValue[i]));
            }

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

        public ushort QuantityOfOutputs
        {
            get
            {
                return quantityOfOutputs;
            }
            set
            {
                quantityOfOutputs = value;
            }
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

        public byte[] OutputsValue
        {
            get
            {
                return outputsValue;
            }
            set
            {
                outputsValue = value;
            }
        }

    }
}
