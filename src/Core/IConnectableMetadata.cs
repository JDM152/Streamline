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
        protected readonly IDictionary<IConnectable, DataPacket> LeftoverData = new Dictionary<IConnectable, DataPacket>();

        /// <summary>
        ///     The connections that provide incoming data
        /// </summary>
        public readonly IList<IConnectable> IncomingConnections = new List<IConnectable>();

        /// <summary>
        ///     Gets the leftover data for a particular connectable.
        ///     Any data added to the returned DataPacket will be saved for next iteration.
        /// </summary>
        /// <param name="connection">The data connection to get the leftovers for</param>
        /// <returns>The leftover data packets for the connection</returns>
        public DataPacket GetLeftoverData(IConnectable connection)
        {
            if (!LeftoverData.ContainsKey(connection))
                LeftoverData.Add(connection, new DataPacket());

            return LeftoverData[connection];
        }

    }
}
