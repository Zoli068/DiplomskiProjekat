namespace Common.Message
{
    public interface IModbusReadFIFOQueueResponse : IModbusPDUData
    {
        ushort ByteCount { get; set; }

        ushort FIFOCount { get; set; }

        short[] FIFOValueRegister { get; set; }
    }
}