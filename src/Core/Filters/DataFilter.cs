using SeniorDesign.Core.Util;
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
        ///     If this connectable is currently active or not
        /// </summary>
        public bool Enabled { get; set; }

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

        #endregion

        /// <summary>
        ///     Creates a new, empty Data Filter
        /// </summary>
        public DataFilter() { }

        /// <summary>
        ///     Creates a new Data Filter restored from previously saved bytes
        /// </summary>
        /// <param name="restore">The bytes to restore from</param>
        public DataFilter(List<byte> restore, ref int offset)
        {
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
        public virtual List<byte> ToBytes()
        {
            var toReturn = new List<byte>();

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Name));

            return toReturn;
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public virtual void Restore(List<byte> data, ref int offset)
        {
            Name = ByteUtil.GetStringFromSizedArray(data, ref offset);
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
