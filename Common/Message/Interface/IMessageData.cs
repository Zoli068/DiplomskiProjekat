using Common.Serialization;

namespace Common.Message
{
    /// <summary>
    /// Every message type object will have his own IMessageData attribute
    /// </summary>
    public interface IMessageData : ISerialize, IDeserialize
    {
    }
}
