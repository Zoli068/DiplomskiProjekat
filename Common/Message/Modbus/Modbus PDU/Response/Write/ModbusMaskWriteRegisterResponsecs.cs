using Common.Message.Modbus;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="IModbusMaskWriteRegisterResponse"/> interface
    /// </summary>
    public class ModbusMaskWriteRegisterResponse : IModbusMaskWriteRegisterResponse
    { 
        private ushort referenceAddress;
        private ushort andMask;
        private ushort orMask;

        public ModbusMaskWriteRegisterResponse() { }

        public ModbusMaskWriteRegisterResponse(ushort referenceAddress, ushort andMask, ushort orMask)
        {
            this.referenceAddress = referenceAddress;
            this.andMask = andMask;
            this.orMask = orMask;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out referenceAddress, data, ref startIndex);
            ByteValueConverter.GetValue(out andMask, data, ref startIndex);
            ByteValueConverter.GetValue(out orMask, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> data = new List<byte>();

            data.AddRange(ByteValueConverter.ExtractBytes(referenceAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(andMask));
            data.AddRange(ByteValueConverter.ExtractBytes(orMask));

            return data.ToArray();
        }

        public ushort ReferenceAddress
        {
            get
            {
                return referenceAddress;
            }
            set
            {
                referenceAddress = value;
            }
        }

        public ushort AndMask
        {
            get
            {
                return andMask;
            }
            set
            {
                andMask = value;
            }
        }

        public ushort OrMask
        {
            get
            {
                return orMask;
            }
            set
            {
                orMask = value;
            }
        }
    }
}
