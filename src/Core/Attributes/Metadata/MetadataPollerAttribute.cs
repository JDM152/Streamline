namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     Additional metadata for a Poller class
    /// </summary>
    public class MetadataPollerAttribute : MetadataAttribute
    {
        /// <summary>
        ///     If this should be triggered per tick, rather than
        ///     by some sort of internal timing method
        /// </summary>
        public bool TickBased { get; set; } = true;
    }
}
