using Common.Serialization;

namespace Common.Message
{
    /// <summary>
    /// Describes the methods that a ModbusData should implement
    /// </summary>
    public interface IModbusData : ISerialize, IDeserialize
    {
    }
}
