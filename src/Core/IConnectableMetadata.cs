using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     Extra state data used by the core to track IConnectables
    /// </summary>
    internal class IConnectableMetadata
    {
        /// <summary>
        ///     A collection of extra data points that were not used in the previous interaction
        /// </summary>
        public readonly IDictionary<IConnectable, DataPacket> LeftoverData = new Dictionary<IConnectable, DataPacket>();

    }
}
