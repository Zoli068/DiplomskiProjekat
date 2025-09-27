namespace Common.Serialization
{
    /// <summary>
    /// Helper class for Serialization objects
    /// </summary>
    public static class Serialization
    {
        public static T CreateMessageObject<T>(byte[] data) where T : IDeserialize, new()
        {
            int parsedBytes = 0;

            IDeserialize deserializedObject = new T();
            deserializedObject.Deserialize(data, ref parsedBytes);

            return (T)deserializedObject;
        }

        public static byte[] ExtractMessageBytes<T>(T message) where T : ISerialize, new()
        {
            return message.Serialize();
        }

    }
}
