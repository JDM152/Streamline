using System;

namespace SeniorDesign.Plugins.Util
{
    /// <summary>
    ///     A series of static utilities for performing type conversions
    /// </summary>
    public static class ConversionUtil
    {

        /// <summary>
        ///     Utility for getting a double from a specified byte count off an array
        /// </summary>
        /// <param name="source">The array of bytes to parse a number from</param>
        /// <param name="offset">The offset into the array to start looking</param>
        /// <param name="count">The size of the numeric type, in bytes</param>
        /// <param name="signed">If the number is signed</param>
        /// <param name="littleEndian">If the data is in little endian mode</param>
        /// <returns>A double representing the parsed number</returns>
        public static double BytesToDouble(byte[] source, int offset, int count, bool signed, bool littleEndian)
        {
            // Figure out if endianess needs to be swapped
            var vals = new byte[count];
            Buffer.BlockCopy(source, offset, vals, 0, count);
            if (littleEndian != BitConverter.IsLittleEndian)
                ReverseArray(ref vals);

            // Switch off by parameters
            if (signed)
            {
                // Signed Numbers
                switch (count)
                {
                    case 1: // SByte
                        return (sbyte) vals[0];
                    case 2: // Short
                        return BitConverter.ToInt16(vals, 0);
                    case 4: // Int
                        return BitConverter.ToInt32(vals, 0);
                    case 8: // Long
                        return BitConverter.ToInt64(vals, 0);
                    default:
                        throw new Exception($"Signed numeric type of {count} bytes not defined.");
                }
            }
            else
            {
                // Unsigned Numbers
                switch (count)
                {
                    case 1: // Byte
                        return vals[0];
                    case 2: // Short
                        return BitConverter.ToUInt16(vals, 0);
                    case 4: // Int
                        return BitConverter.ToUInt32(vals, 0);
                    case 8: // Long
                        return BitConverter.ToUInt64(vals, 0);
                    default:
                        throw new Exception($"Unsigned numeric type of {count} bytes not defined.");
                }
            }
        }

        /// <summary>
        ///     Converts a double value into a byte array of a different type
        /// </summary>
        /// <param name="source">The double to convert</param>
        /// <param name="count">The number of bytes expected</param>
        /// <param name="signed">If the value is signed</param>
        /// <param name="littleEndian">If the value requires little endian order</param>
        /// <returns></returns>
        public static byte[] DoubleToBytes(double source, int count, bool signed, bool littleEndian)
        {
            byte[] toReturn;

            // Switch off by parameters
            if (signed)
            {
                // Signed Numbers
                switch (count)
                {
                    case 1: // SByte
                        toReturn = BitConverter.GetBytes((sbyte) source);
                        break;
                    case 2: // Short
                        toReturn = BitConverter.GetBytes((short) source);
                        break;
                    case 4: // Int
                        toReturn = BitConverter.GetBytes((int) source);
                        break;
                    case 8: // Long
                        toReturn = BitConverter.GetBytes((long) source);
                        break;
                    default:
                        throw new Exception($"Signed numeric type of {count} bytes not defined.");
                }
            }
            else
            {
                // Unsigned Numbers
                switch (count)
                {
                    case 1: // Byte
                        toReturn = new[] { (byte) source };
                        break;
                    case 2: // Short
                        toReturn = BitConverter.GetBytes((ushort) source);
                        break;
                    case 4: // Int
                        toReturn = BitConverter.GetBytes((uint) source);
                        break;
                    case 8: // Long
                        toReturn = BitConverter.GetBytes((ulong) source);
                        break;
                    default:
                        throw new Exception($"Unsigned numeric type of {count} bytes not defined.");
                }
            }

            // Ensure that the endianess does not need to be swapped
            if (littleEndian != BitConverter.IsLittleEndian)
                ReverseArray(ref toReturn);

            return toReturn;
        }

        /// <summary>
        ///     Reverses the contents of an array.
        ///     Excellent for correcting endianess
        /// </summary>
        /// <typeparam name="T">The type of the array to reverse</typeparam>
        /// <param name="arr">The array to reverse</param>
        public static void ReverseArray<T>(ref T[] arr)
        {
            T temp;
            for (var k = 0; k < arr.Length; k++)
            {
                var pnt = arr.Length - k - 1;
                if (pnt == k) continue;

                temp = arr[pnt];
                arr[pnt] = arr[k];
                arr[k] = temp;
            }
        }

    }
}
