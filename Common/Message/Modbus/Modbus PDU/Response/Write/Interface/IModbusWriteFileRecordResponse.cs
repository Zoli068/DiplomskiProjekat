namespace Common.Message
{
    public interface IModbusWriteFileRecordResponse:IModbusPDUData
    {
        byte ResponseDataLength { get; set; }

        byte[] ReferenceType { get; set; }

        ushort[] FileNumber { get; set; }

        ushort[] RecordNumber { get; set; }

        ushort[] RecordLength { get; set; }

        short[][] RecordData { get; set; }
    }
}