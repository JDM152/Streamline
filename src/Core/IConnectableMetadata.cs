using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     Extra state data used by the core to track IConnectables
    /// </summary>
    internal class IConnectableMetadata
    {
        /// <summary>
        ///     A collection of extra data points that were not used in the previous interaction (input)
        /// </summary>
        public readonly DataPacket LeftoverData = new DataPacket();

        /// <summary>
        ///     The connections that provide incoming data
        /// </summary>
        public readonly IList<IConnectable> IncomingConnections = new List<IConnectable>();

        /// <summary>
        ///     Creates a new set of IConnectableMetadata
        /// </summary>
        public IConnectableMetadata()
        {
            // Always have at least one channel
            LeftoverData.AddChannel();
        }

    }
}
