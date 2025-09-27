namespace Common.FIFOQueue
{
    public interface IFIFOQueue
    {
        short[] GetValuesFromQueue(ushort startingAddress);

        bool CheckAddress(ushort address);
    }
}