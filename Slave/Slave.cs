using Common.Communication;
using Common.CommunicationExceptions;
using Common.FIFOQueue;
using Common.FileRecord;
using Common.IPointsDataBase;
using Common.Message;
using Common.PointsDataBase;
using Slave.Message;
using System;

namespace Slave
{
    public class Slave
    {
        ICommunication communication;
        IMessageProcesser<FunctionCode> messageProcesser;
        IPointsDataBase pointsDataBase = new PointsDataBase();
        IFileRecord fileRecord=new FileRecord();
        IFIFOQueue fIFOQueue = new FIFOQueue();

        public void Start()
        {
            try
            {
                communication = new Communication.Communication();
                messageProcesser = new ModbusMessageProcesser(pointsDataBase,fileRecord,fIFOQueue, communication);
            }
            catch (Exception ex) when (ex is ListeningNotSuccessedException)
            {
                Console.WriteLine("The server was not able to start");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unknown error happened");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
