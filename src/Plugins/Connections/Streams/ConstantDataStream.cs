using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.IO;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A dummy type of Data Connection where constant data is fed
    ///     as a byte stream.
    /// </summary>
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false)]
    public class ConstantDataStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The value that this stream outputs
        /// </summary>
        [UserConfigurableDouble(
            Name = "Value",
            Description = "The value the stream will output"
        )]
        public double Value {
            get { return _value; }
            set
            {
                _value = value;
                _valueBytes = BitConverter.GetBytes(value);
            }
        }

        private double _value = 0.0;
        private byte[] _valueBytes = BitConverter.GetBytes(0.0);

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Constant Stream"; } }

        /// <summary>
        ///     Checks if this stream can be read from
        /// </summary>
        public override bool CanRead { get { return true; } }

        /// <summary>
        ///     Checks if the position of the stream can be changed.
        ///     Constant will not allow to simplify finding the length.
        /// </summary>
        public override bool CanSeek { get { return false; } }

        /// <summary>
        ///     Checks if this stream can be written to.
        ///     Constant will allow, only as a dummy.
        /// </summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>
        ///     Gets the length of the available stream.
        ///     Constant will not allow seeking, so this throws.
        /// </summary>
        public override long Length { get { throw new NotSupportedException(); } }

        /// <summary>
        ///     Gets the current position in the stream.
        ///     Constant will not allow seeking, so this throws.
        /// </summary>
        public override long Position
        {
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
            if (offset + count > buffer.Length)
                throw new IndexOutOfRangeException($"Cannot read [{count}] more values at an offset of [{offset}] into a buffer of size [{buffer.Length}]");

            // Copy over data, not surpassing the limit
            var realCount = 0;
            while (offset + _valueBytes.Length < count)
            {
                Buffer.BlockCopy(_valueBytes, 0, buffer, offset, _valueBytes.Length);
                offset += _valueBytes.Length;
                realCount += _valueBytes.Length;
            }
            return realCount;
        }

        /// <summary>
        ///     Moves to a position in the stream.
        ///     Constant does not allow seeking, so this throws.
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
        ///     Constant does not allow seeking, so this throws.
        /// </summary>
        /// <param name="value">The length to set the stream.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Writes bytes out to the stream.
        ///     Constant supports this as a dummy operation, but it does nothing
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
