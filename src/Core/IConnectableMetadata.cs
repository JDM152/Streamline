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
        public readonly DataPacket LeftoverData = new DataPacket();

        /// <summary>
        ///     The connections that provide incoming data
        /// </summary>
        public readonly IList<IConnectable> IncomingConnections = new List<IConnectable>();

    }
}
