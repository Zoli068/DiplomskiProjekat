using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusReadFileRecordRequest : IModbusReadFileRecordRequest
    {
        private byte byteCount;
        private byte[] referenceType;
        private ushort[] fileNumber;
        private ushort[] recordNumber;
        private ushort[] recordLength;

        public ModbusReadFileRecordRequest(){}

        public ModbusReadFileRecordRequest(byte byteCount, byte[] referenceType, ushort[] fileNumber, ushort[] recordNumber, ushort[] recordLength)
        {
            this.byteCount = byteCount;
            this.referenceType = referenceType;
            this.fileNumber = fileNumber;
            this.recordNumber = recordNumber;
            this.recordLength = recordLength;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out byteCount, data, ref startIndex);

            int numOfGroups = byteCount / 7;
            referenceType = new byte[numOfGroups];
            fileNumber = new ushort[numOfGroups];
            recordNumber = new ushort[numOfGroups];
            recordLength = new ushort[numOfGroups];

            for(int i=0 ; i < numOfGroups; i++)
            {
                ByteValueConverter.GetValue(out referenceType[i], data, ref startIndex);
                ByteValueConverter.GetValue(out fileNumber[i], data, ref startIndex);
                ByteValueConverter.GetValue(out recordNumber[i], data, ref startIndex);
                ByteValueConverter.GetValue(out recordLength[i], data, ref startIndex);
            }
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();

            bytes.Add(byteCount);
            int numOfGroups = byteCount / 7;

            for(int i=0;i < numOfGroups; i++)
            {
                bytes.AddRange(ByteValueConverter.ExtractBytes(referenceType[i]));
                bytes.AddRange(ByteValueConverter.ExtractBytes(fileNumber[i]));
                bytes.AddRange(ByteValueConverter.ExtractBytes(recordNumber[i]));
                bytes.AddRange(ByteValueConverter.ExtractBytes(recordLength[i]));
            }

            return bytes.ToArray();
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

        public byte[] ReferenceType
        {
            get
            {
                return referenceType;
            }
            set
            {
                referenceType = value;
            }

        }

        public ushort[] FileNumber
        {
            get
            {
                return fileNumber;
            }
            set
            {
                fileNumber = value;
            }
        }

        public ushort[] RecordNumber
        {
            get
            {
                return recordNumber;
            }
            set
            {
                recordNumber = value;
            }
        }

        public ushort[] RecordLength
        {
            get
            {
                return recordLength;
            }
            set
            {
                recordLength = value;
            }
        }
    }
}
