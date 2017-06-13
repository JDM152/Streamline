using System;
using System.IO;

namespace SeniorDesign.Plugins.Util
{
    /// <summary>
    ///     A series of static utilities for manipulating Streams
    /// </summary>
    public static class StreamUtil
    {

        /// <summary>
        ///     Reads all currently available bytes in a stream into an array
        /// </summary>
        /// <param name="stream">The stream to read the bytes from</param>
        /// <param name="maxSize">The maximum size read off the stream in bytes</param>
        /// <returns>A byte array of the available data</returns>
        public static byte[] ReadToEnd(this Stream stream, int maxSize = 0)
        {
            var readBuffer = new byte[maxSize > 4096 || maxSize <= 0 ? 4096 : maxSize];

            var totalBytesRead = 0;
            var bytesRead = 0;

            // Continuously read from the stream to fill the buffer
            while (true)
            {
                bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead);
                if (bytesRead <= 0) break;
                totalBytesRead += bytesRead;

                // Expand the buffer if it becomes full
                if (totalBytesRead == readBuffer.Length)
                {
                    if (totalBytesRead == maxSize)
                        break;

                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        var newSize = readBuffer.Length * 2;
                        if (newSize > maxSize) newSize = maxSize;
                        byte[] temp = new byte[newSize];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte) nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            // Shrink-wrap the array down to the minimum size
            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }

    }
}
