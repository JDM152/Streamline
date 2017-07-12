using System.Text;

namespace SeniorDesign.Plugins.Enums
{
    /// <summary>
    ///     An enum representing supported encoding types
    /// </summary>
    public enum EncodingEnum
    {
        UTF8,
        UTF32,
        ASCII,
        Unicode
    }

    /// <summary>
    ///     A series of static utiltites for the Encoding Enum
    /// </summary>
    public static class EncodingEnumUtil
    {
        /// <summary>
        ///     Converts an encoding Enum back into an encoding
        /// </summary>
        /// <param name="e">The Enum to convert</param>
        /// <returns>The encoding represented by the enum</returns>
        public static Encoding EnumToEncoding(EncodingEnum e)
        {
            switch (e)
            {
                case EncodingEnum.ASCII:    return Encoding.ASCII;
                case EncodingEnum.UTF8:     return Encoding.UTF8;
                case EncodingEnum.UTF32:    return Encoding.UTF32;
                case EncodingEnum.Unicode:  return Encoding.Unicode;
            }

            throw new System.Exception($"Unsupported Encoding Enum [{e}]. Did you forget to add it to the EnumToEncoding utility?");
        }

        /// <summary>
        ///     Converts an Encoding to an encoding Enum
        /// </summary>
        /// <param name="e">The encoding to convert</param>
        /// <returns>The Enum representing the encoding</returns>
        public static EncodingEnum EncodingToEnum(Encoding e)
        {
            if (e == Encoding.ASCII)    return EncodingEnum.ASCII;
            if (e == Encoding.UTF8)     return EncodingEnum.UTF8;
            if (e == Encoding.UTF32)    return EncodingEnum.UTF32;
            if (e == Encoding.Unicode)  return EncodingEnum.Unicode;

            throw new System.Exception($"Unsupported Encoding [{e}].");
        }

    }
}
