using Common.Command;
using Common.Message;
using System;

namespace Master.MessageProcesser
{
    public class WriteFileRecordResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public void Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusWriteFileRecordResponse res= response as ModbusWriteFileRecordResponse;


            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Write File Record Response:");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Response Data Length:" + res.ResponseDataLength);
            Console.WriteLine("-------------------------------");

            for (int i = 0; i < res.ReferenceType.Length; i++)
            {
                Console.WriteLine(i + "Record\n");
                Console.WriteLine("Reference Type:" + res.ReferenceType[i]);
                Console.WriteLine("File Number:" + res.FileNumber[i]);
                Console.WriteLine("Record Number:" + res.RecordNumber[i]);
                Console.WriteLine("Record Length:" + res.RecordLength[i]);

                for (int j = 0; j < res.RecordLength[i]; j++)
                {
                    Console.WriteLine(j + " value:" + res.RecordData[i][j]);
                }
                Console.WriteLine("-------------------------------");
            }
        }
    }
}