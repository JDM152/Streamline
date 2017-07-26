using SeniorDesign.Core;
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
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false, GenericConverter = false)]
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
        ///     If this type of data stream supports ReadDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public override bool CanReadDirect { get { return true; } }

        /// <summary>
        ///     Reads directly from the stream, ignoring the Converter, and providing 
        /// </summary>
        /// <param name="count">The number of points to poll</param>
        /// <returns>The data packet with at most count data points added</returns>
        public override DataPacket ReadDirect(int count)
        {
            var toReturn = new DataPacket();
            toReturn.AddChannel();
            while (count-- > 0)
                toReturn[0].Add(_value);
            return toReturn;
        }

    }
}
