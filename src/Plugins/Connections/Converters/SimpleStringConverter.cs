using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Util;
using SeniorDesign.Plugins.Enums;
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
        [UserConfigurableString(
            Name = "Seperator Token",
            Description = "The token that seperates each value"
        )]
        public string SeperatorToken = ",";

        /// <summary>
        ///     The encoding used in the text (both input and output)
        /// </summary>
        [UserConfigurableSelectableList(
            Name = "String Encoding",
            Description = "The encoding used when reading/writing strings",
            Values = new object[] {
                 "UTF8", EncodingEnum.UTF8,
                 "ASCII", EncodingEnum.ASCII,
                 "Unicode", EncodingEnum.Unicode,
                 "UTF32", EncodingEnum.UTF32,
            }    
        )]
        public EncodingEnum StringEncoding
        {
            get { return EncodingEnumUtil.EncodingToEnum(_stringEncoding); }
            set { _stringEncoding = EncodingEnumUtil.EnumToEncoding(value); }
        }
        private Encoding _stringEncoding = Encoding.UTF8;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "String Byte Stream Converter"; } }

        /// <summary>
        ///     Decodes an input byte stream into seperate values.
        ///     If input is not emptied or set to null, it will be appended with future data
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public override DataPacket DecodeData(ref byte[] input)
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
                    toReturn.AddRange(_stringEncoding.GetBytes(data[k][j] + SeperatorToken));

            // Empty right off the bat
            data.Clear();

            return toReturn.ToArray();
        }

        /// <summary>
        ///     The number of output streams this converts from a single byte stream
        /// </summary>
        public override int DecodeDataCount { get { return 1; } }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            var toReturn = new List<byte>(base.ToBytes());

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(SeperatorToken));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation((int) StringEncoding));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            base.Restore(data, ref offset);

            SeperatorToken = ByteUtil.GetStringFromSizedArray(data, ref offset);
            StringEncoding = (EncodingEnum) ByteUtil.GetIntFromSizedArray(data, ref offset);
        }
    }
}
