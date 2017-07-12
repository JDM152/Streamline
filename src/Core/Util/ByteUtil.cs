using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeniorDesign.Core.Util
{
    /// <summary>
    ///     A series of static utilities for manipulating bytes
    /// </summary>
    public static class ByteUtil
    {
        /// <summary>
        ///     Gets an array of bytes that represent a data string.
        ///     This can later be retrieved using GetStringFromSizedArray.
        ///     This is always Little Endian
        /// </summary>
        /// <param name="data">The data to get the representation for</param>
        /// <returns>A list of bytes for the representation</returns>
        public static List<byte> GetSizedArrayRepresentation(string data)
        {
            var toReturn = new List<byte>();
            var temp = new List<byte>();
            var realData = Encoding.UTF8.GetBytes(data);

            // Tack on the string length as an integer
            temp.AddRange(BitConverter.GetBytes(realData.Length));
            if (!BitConverter.IsLittleEndian)
                temp.Reverse();
            toReturn.AddRange(temp);

            // Add in the UTF-8 encoded data
            toReturn.AddRange(realData);

            return toReturn;
        }

        /// <summary>
        ///     Gets an array of bytes that represent an integer
        ///     This can be retrieved using GetIntFromSizedArray.
        ///     This is always Little Endian
        /// </summary>
        /// <param name="data">The data to get the representation for</param>
        /// <returns>A list of bytes for the representation</returns>
        public static List<byte> GetSizedArrayRepresentation(int data)
        {
            // Convert to bytes, reversing as needed
            var toReturn = new List<byte>();
            toReturn.AddRange(BitConverter.GetBytes(data));
            if (!BitConverter.IsLittleEndian)
                toReturn.Reverse();
            return toReturn;
        }

        /// <summary>
        ///     Gets an array of bytes that represent a double
        ///     This can be retrieved using GetIntFromSizedArray.
        ///     This is always Little Endian
        /// </summary>
        /// <param name="data">The data to get the representation for</param>
        /// <returns>A list of bytes for the representation</returns>
        public static List<byte> GetSizedArrayRepresentation(double data)
        {
            // Convert to bytes, reversing as needed
            return BitConverter.GetBytes(data).ToList();
        }

        /// <summary>
        ///     Gets a string from sized array data given by GetSizedArrayRepresentation
        /// </summary>
        /// <param name="data">The data to read from</param>
        /// <param name="offset">The offset into the data to start</param>
        /// <returns>The original string</returns>
        public static string GetStringFromSizedArray(List<byte> data, ref int offset)
        {
            // Figure out how long the string is
            var strSizeArr = data.GetRange(offset, 4);
            if (!BitConverter.IsLittleEndian)
                strSizeArr.Reverse();
            var strSize = BitConverter.ToInt32(strSizeArr.ToArray(), 0);
            offset += 4;

            // Grab the string from the bytes
            var str = data.GetRange(offset, strSize);
            offset += strSize;

            // Convert back to a string
            return Encoding.UTF8.GetString(str.ToArray());
        }

        /// <summary>
        ///     Gets an integer from a sized array data given by GetSizedArrayRepresentation
        /// </summary>
        /// <param name="data">The data to read from</param>
        /// <param name="offset">The offset into the data to start</param>
        /// <returns>The original integer</returns>
        public static int GetIntFromSizedArray(List<byte> data, ref int offset)
        {
            // Convert to int, reversing as needed
            var i = data.GetRange(offset, 4);
            if (!BitConverter.IsLittleEndian)
                i.Reverse();
            offset += 4;
            return BitConverter.ToInt32(i.ToArray(), 0);
        }

        /// <summary>
        ///     Gets a double from a sized array data given by GetSizedArrayRepresentation
        /// </summary>
        /// <param name="data">The data to read from</param>
        /// <param name="offset">The offset into the data to start</param>
        /// <returns>The original double</returns>
        public static double GetDoubleFromSizedArray(List<byte> data, ref int offset)
        {
            var d = data.GetRange(offset, 8);
            offset += 8;
            return BitConverter.ToDouble(d.ToArray(), 0);
        }
    }
}
