using SeniorDesign.Core;
using SeniorDesign.Core.Connections.Converter;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeniorDesign.Plugins.Connections.Converters
{
    /// <summary>
    ///     A simple converter only capable converting strings to values, with each
    ///     value being seperated by a specific token
    /// </summary>
    public class SimpleStringConverter : DataConverter
    {

        #region User Configuration

        /// <summary>
        ///     The token that is used to determine where numbers end
        /// </summary>
        public string SeperatorToken = ",";

        /// <summary>
        ///     The encoding used in the text (both input and output)
        /// </summary>
        public Encoding StringEncoding = Encoding.UTF8;

        #endregion

        /// <summary>
        ///     Decodes an input byte stream into seperate values.
        ///     If input is not emptied or set to null, it will be appended with future data
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public override double[][] DecodeData(ref byte[] input)
        {
            // TODO : The way Encodings get bytes is strange. Figure out how to get the number of bytes used
            throw new NotImplementedException();

        }

        /// <summary>
        ///     Encodes input values into a single byte stream
        ///     If output is not emptied, it will be appended with future data
        /// </summary>
        /// <param name="output">The output byte array to convert</param>
        /// <returns>A series of bytes representing the encoded data</returns>
        public override byte[] EncodeData(DataPacket data)
        {

            // Go through and convert every double in the array
            var toReturn = new List<byte>();
            for (var k = 0; k < data.ChannelCount; k++)
                for (var j = 0; j < data[k].Count; j++)
                    toReturn.AddRange(StringEncoding.GetBytes(data[k][j] + SeperatorToken));

            // Empty right off the bat
            data.Clear();

            return toReturn.ToArray();
        }

        /// <summary>
        ///     The number of output streams this converts from a single byte stream
        /// </summary>
        public override int DecodeDataCount { get { return 1; } }
    }
}
