namespace Common.FileRecord
{
    public interface IFileRecord
    {
        short[] ReadFileRecord(ushort fileNumber, ushort recordNumber,ushort recordLength);

        void WriteFileRecord(ushort fileNumber,ushort recordNumber, short[] data);

        bool CheckValues(byte referenceType, ushort fileNumber, ushort recordNumber, ushort length);
    }
}