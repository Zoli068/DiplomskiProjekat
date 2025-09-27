using Common.Serialization;

namespace Common.Message
{
    /// <summary>
    /// Every message type object will have his own IMessageHeader attribute
    /// </summary>
    public interface IMessageHeader : ISerialize, IDeserialize
    {
    }
}
