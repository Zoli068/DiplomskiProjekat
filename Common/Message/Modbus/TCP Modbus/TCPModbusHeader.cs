using Common.Utilities;
using System.Collections.Generic;

namespace Common.Message
{
    /// <summary>
    /// Implementation of the <see cref="ITCPModbusHeader"/> interface
    /// </summary>
    public class TCPModbusHeader : ITCPModbusHeader
    {
        private ushort transactionID;
        private ushort protocolID = 0x0000;
        private ushort length;
        private byte unitID;

        public TCPModbusHeader() { }

        public TCPModbusHeader(ushort transactionID, ushort protocolID, ushort length, byte unitID)
        {
            this.transactionID = transactionID;
            this.protocolID = protocolID;
            this.length = length;
            this.unitID = unitID;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out transactionID, data, ref startIndex);
            ByteValueConverter.GetValue(out protocolID, data, ref startIndex);
            ByteValueConverter.GetValue(out length, data, ref startIndex);
            ByteValueConverter.GetValue(out unitID, data, ref startIndex);
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>(7);

            bytes.AddRange(ByteValueConverter.ExtractBytes(transactionID));
            bytes.AddRange(ByteValueConverter.ExtractBytes(protocolID));
            bytes.AddRange(ByteValueConverter.ExtractBytes(length));
            bytes.AddRange(ByteValueConverter.ExtractBytes(unitID));

            return bytes.ToArray();
        }

        public ushort TransactionID
        {
            get
            {
                return transactionID;
            }
            set
            {
                transactionID = value;
            }
        }

        public ushort ProtocolID
        {
            get
            {
                return protocolID;
            }
            set
            {
                protocolID = value;
            }
        }

        public ushort Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }

        public byte UnitID
        {
            get
            {
                return unitID;
            }
            set
            {
                unitID = value;
            }
        }
    }
}
