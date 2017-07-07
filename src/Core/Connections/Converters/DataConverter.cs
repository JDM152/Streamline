namespace SeniorDesign.Core.Connections.Converter
{
    /// <summary>
    ///     A configurable converter used in Data Connections to decode strings
    ///     into usable data. This class is allowed to have state for data input
    ///     that is multi-packet.
    /// </summary>
    public abstract class DataConverter
    {
        /// <summary>
        ///     Decodes an input byte stream into seperate values.
        ///     If input is not emptied, it will be appended to the next round.
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public virtual double[][] DecodeData(ref byte[] input)
        {
            return null;
        }

        /// <summary>
        ///     Encodes input values into a single byte stream.
        ///     If output is not emptied, it will be appended to the next round.
        /// </summary>
        /// <param name="output">The output byte array to convert</param>
        /// <returns>A series of bytes representing the encoded data</returns>
        public virtual byte[] EncodeData(DataPacket output)
        {
            return null;
        }

        /// <summary>
        ///     The number of output streams this converts from a single byte stream
        /// </summary>
        public virtual int DecodeDataCount { get; }

    }
}
