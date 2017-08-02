using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using SeniorDesign.Core.Util;
using System;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A dummy type of Data Connection where random data is fed
    ///     as a byte stream.
    /// </summary>
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false, GenericConverter = false)]
    public class RandomDataStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The lowest random value that is allowed
        /// </summary>
        [UserConfigurableDouble(
            Name = "Minimum",
            Description = "The lowest value allowed"
        )]
        public double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; _off = _maximum - _minimum; }
        }
        private double _minimum = 0;

        /// <summary>
        ///     The highest random value that is allowed
        /// </summary>
        [UserConfigurableDouble(
            Name = "Maximum",
            Description = "The highest value allowed"
        )]
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; _off = _maximum - _minimum; }
        } 
        private double _maximum = 100;

        /// <summary>
        ///     The difference between the min and max (faster calc)
        /// </summary>
        private double _off = 100;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Random Stream"; } }

        /// <summary>
        ///     The random number generator used to supply data
        /// </summary>
        protected Random RNG = new Random();

        /// <summary>
        ///     Checks if this stream can be read from
        /// </summary>
        public override bool CanRead{  get { return true; } }

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

            // Copy over random data
            var tempBuff = new byte[count];
            RNG.NextBytes(tempBuff);
            Buffer.BlockCopy(tempBuff, 0, buffer, offset, count);
            return count;
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
                toReturn[0].Add((RNG.NextDouble() * _off) + Minimum);
            return toReturn;
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            // Start constructing the data array
            var toReturn = new List<byte>(base.ToBytes());

            // Add all of the user configurable options
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Minimum));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Maximum));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            // Restore the base first
            base.Restore(data, ref offset);

            // Restore all of the user configurable options
            Minimum = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
            Maximum = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
        }

    }
}
