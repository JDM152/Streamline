using System.Collections.Generic;

namespace SeniorDesign.Core.Filters
{
    /// <summary>
    ///     A single filter that accepts input values, and produces output values.
    /// </summary>
    public abstract class DataFilter : IConnectable
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
        public abstract void AcceptIncomingData(double[][] data);

        #endregion

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public abstract int InputLength { get; }
    }
}
