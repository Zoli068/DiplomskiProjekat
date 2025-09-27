namespace Common.Serialization
{
    /// <summary>
    /// Interface that describes the method which have to be 
    /// implemented to be able to Deserialize to that class
    /// </summary>
    public interface IDeserialize
    {
        /// <summary>
        /// Method for Deserialize a byte array into the class which implements that interface
        /// </summary>
        /// <param name="data">Byte array which contains the data for deserialization</param>
        /// <param name="startIndex">The starting index where does the data starts inside the array</param>
        void Deserialize(byte[] data, ref int startIndex);
    }
}
