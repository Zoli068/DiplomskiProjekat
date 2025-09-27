namespace Common.Message
{
    public interface IModbusReadFIFOQueueRequest : IModbusPDUData
    {
        ushort PointerAddress { get; set; }
    }
}