using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    public class ModbusWriteFileRecordRequest : IModbusWriteFileRecordRequest
    {
        private byte requestDataLength;
        private byte[] referenceType;
        private ushort[] fileNumber;
        private ushort[] recordNumber;
        private ushort[] recordLength;
        private short[][] recordData;

        public ModbusWriteFileRecordRequest() { }

        public ModbusWriteFileRecordRequest(byte requestDataLength, byte[] referenceType, ushort[] fileNumber, ushort[] recordNumber, ushort[] recordLength, short[][] recordData)
        {
            this.requestDataLength = requestDataLength;
            this.referenceType = referenceType;
            this.fileNumber = fileNumber;
            this.recordNumber = recordNumber;
            this.recordLength = recordLength;
            this.recordData = recordData;
        }

        public void Deserialize(byte[] data, ref int startIndex)
        {
            ByteValueConverter.GetValue(out requestDataLength, data, ref startIndex);
            List<byte> tempReferenceType= new List<byte>();
            List<ushort> tempFileNumber = new List<ushort>();
            List<ushort> tempRecordNumber = new List<ushort>();
            List<ushort> tempRecordLength = new List<ushort>();
            List<List<short>> tempRecordData= new List<List<short>>();

            List<short> shortHelper;

            byte byteTemp;
            short shortTemp;
            ushort ushortTemp;
            byte bytesLeftToRead=requestDataLength;
            int i = 0;

            while (bytesLeftToRead > 0)
            {
                ByteValueConverter.GetValue(out byteTemp, data, ref startIndex);
                tempReferenceType.Add(byteTemp);

                ByteValueConverter.GetValue(out ushortTemp,data, ref startIndex);
                tempFileNumber.Add(ushortTemp);

                ByteValueConverter.GetValue(out ushortTemp, data, ref startIndex);
                tempRecordNumber.Add(ushortTemp);

                ByteValueConverter.GetValue(out ushortTemp, data, ref startIndex);
                tempRecordLength.Add(ushortTemp);

                bytesLeftToRead = (byte)(bytesLeftToRead - (7 + tempRecordLength[i] * 2));

                shortHelper= new List<short>();

                for(int j=0; j < tempRecordLength[i];j++)
                {
                    ByteValueConverter.GetValue(out shortTemp, data,ref startIndex);
                    shortHelper.Add(shortTemp);
                }

                tempRecordData.Add(shortHelper);
                i++;
            }

            referenceType = tempReferenceType.ToArray();
            fileNumber= tempFileNumber.ToArray();
            recordNumber = tempRecordNumber.ToArray();
            recordLength = tempRecordLength.ToArray();

            recordData = new short[tempRecordData.Count][];

            for(int j=0;j<tempRecordData.Count;j++)
            {
                recordData[j] = tempRecordData[j].ToArray();
            }
        }

        public byte[] Serialize()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(RequestDataLength);

            for(int i=0;i<referenceType.Length;i++)
            {
                bytes.Add(referenceType[i]);
                bytes.AddRange(ByteValueConverter.ExtractBytes(fileNumber[i]));
                bytes.AddRange(ByteValueConverter.ExtractBytes(recordNumber[i]));
                bytes.AddRange(ByteValueConverter.ExtractBytes(recordLength[i]));

                for(int j = 0; j < recordLength[i];j++)
                {
                    bytes.AddRange(ByteValueConverter.ExtractBytes(recordData[i][j]));
                }
            }

            return  bytes.ToArray();
        }


        public byte RequestDataLength
        {
            get
            {
                return requestDataLength;
            }
            set
            {
                requestDataLength= value;
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
                referenceType= value;
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
                fileNumber= value;
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
                recordNumber= value;
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
                recordLength= value;
            }
        }

        public short[][] RecordData
        {
            get
            {
                return recordData;
            }
            set
            {
                recordData= value;
            }
        }
    }
}
