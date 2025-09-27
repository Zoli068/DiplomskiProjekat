using Common.Message;
using Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.MessageProcesser.MessageInitiateHandler
{
    public class ModbusReadFIFOQueueInitiateCommand : IMessageInitiateCommand<IMessageDTO, IModbusData>
    {
        public IModbusData InitiateMessage(IMessageDTO messageDTO)
        {
           InitiateReadFIFOModbusDTO DTO = messageDTO as InitiateReadFIFOModbusDTO;

            ModbusReadFIFOQueueRequest req = new ModbusReadFIFOQueueRequest();

            req.PointerAddress = DTO.PointAddress;

            return req;
        }
    }
}
