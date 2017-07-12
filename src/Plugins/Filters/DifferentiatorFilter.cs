using SeniorDesign.Core;
using SeniorDesign.Core.Filters;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that performs differentiation on a stream
    /// </summary>
    public class DifferentiatorFilter : DataFilter
    {

        #region User Configuration

        /// <summary>
        ///     The 
        /// </summary>
        public int SamplingPeriod { get; set; }

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Differentiator Filter"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return 1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     -1 means the number of outputs matches the number of inputs
        /// </summary>
        public override int OutputCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return 2; } }

        /// <summary>
        ///     The last return value from this filter
        /// </summary>
        //private double LastValue = 0.0;

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override List<byte> ToBytes()
        {
            // TODO : Finish filter
            return base.ToBytes();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(List<byte> data, ref int offset)
        {
            // TODO : Finish Filter
            base.Restore(data, ref offset);
        }

    }
}
