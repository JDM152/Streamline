using System;

namespace SeniorDesign.Core.Util
{
    /// <summary>
    ///     A series of static utilities for manipulating arrays
    /// </summary>
    public static class ArrayUtil
    {
        /// <summary>
        ///     Concatenates one array with another in a new array
        /// </summary>
        /// <typeparam name="T">The type of the arrays</typeparam>
        /// <param name="original">The first array that is being concatentated onto</param>
        /// <param name="toAppend">The second array that is being added</param>
        /// <returns>A new combination of both arrays</returns>
        public static T[] Concat<T>(this T[] original, T[] toAppend)
        {
            var temp = new T[original.Length + toAppend.Length];
            Buffer.BlockCopy(original, 0, temp, 0, original.Length);
            Buffer.BlockCopy(toAppend, 0, temp, original.Length, toAppend.Length);
            return temp;
        }

        /// <summary>
        ///     Returns a copy of a sub-array of a given array.
        /// </summary>
        /// <typeparam name="T">The type of the array</typeparam>
        /// <param name="original">The array to get the sub-array of</param>
        /// <param name="start">The start of the new sub-array</param>
        /// <param name="count">The number of items to get</param>
        /// <returns>A new array containing the sub-array</returns>
        public static T[] Subarray<T>(this T[] original, int start, int count)
        {
            var toReturn = new T[count];
            Buffer.BlockCopy(original, start, toReturn, 0, count);
            return toReturn;
        }
    }
}
