using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using System;
using System.IO;
using System.Reflection;

namespace SeniorDesign.Core.Connections.Streams
{
    /// <summary>
    ///     A stream specialized for use with Streamline
    /// </summary>
    public abstract class DataStream : Stream, IRestorable
    {

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     Verifies that a poller is compatible with this DataStream
        /// </summary>
        /// <param name="poller">The poller to verify</param>
        /// <returns>True if the poller is allowed</returns>
        public virtual bool VerifyPoller(PollingMechanism poller)
        {
            // Don't allow if the poller is built-in
            if (!UsesGenericPollers)
                return false;

            return true;
        }

        /// <summary>
        ///     Verifies that a converter is compatible with this DataStream
        /// </summary>
        /// <param name="converter">The converter to verify</param>
        /// <returns>True if the converter is allowed</returns>
        public virtual bool VerifyConverter(DataConverter converter)
        {
            // Don't allow if the converter is built-in
            if (!UsesGenericConverters)
                return false;

            return true;
        }
        /// <summary>
        ///     If this Data Stream can use a generic poller
        /// </summary>
        public bool UsesGenericPollers { get { return GetType().GetCustomAttribute<MetadataDataStreamAttribute>().GenericPoller; } }

        /// <summary>
        ///     If this Data Stream can use a generic converter
        /// </summary>
        public bool UsesGenericConverters { get { return GetType().GetCustomAttribute<MetadataDataStreamAttribute>().GenericConverter; } }

        /// <summary>
        ///     If this type of data stream supports ReadDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public virtual bool CanReadDirect { get { return false; } }

        /// <summary>
        ///     How many output channels are available when reading directly
        /// </summary>
        public virtual int DirectOutputCount { get { return 0; } }

        /// <summary>
        ///     Reads directly from the stream, ignoring the Converter
        /// </summary>
        /// <param name="count">The number of points to poll</param>
        /// <returns>The data packet with at most count data points added</returns>
        public virtual DataPacket ReadDirect(int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     If this type of data stream supports WriteDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public virtual bool CanWriteDirect { get { return false; } }

        /// <summary>
        ///     Writes directly from the stream, ignoring the Converter
        /// </summary>
        /// <param name="data">The data to write to the stream</param>
        public virtual void WriteDirect(DataPacket data)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Gets the metadata for this particular data stream
        /// </summary>
        /// <returns>The metadata attached to this data stream type</returns>
        public static MetadataDataStreamAttribute GetMetadata()
        {
            return MethodBase.GetCurrentMethod().DeclaringType.GetCustomAttribute<MetadataDataStreamAttribute>();
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public virtual byte[] ToBytes()
        {
            return new byte[0];
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public virtual void Restore(byte[] data, ref int offset)
        {

        }
    }
}
