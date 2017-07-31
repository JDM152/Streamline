using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
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
        ///     The period for a single 
        /// </summary>
        [UserConfigurableDouble(
            Name = "Sampling Period",
            Description = "The weight given to the difference between points",
            Minimum = 0.00000001
        )]
        public double SamplingPeriod { get { return 2.0 / _samplingPeriod; } set { _samplingPeriod = 2.0 / value; } }
        private double _samplingPeriod = 1.0;

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
        ///     The last output data, used in future calculations
        /// </summary>
        private double LastOutput = 0.0;

        /// <summary>
        ///     Creates a new Differentiator Filter
        /// </summary>
        public DifferentiatorFilter(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Return a new packet with the sum
            var toReturn = new DataPacket();
            toReturn.AddChannel();

            while (data[0].Count >= 2)
            {
                // Calculate the new point
                var nd = _samplingPeriod * data[0][1] - _samplingPeriod * data[0][0] - LastOutput;
                LastOutput = nd;
                toReturn[0].Add(nd);

                // Remove the oldest data point
                data.Pop(0);
            }

            // Push to the next node
            core.PassDataToNextConnectable(this, toReturn);
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            var toReturn = new List<byte>(base.ToBytes());

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(_samplingPeriod));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            base.Restore(data, ref offset);

            _samplingPeriod = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
        }

    }
}
