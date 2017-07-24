using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using System.IO;
using System.Reflection;

namespace SeniorDesign.Core.Connections.Streams
{
    /// <summary>
    ///     A stream specialized for use with Streamline
    /// </summary>
    public abstract class DataStream : Stream
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
        ///     Gets the metadata for this particular data stream
        /// </summary>
        /// <returns>The metadata attached to this data stream type</returns>
        public static MetadataDataStreamAttribute GetMetadata()
        {
            return MethodBase.GetCurrentMethod().DeclaringType.GetCustomAttribute<MetadataDataStreamAttribute>();
        }
    }
}
