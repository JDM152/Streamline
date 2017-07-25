namespace SeniorDesign.Core
{
    /// <summary>
    ///     An object that can be saved and loaded.
    ///     Note that this type of object must also have
    ///     a constructor that accepts only the byte[] from ToBytes()
    ///     to be used with Streamline.
    /// </summary>
    public interface IRestorable
    {
        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        byte[] ToBytes();

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        void Restore(byte[] data, ref int offset);
    }
}
