using Common.Command;
using Common.Message;
using Common.Utilities;
using Master.CommandHandler.ResponseCommands;
using Master.MessageProcesser;
using Master.MessageProcesser.CommandHandler.MessageInitiateHandler.DTOToUIResponse;
using System;
using System.Collections.Generic;

namespace Master.CommandHandler
{
    /// <summary>
    /// Handles all the possible modbus responses
    /// </summary>
    public class ModbusPDUResponseHandler : IResponseHandler
    {
        private readonly Dictionary<FunctionCode, IResponseCommand<IModbusPDUData>> commands;
        private Action<FunctionCode, IMessageDTO> responseRecived;

        public ModbusPDUResponseHandler(Action<FunctionCode, IMessageDTO> responseRecived)
        {
            this.responseRecived = responseRecived;

            commands = new Dictionary<FunctionCode, IResponseCommand<IModbusPDUData>>()
            {
                {FunctionCode.ReadCoils,new ReadCoilsResponseCommand() },
                {FunctionCode.ReadDiscreteInputs,new ReadDiscreteResponseCommand() },
                {FunctionCode.ReadHoldingRegisters,new ReadHoldingRegistersResponseCommand() },
                {FunctionCode.ReadInputRegisters,new ReadInputRegistersResponseCommand() },
                {FunctionCode.WriteMultipleCoils,new WriteMultipleCoilsResponseCommand() },
                {FunctionCode.WriteMultipleRegisters,new WriteMultipleRegistersResponseCommand() },
                {FunctionCode.WriteSingleCoil, new WriteSingleCoilResponseCommand() },
                {FunctionCode.WriteSingleRegister,new WriteSingleRegisterResponseCommand() },
                {FunctionCode.MaskWriteRegister,new MaskWriteRegisterResponseCommand() },
                {FunctionCode.ReadFileRecord,new ReadFileRecordResponseCommand() },
                {FunctionCode.WriteFileRecord,new WriteFileRecordResponseCommand() },
                {FunctionCode.ReadWriteMultipleRegisters,new ReadWriteMultipleRegistersResponseCommand() },
                {FunctionCode.ReadFIFOQueue,new ReadFIFOQueueResponseCommand() },
            };
        }

        public void ProcessMessageData(IMessageData request, IMessageData response)
        {
            IResponseCommand<IModbusPDUData> command;
            IMessageDTO responseDTO;

            if (((byte)(((IModbusPDU)response).FunctionCode) & 0x80) == 0)
            {
                if (commands.TryGetValue(((IModbusPDU)response).FunctionCode, out command))
                {
                    responseDTO = command.Execute(((IModbusPDU)request).Data, ((IModbusPDU)response).Data);

                    responseRecived(((IModbusPDU)response).FunctionCode, responseDTO);
                }
            }
            else
            {
                ModbusPDU errorPDU = response as ModbusPDU;

                responseDTO = new ErrorResponseDTO();
                (responseDTO as ErrorResponseDTO).ExceptionCode= ((ModbusError)errorPDU.Data).ExceptionCode;
                (responseDTO as ErrorResponseDTO).ErrorCode= ((ModbusError)errorPDU.Data).ErrorCode;

                responseRecived(((IModbusPDU)response).FunctionCode, responseDTO);
            }
        }
    }
}