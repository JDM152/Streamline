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
    }
}
