namespace Common.Message
{
    public interface IModbusReadFileRecordResponse:IModbusPDUData
    {
        byte ResponseDataLength { get; set; }

        byte[] FileResponseLength { get; set; }

        byte[] ReferenceType { get; set; }

        short[][] RecordData { get; set; }
    }
}