using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Plugins.Util;
using System;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Connections.Converters
{
    /// <summary>
    ///     A simple converter only capable of 1-to-1 byte stream conversion.
    ///     Mostly just for testing.
    /// </summary>
    public class SimpleStreamConverter : DataConverter
    {

        #region User Configuration

        /// <summary>
        ///     The number of bytes per number
        /// </summary>
        [UserConfigurableInteger(
            Name = "Output Type",
            Description = "The minimum value in the range (values below will snap to this)."
        )]
        public int DataSize = 4;

        /// <summary>
        ///     If the data should be considered signed or not
        /// </summary>
        [UserConfigurableBoolean(
            Name = "Signed Mode",
            Description = "If the data should be treated as signed numbers rather than unsigned."
        )]
        public bool Signed = false;

        /// <summary>
        ///     If the input data will be treated as Little Endian or Big Endian.
        ///     A conversion will only be done if this does not match the computer's architecture.
        /// </summary>
        [UserConfigurableBoolean(
            Name = "Little Endian Mode",
            Description = "If the data is stored in Little Endian format rather than Big Endian"
        )]
        public bool LittleEndianMode = BitConverter.IsLittleEndian;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Numeric Byte Stream Converter"; } }

        /// <summary>
        ///     Decodes an input byte stream into seperate values.
        ///     If input is not emptied or set to null, it will be appended with future data
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public override DataPacket DecodeData(ref byte[] input)
        {
            // Only decode when possible
            if (input.Length < DataSize)
                return null;

            // Take out everything possible from the input
            var pieceCount = input.Length / DataSize;
            var toReturn = new DataPacket();
            toReturn.AddChannel();
            for (var k = 0; k < pieceCount; k++)
                toReturn[0].Add(ConversionUtil.BytesToDouble(input, k * DataSize, DataSize, Signed, LittleEndianMode));

            // Set the input to the correct sub-buffer
            var fullSize = pieceCount * DataSize;
            if (fullSize == input.Length)
            {
                input = null;
            }
            else
            {
                var temp = new byte[input.Length - fullSize];
                Buffer.BlockCopy(input, fullSize, temp, 0, temp.Length);
            }

            return toReturn;

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
                    toReturn.AddRange(ConversionUtil.DoubleToBytes(data[k][j], DataSize, Signed, LittleEndianMode));

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
