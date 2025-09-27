using Common.Message;
using Common.Utilities;
using Master.CommandHandler.MessageInitiateHandler;
using Master.MessageProcesser.MessageInitiateHandler;
using System;
using System.Net;
using System.Threading;

namespace Master.UI
{
    /// <summary>
    /// A basic UI class, with a basic terminal based menu
    /// </summary>
    public class ConsoleUI
    {
        private Action<FunctionCode, IMessageDTO> initateMessage;

        public ConsoleUI(Action<FunctionCode, IMessageDTO> initateMessage)
        {
            this.initateMessage = initateMessage;
        }

        public void Start()
        {
            int selectedOption;
            bool running = true;

            while (running)
            {

                Console.WriteLine("Choose your option");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Read Coils");
                Console.WriteLine("2 - Read Discrete Inputs");
                Console.WriteLine("3 - Read Holding Registers");
                Console.WriteLine("4 - Read Input Registers");
                Console.WriteLine("5 - Write Single Coil");
                Console.WriteLine("6 - Write Single Register");
                Console.WriteLine("7 - Write Multiple Coils");
                Console.WriteLine("8 - Write Multiple Registers");
                Console.WriteLine("9 - Mask Write Register");
                Console.WriteLine("10 - Read Write Multiple Registers");
                Console.WriteLine("11 - Read FIFO Queue");
                Console.WriteLine("12 - Read File Record");
                Console.WriteLine("13 - Write File Record");

                if (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    continue;
                }

                try
                {
                    switch (selectedOption)
                    {
                        case 0:
                            running = false;
                            break;
                        case 1:
                            ReadCoils();
                            break;
                        case 2:
                            ReadDiscreteInputsRequest();
                            break;
                        case 3:
                            ReadHoldingRegisters();
                            break;
                        case 4:
                            ReadInputRegisters();
                            break;
                        case 5:
                            WriteSingleCoil();
                            break;
                        case 6:
                            WriteSingleRegister();
                            break;
                        case 7:
                            WriteMultipleCoilsRequest();
                            break;
                        case 8:
                            WriteMultipleRegisters();
                            break;
                        case 9:
                            MaskWriteRegister();
                            break;
                        case 10:
                            ReadWriteMultipleRegisters();
                            break;                        
                        case 11:
                            ReadFIFOQueue();
                            break;
                        case 12:
                            ReadFileRecord();
                            break;
                        case 13:
                            WriteFileRecord();
                            break;
                    }
                }
                catch (MessageDTOBadValuesException)
                {
                    Console.WriteLine("Bad values");
                }

                Thread.Sleep(200);
            }
        }

        private void ReadCoils()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Coils");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadCoils, initiateReadModbusDTO);
        }

        private void ReadDiscreteInputsRequest()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Discrete Inputs");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadDiscreteInputs, initiateReadModbusDTO);
        }

        private void ReadHoldingRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Holding Registers");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadHoldingRegisters, initiateReadModbusDTO);
        }

        private void ReadInputRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Read Input Registers");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            InitiateReadModbusDTO initiateReadModbusDTO = new InitiateReadModbusDTO();
            initiateReadModbusDTO.Address = address;
            initiateReadModbusDTO.Quantity = quantity;

            initateMessage(FunctionCode.ReadInputRegisters, initiateReadModbusDTO);
        }

        private void WriteSingleCoil()
        {
            ushort address;
            short value;

            Console.Clear();
            Console.WriteLine("Write Single Coil");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the value:");
            if (!short.TryParse(Console.ReadLine(), out value)) { return; }

            InitiateWriteSingleModbusDTO initiateWriteSingleModbusDTO = new InitiateWriteSingleModbusDTO();
            initiateWriteSingleModbusDTO.Address = address;
            initiateWriteSingleModbusDTO.Value = value;

            initateMessage(FunctionCode.WriteSingleCoil, initiateWriteSingleModbusDTO);
        }

        private void WriteSingleRegister()
        {
            ushort address;
            short value;

            Console.Clear();
            Console.WriteLine("Write Single Register");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the value:");
            if (!short.TryParse(Console.ReadLine(), out value)) { return; }

            InitiateWriteSingleModbusDTO initiateWriteSingleModbusDTO = new InitiateWriteSingleModbusDTO();
            initiateWriteSingleModbusDTO.Address = address;
            initiateWriteSingleModbusDTO.Value = value;

            initateMessage(FunctionCode.WriteSingleRegister, initiateWriteSingleModbusDTO);
        }

        private void WriteMultipleRegisters()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Write Multiple Register");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            short[] values = new short[quantity];

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Enter the " + (i + 1) + " value:");
                if (!short.TryParse(Console.ReadLine(), out values[i])) { return; }
            }

            InitiateWriteMultipleModbusDTO initiateWriteMultipleModbusDTO = new InitiateWriteMultipleModbusDTO();

            initiateWriteMultipleModbusDTO.Address = address;
            initiateWriteMultipleModbusDTO.Quantity = quantity;
            initiateWriteMultipleModbusDTO.Values = values;

            initateMessage(FunctionCode.WriteMultipleRegisters, initiateWriteMultipleModbusDTO);
        }

        private void WriteMultipleCoilsRequest()
        {
            ushort address;
            ushort quantity;

            Console.Clear();
            Console.WriteLine("Write Multiple Coils");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the quantity:");
            if (!ushort.TryParse(Console.ReadLine(), out quantity)) { return; }

            short[] values = new short[quantity];

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine("Enter the " + (i + 1) + " value:");
                if (!short.TryParse(Console.ReadLine(), out values[i])) { return; }
            }

            InitiateWriteMultipleModbusDTO initiateWriteMultipleModbusDTO = new InitiateWriteMultipleModbusDTO();

            initiateWriteMultipleModbusDTO.Address = address;
            initiateWriteMultipleModbusDTO.Quantity = quantity;
            initiateWriteMultipleModbusDTO.Values = values;

            initateMessage(FunctionCode.WriteMultipleCoils, initiateWriteMultipleModbusDTO);
        }

        private void MaskWriteRegister()
        {
            ushort address;
            ushort orMask;
            ushort andMask;

            Console.Clear();
            Console.WriteLine("Mask Write Register");
            Console.WriteLine("Enter the address:");
            if (!ushort.TryParse(Console.ReadLine(), out address)) { return; }

            Console.WriteLine("Enter the or mask:");
            if (!ushort.TryParse(Console.ReadLine(), out orMask)) { return; }

            Console.WriteLine("Enter the and mask:");
            if (!ushort.TryParse(Console.ReadLine(), out andMask)) { return; }

            InitiateMaskWriteModbusDTO initiateMaskWriteModbusDTO = new InitiateMaskWriteModbusDTO();

            initiateMaskWriteModbusDTO.Address = address;
            initiateMaskWriteModbusDTO.OrMask = orMask;
            initiateMaskWriteModbusDTO.AndMask = andMask;

            initateMessage(FunctionCode.MaskWriteRegister, initiateMaskWriteModbusDTO);
        }

        private void ReadWriteMultipleRegisters()
        {
            ushort writeAddress;
            ushort readAddress;
            ushort quantityToRead;
            ushort quantityToWrite;

            Console.Clear();
            Console.WriteLine("Read Write Multiple Registers");

            Console.WriteLine("Enter the write address:");
            if (!ushort.TryParse(Console.ReadLine(), out writeAddress)) { return; }
            
            Console.WriteLine("Enter the quantity to write:");
            if (!ushort.TryParse(Console.ReadLine(), out quantityToWrite)) { return; }

            InitiateReadWriteMultipleRegistersModbusDTO req=new InitiateReadWriteMultipleRegistersModbusDTO();
            req.WriteRegistersValue = new short[quantityToWrite];

            for(int i=0;i< quantityToWrite; i++)
            {
                Console.WriteLine("Enter the " + i + " value to write:");
                if (!short.TryParse(Console.ReadLine(), out req.WriteRegistersValue[i])) { return; }
            }

            Console.WriteLine("Enter the read address:");
            if (!ushort.TryParse(Console.ReadLine(), out readAddress)) { return; }


            Console.WriteLine("Enter the quantity to read:");
            if (!ushort.TryParse(Console.ReadLine(), out quantityToRead)) { return; }

            req.ReadStartingAddress=readAddress;
            req.WriteStartingAddress = writeAddress;
            req.QuantityToRead = quantityToRead;
            req.QuantityToWrite = quantityToWrite;

            initateMessage(FunctionCode.ReadWriteMultipleRegisters, req);
        }

        private void ReadFIFOQueue()
        {
            ushort pointAddress;
            Console.Clear();
            Console.WriteLine("Read FIFO Queue");

            Console.WriteLine("Enter the point address:");
            if (!ushort.TryParse(Console.ReadLine(), out pointAddress)) { return; }

            InitiateReadFIFOModbusDTO initiateReadFIFOModbusDTO = new InitiateReadFIFOModbusDTO();
            initiateReadFIFOModbusDTO.PointAddress=pointAddress;

            initateMessage(FunctionCode.ReadFIFOQueue, initiateReadFIFOModbusDTO);
        }

        private void ReadFileRecord()
        {
            Console.Clear();
            Console.WriteLine("Read File Record");

            ushort numberOfRecords;

            Console.WriteLine("Enter the number of records to be readed:");
            if (!ushort.TryParse(Console.ReadLine(), out numberOfRecords)) { return; }

            InitiateReadFileRecordModbusDTO req = new InitiateReadFileRecordModbusDTO();

            req.ReferenceType = new byte[numberOfRecords];
            req.FileNumber = new ushort[numberOfRecords];
            req.RecordNumber = new ushort[numberOfRecords];
            req.RecordLength = new ushort[numberOfRecords];

            for(int i = 0; i < numberOfRecords; i++)
            {
                Console.WriteLine("Enter the reference type for record "+i+" :");
                if (!byte.TryParse(Console.ReadLine(), out req.ReferenceType[i])) { return; }                
                
                Console.WriteLine("Enter the file number for record "+i+" :");
                if (!ushort.TryParse(Console.ReadLine(), out req.FileNumber[i])) { return; }    
                
                Console.WriteLine("Enter the record number for record "+i+" :");
                if (!ushort.TryParse(Console.ReadLine(), out req.RecordNumber[i])) { return; }    
                
                Console.WriteLine("Enter the record length for record "+i+" :");
                if (!ushort.TryParse(Console.ReadLine(), out req.RecordLength[i])) { return; }
            }

            initateMessage(FunctionCode.ReadFileRecord, req);
        }

        private void WriteFileRecord()
        {
            Console.Clear();
            Console.WriteLine("Write File Record");

            ushort numberOfRecords;

            Console.WriteLine("Enter the number of records to be written:");
            if (!ushort.TryParse(Console.ReadLine(), out numberOfRecords)) { return; }

            InitiateWriteFileRecordModbusDTO req = new InitiateWriteFileRecordModbusDTO();

            req.ReferenceType = new byte[numberOfRecords];
            req.FileNumber = new ushort[numberOfRecords];
            req.RecordNumber = new ushort[numberOfRecords];
            req.RecordLength = new ushort[numberOfRecords];
            req.RecordData =new short[numberOfRecords][];

            for (int i = 0; i < numberOfRecords; i++)
            {
                Console.WriteLine("Enter the reference type for record " + i + " :");
                if (!byte.TryParse(Console.ReadLine(), out req.ReferenceType[i])) { return; }

                Console.WriteLine("Enter the file number for record " + i + " :");
                if (!ushort.TryParse(Console.ReadLine(), out req.FileNumber[i])) { return; }

                Console.WriteLine("Enter the record number for record " + i + " :");
                if (!ushort.TryParse(Console.ReadLine(), out req.RecordNumber[i])) { return; }

                Console.WriteLine("Enter the record length for record " + i + " :");
                if (!ushort.TryParse(Console.ReadLine(), out req.RecordLength[i])) { return; }

                req.RecordData[i] = new short[req.RecordLength[i]];

                for (int j = 0; j < req.RecordLength[i]; j++)
                {
                    Console.WriteLine("Enter the " + j + " value for the " + i + " record:");
                    if (!short.TryParse(Console.ReadLine(), out req.RecordData[i][j])) { return; }
                }
            }
                initateMessage(FunctionCode.WriteFileRecord, req);
        }
    }
}
