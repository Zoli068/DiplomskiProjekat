namespace Common.Message
{
    public interface IModbusReadWriteMultpleRegistersResponse:IModbusPDUData
    {
        byte ByteCount { get; set; }

        short[] ReadRegistersValue { get; set; }
    }
}