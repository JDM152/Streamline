namespace SeniorDesign.Core.Connections.Converter
{
    /// <summary>
    ///     A configurable converter used in Data Connections to decode strings
    ///     into usable data. This class is allowed to have state for data input
    ///     that is multi-packet.
    /// </summary>
    public class DataConverter
    {
        /// <summary>
        ///     Decodes an input byte stream into seperate values
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public double[][] DecodeData(byte[] input)
        {
            return null;
        }

        /// <summary>
        ///     Encodes input values into a single byte stream
        /// </summary>
        /// <param name="output">The output byte array to convert</param>
        /// <returns>A series of bytes representing the encoded data</returns>
        public byte[] EncodeData(double[][] output)
        {
            return null;
        }

    }
}
