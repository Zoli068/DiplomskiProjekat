using Common.Command;
using Common.Message;
using System;

namespace Master.MessageProcesser
{
    public class MaskWriteRegisterResponseCommand : IResponseCommand<IModbusPDUData>
    {
        public void Execute(IModbusPDUData request, IModbusPDUData response)
        {
            ModbusMaskWriteRegisterResponse res = response as ModbusMaskWriteRegisterResponse;

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Mask Write Register Response:");
            Console.WriteLine("--------------------------------------------------------------");

            Console.WriteLine("Reference address:" + res.ReferenceAddress);
            Console.WriteLine("-------------------------------");
            Console.WriteLine("Or mask:" + Convert.ToString(res.OrMask, 2));
            Console.WriteLine("-------------------------------");
            Console.WriteLine("And mask:" + Convert.ToString(res.AndMask, 2));
            Console.WriteLine("-------------------------------");
        }
    }
}