namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     Additional metadata for a DataStream class
    /// </summary>
    public class MetadataDataStreamAttribute : MetadataAttribute
    {
        /// <summary>
        ///     If this stream is allowed as an input stream
        /// </summary>
        public bool AllowAsInput { get; set; }

        /// <summary>
        ///     If this stream is allowed as output
        /// </summary>
        public bool AllowAsOutput { get; set; }

        /// <summary>
        ///     If a generic poller is allowed (Defaults to true)
        ///     If not enabled, the poller must be built-in to the stream.
        /// </summary>
        public bool GenericPoller { get; set; } = true;

        /// <summary>
        ///     If a generic converter is allowed (Defaults to true)
        ///     If not enabled, the converter must be built-in to the stream.
        /// </summary>
        public bool GenericConverter { get; set; } = true;
    }
}
