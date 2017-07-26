
using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     An interface for objects that can be chained together,
    ///     that take some data input and give some data output.
    /// </summary>
    public interface IConnectable
    {

        /// <summary>
        ///     If this connectable is currently active or not
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     An indentifier for this particular object.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        string InternalName { get; }

        /// <summary>
        ///     The X position of this module in the block editor
        /// </summary>
        int PositionX { get; set; }

        /// <summary>
        ///     The Y position of this module in the block editor
        /// </summary>
        int PositionY { get; set; }

        /// <summary>
        ///     The name given to this object by the user to differentiate it
        ///     from the others.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        int InputCount { get; }

        /// <summary>
        ///     The number of output connections this connectable provides.
        /// </summary>
        int OutputCount { get; }

        /// <summary>
        ///     The next connections in the connectable graph
        /// </summary>
        IList<IConnectable> NextConnections { get; }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="core">The core that is passing the data in</param>
        /// <param name="data">The data being pushed from the previous node.</param>
        /// <remarks>If data is not emptied, it will be added onto and passed next cycle</remarks>
        void AcceptIncomingData(StreamlineCore core, DataPacket data);

        /// <summary>
        ///     Ensures that this object is valid before allowing it to be used
        /// </summary>
        /// <returns>True if the object is valid</returns>
        bool Validate();

    }
}
