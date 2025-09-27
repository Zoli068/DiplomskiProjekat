using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.FileRecord
{
    public class FileRecord : IFileRecord
    {
        private Dictionary<ushort, List<List<short>>> fileDatabase;

        public FileRecord()
        {
            fileDatabase = new Dictionary<ushort, List<List<short>>>();
            AddExamples();
        }

        public short[] ReadFileRecord(ushort fileNumber, ushort recordNumber,ushort recordLength)
        {
            List<List<short>> file;
            List<short> record;

            if (fileDatabase.TryGetValue(fileNumber, out file)){

                if(file.Count - 1  < recordNumber)
                {
                    throw new UnsuccessfullReadFileRecordException();
                }

                record = file[recordNumber];

                short[] recordValues=new short[recordLength];

                for(int i = 0; i < recordLength; i++)
                {
                    recordValues[i] = record[i];
                }

                return recordValues;
            }
            else
            {
                throw new UnsuccessfullReadFileRecordException();
            }
        }

        public void WriteFileRecord(ushort fileNumber, ushort recordNumber, short[] data)
        {
            List<List<short>> file;

            if (fileDatabase.TryGetValue(fileNumber, out file))
            {
                List<short> newData = new List<short>();
                newData.AddRange(data);

                if (recordNumber>=0 && recordNumber < file.Count)
                {
                    file[recordNumber] = newData;

                }
                else if (recordNumber == file.Count)
                {
                    file.Add(newData);
                }
                else
                {
                    throw new UnsuccessfullWriteFileRecordException();
                }
            }
            else
            {
                throw new UnsuccessfullWriteFileRecordException();
            }
        }


        public bool CheckValues(byte referenceType, ushort fileNumber, ushort recordNumber, ushort length)
        {
            if (referenceType != 0x06)
            {
                return false;
            }

            List<List<short>> file;

            if (fileDatabase.TryGetValue(fileNumber, out file)) 
            { 
                if (file.Count > recordNumber || file.Count > (recordNumber + length - 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void AddExamples()
        {
            fileDatabase[0] = new List<List<short>> {
                new List<short> { 10, 20 },
                new List<short> { 30, 40 },
                new List<short> { 50, 60 }
            };

            fileDatabase[1] = new List<List<short>> {
                new List<short> { 100, 200, 300 },
                new List<short> { 400, 500, 600 }
            };

            fileDatabase[2] = new List<List<short>> {
                new List<short> { 700 },
                new List<short> { 800 },
                new List<short> { 900 },
                new List<short> { 1000 }
            };

            fileDatabase[3] = new List<List<short>> {
                new List<short> { 110, 120, 130, 140, 150 },
                new List<short> { 210, 220, 230, 240, 250 }
            };

            fileDatabase[4] = new List<List<short>> {
                new List<short> { 310, 320, 330, 340 },
                new List<short> { 410, 420, 430, 440 },
                new List<short> { 510, 520, 530, 540 }
            };
        }
    }
}
