using SeniorDesign.Core.Attributes;
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
        ///     Gets the metadata for this particular data stream
        /// </summary>
        /// <returns>The metadata attached to this data stream type</returns>
        public static MetadataDataStreamAttribute GetMetadata()
        {
            return MethodBase.GetCurrentMethod().DeclaringType.GetCustomAttribute<MetadataDataStreamAttribute>();
        }
    }
}
