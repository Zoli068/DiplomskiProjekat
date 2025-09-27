using Common.Command;
using Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser
{
    public class ReadFileRecordResponseCommand : IResponseMessageDataCommand<IModbusData>
    {
        public void Execute(IModbusData request, IModbusData response)
        {
            ModbusReadFileRecordResponse res= response as ModbusReadFileRecordResponse;

            ModbusReadFileRecordRequest req= request as ModbusReadFileRecordRequest;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Read File Record Response:");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Response Data Length:" + res.ResponseDataLength);
            Console.WriteLine("-------------------------------");

            for(int i = 0; i < res.ReferenceType.Length; i++)
            {
                Console.WriteLine(i + "Record\n");
                Console.WriteLine("Reference Type:" + req.ReferenceType[i]);
                Console.WriteLine("File Number:" + req.FileNumber[i]);
                Console.WriteLine("Record Number:" + req.RecordNumber[i]);
                Console.WriteLine("Record Length:" + req.RecordLength[i]);
                Console.WriteLine("File Response Length:" + res.FileResponseLength[i]);

                for(int j=0;j <( res.FileResponseLength[i]-1)/2; j++)
                {
                    Console.WriteLine(j+" value:" + res.RecordData[i][j]);
                }
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
