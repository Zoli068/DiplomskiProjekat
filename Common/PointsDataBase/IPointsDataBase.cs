namespace Common.IPointsDataBase
{
    /// <summary>
    /// Describes the methods for a Points Data Base class
    /// </summary>
    public interface IPointsDataBase
    {
        /// <summary>
        /// Method to write register value
        /// </summary>
        /// <param name="address">The address of the point</param>
        /// <param name="pointType">Type of the point</param>
        /// <param name="value">Value that will be written</param>
        void WriteRegisterValue(ushort address, PointsType pointType, short value);

        /// <summary>
        /// Method to write discrete value
        /// </summary>
        /// <param name="address">The address of the point</param>
        /// <param name="pointType">Type of the point</param>
        /// <param name="value">Value that will be written</param>
        void WriteDiscreteValue(ushort address, PointsType pointType, byte value);

        /// <summary>
        /// Method to read register value
        /// </summary>
        /// <param name="address">The address of the point</param>
        /// <param name="pointType">The type of the point</param>
        /// <returns></returns>
        short ReadRegisterValue(ushort address, PointsType pointType);

        /// <summary>
        /// Method to read discrete value
        /// </summary>
        /// <param name="address">The address of the point</param>
        /// <param name="pointType">The type of the point</param>
        /// <returns></returns>
        byte ReadDiscreteValue(ushort address, PointsType pointType);

        /// <summary>
        /// Method to check the address is exists
        /// </summary>
        /// <param name="address">The address to be check</param>
        /// <returns></returns>
        bool CheckAddress(ushort address);
    }
}
