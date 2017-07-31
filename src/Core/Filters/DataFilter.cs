using SeniorDesign.Core.Util;
using System;
using System.Collections.Generic;

namespace SeniorDesign.Core.Filters
{
    /// <summary>
    ///     A single filter that accepts input values, and produces output values.
    /// </summary>
    public abstract class DataFilter : IConnectable, IRestorable
    {

        #region IConnectable

        /// <summary>
        ///     The core that this connectable reports back to
        /// </summary>
        public StreamlineCore Core { get; protected set; }

        /// <summary>
        ///     Enables this connectable to be used
        /// </summary>
        public void Enable() { throw new NotImplementedException(); }

        /// <summary>
        ///     Stops this connectable from being used
        /// </summary>
        public void Disable() { }

        /// <summary>
        ///     If this connectable is currently active or not
        /// </summary>
        public bool Enabled { get { return true; } set { } }

        /// <summary>
        ///     An indentifier for this particular object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     The name given to this object by the user to differentiate it
        ///     from the others.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The X position of this module in the block editor
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        ///     The Y position of this module in the block editor
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public abstract int InputCount { get; }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     -1 means the number of outputs match the inputs
        /// </summary>
        public abstract int OutputCount { get; }

        /// <summary>
        ///     The next connections in the connectable graph
        /// </summary>
        public IList<IConnectable> NextConnections { get; protected set; } = new List<IConnectable>();

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public abstract void AcceptIncomingData(StreamlineCore core, DataPacket data);

        /// <summary>
        ///     Ensures that this object is valid before allowing it to be used
        /// </summary>
        /// <returns>True if the object is valid</returns>
        public bool Validate()
        {
            return true;
        }

        #endregion

        /// <summary>
        ///     Creates a new, empty Data Filter
        /// </summary>
        public DataFilter(StreamlineCore core) { Core = core; }

        /// <summary>
        ///     Creates a new Data Filter restored from previously saved bytes
        /// </summary>
        /// <param name="restore">The bytes to restore from</param>
        public DataFilter(StreamlineCore core, byte[] restore, ref int offset)
        {
            Core = core;
            Restore(restore, ref offset);
        }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public abstract int InputLength { get; }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public virtual byte[] ToBytes()
        {
            var toReturn = new List<byte>();

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Id));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Name));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(PositionX));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(PositionY));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public virtual void Restore(byte[] data, ref int offset)
        {
            Id = ByteUtil.GetIntFromSizedArray(data, ref offset);
            Name = ByteUtil.GetStringFromSizedArray(data, ref offset);
            PositionX = ByteUtil.GetIntFromSizedArray(data, ref offset);
            PositionY = ByteUtil.GetIntFromSizedArray(data, ref offset);
        }

        /// <summary>
        ///     Gets this object's name
        /// </summary>
        /// <returns>The user-given name of the Data Filter</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
