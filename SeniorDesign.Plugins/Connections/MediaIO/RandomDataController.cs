using System;
using System.IO;
using SeniorDesign.Core.Connections.MediaIO;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A dummy type of Data Connection where random data is fed
    ///     as a byte stream.
    /// </summary>
    public class RandomDataController : MediaController
    {
        /// <summary>
        ///     The random number generator used to supply data
        /// </summary>
        protected Random RNG = new Random();

        /// <summary>
        ///     Checks if this stream can be read from
        /// </summary>
        public override bool CanRead{  get { return true; } }

        /// <summary>
        ///     Checks if the position of the stream can be changed.
        ///     Random will not allow to simplify finding the length.
        /// </summary>
        public override bool CanSeek { get { return false; } }

        /// <summary>
        ///     Checks if this stream can be written to.
        ///     Random will allow, only as a dummy.
        /// </summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>
        ///     Gets the length of the available stream.
        ///     Random will not allow seeking, so this throws.
        /// </summary>
        public override long Length { get { throw new NotSupportedException(); } }

        /// <summary>
        ///     Gets the current position in the stream.
        ///     Random will not allow seeking, so this throws.
        /// </summary>
        public override long Position {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Flushes all of the input from the buffer to the output.
        /// </summary>
        public override void Flush()
        {
            // Do nothing
        }

        /// <summary>
        ///     Reads from the stream into a byte buffer
        /// </summary>
        /// <param name="buffer">The buffer to read into</param>
        /// <param name="offset">The offset into the buffer to start writing</param>
        /// <param name="count">The number of bytes to read into the buffer</param>
        /// <returns>The number of bytes read</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            // Ensure that the buffer will not overflow
            if (offset + count >= buffer.Length)
                throw new IndexOutOfRangeException($"Cannot read [{count}] more values at an offset of [{offset}] into a buffer of size [{buffer.Length}]");

            // Copy over random data
            var tempBuff = new byte[count];
            RNG.NextBytes(tempBuff);
            Buffer.BlockCopy(tempBuff, 0, buffer, offset, count);
            return count;
        }

        /// <summary>
        ///     Moves to a position in the stream.
        ///     Random does not allow seeking, so this throws.
        /// </summary>
        /// <param name="offset">The position in the stream to move to</param>
        /// <param name="origin">The position to use as the origin for the stream</param>
        /// <returns>The position that was moved to in the stream</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Sets the length of the current stream.
        ///     Random does not allow seeking, so this throws.
        /// </summary>
        /// <param name="value">The length to set the stream.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Writes bytes out to the stream.
        ///     Random supports this as a dummy operation, but it does nothing
        /// </summary>
        /// <param name="buffer">The bytes to write to the stream</param>
        /// <param name="offset">The offset into the buffer to start</param>
        /// <param name="count">The number of bytes to write</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            // Do nothing
        }
    }
}
