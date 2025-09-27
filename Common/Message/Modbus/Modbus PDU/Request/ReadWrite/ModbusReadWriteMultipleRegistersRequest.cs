using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadWriteMultipleRegistersRequest : IModbusReadWriteMultipleRegistersRequest
    {
        private ushort readStartingAddress;
        private ushort writeStartingAddress;
        private ushort quantityToRead;
        private ushort quantityToWrite;
        private byte writeByteCount;
        private short[] writeRegistersValue;

        public ModbusReadWriteMultipleRegistersRequest() { }

        public ModbusReadWriteMultipleRegistersRequest(ushort readStartingAddress, ushort writeStartingAddress, ushort quantityToRead, ushort quantityToWrite,byte writeByteCount,short[] writeRegistersValue) 
        { 
            this.readStartingAddress = readStartingAddress;
            this.quantityToRead = quantityToRead;
            this.writeStartingAddress = writeStartingAddress;
            this.quantityToWrite = quantityToWrite;
            this.writeByteCount = writeByteCount;
            this.writeRegistersValue = writeRegistersValue;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out readStartingAddress,data,ref startIndex);
            ByteValueConverter.GetValue(out quantityToRead, data,ref startIndex);
            ByteValueConverter.GetValue(out writeStartingAddress, data,ref startIndex);
            ByteValueConverter.GetValue(out quantityToWrite, data,ref startIndex);
            ByteValueConverter.GetValue(out writeByteCount, data,ref startIndex);

            writeRegistersValue = new short[quantityToWrite];
            
            for(int i=0; i < quantityToWrite; i++)
            {
                 ByteValueConverter.GetValue(out writeRegistersValue[i],data,ref startIndex);
            }

        }

        public byte[] Serialize()
        {
            List<byte> data= new List<byte>();
            data.AddRange(ByteValueConverter.ExtractBytes(readStartingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityToRead));
            data.AddRange(ByteValueConverter.ExtractBytes(writeStartingAddress));
            data.AddRange(ByteValueConverter.ExtractBytes(quantityToWrite));
            data.AddRange(ByteValueConverter.ExtractBytes(writeByteCount));

            for (int i = 0; i < quantityToWrite; i++)
            {
                data.AddRange(ByteValueConverter.ExtractBytes(writeRegistersValue[i]));
            }

            return data.ToArray();
        }

        public ushort ReadStartingAddress
        {
            get
            {
                return readStartingAddress;
            }
            set
            {
                readStartingAddress = value;
            }
        }

        public ushort QuantityToRead
        {
            get
            {
                return quantityToRead;
            }
            set
            {
                quantityToRead = value;
            }
        }

        public ushort WriteStartingAddress
        {
            get
            {
                return writeStartingAddress;
            }
            set
            {
                writeStartingAddress = value;
            }
        }

        public ushort QuantityToWrite
        {
            get
            {
                return quantityToWrite;
            }
            set
            {
                quantityToWrite = value;
            }
        }

        public byte WriteByteCount
        {
            get
            {
                return writeByteCount;
            }
            set
            {
                writeByteCount = value;
            }
        }

        public short[] WriteRegistersValue
        {
            get
            {
                return writeRegistersValue;
            }
            set
            {
                writeRegistersValue = value;
            }
        }
    }
}
