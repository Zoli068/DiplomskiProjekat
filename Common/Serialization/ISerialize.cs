namespace Common.Serialization
{
    /// <summary>
    /// Interface that describes the method which have to be 
    /// implemented to be able to Serialize to that class
    /// </summary>
    public interface ISerialize
    {
        /// <summary>
        /// Method which returns our object as byte array
        /// </summary>
        /// <returns>Our object in byte array form</returns>
        byte[] Serialize();
    }
}
