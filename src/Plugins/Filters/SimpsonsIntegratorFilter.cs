using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Attributes.Specialized;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that performs integration on a stream
    /// </summary>
    [RenderIcon(Filename = "Integrator2")]
    public class SimpsonsIntegratorFilter : DataFilter
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
        public double SamplingPeriod { get { return _samplingPeriod * 2.0; } set { _samplingPeriod = value / 2.0; } }
        private double _samplingPeriod = 1.0;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Simpson's Integrator Filter"; } }

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
        private double _y0 = 0.0;

        /// <summary>
        ///     The previous previous data
        /// </summary>
        private double _y1 = 0.0;

        /// <summary>
        ///     The previous previous previous data
        /// </summary>
        private double _y2 = 0.0;

        /// <summary>
        ///     Creates a new Integrator Filter
        /// </summary>
        public SimpsonsIntegratorFilter(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            var toReturn = new DataPacket();
            toReturn.AddChannel();

            while ((BatchMode && data[0].Count > InputLength) || toReturn[0].Count == 0)
            {
                // Calculate the new point
                var nd = _y2 + (data[0][2] + (4.0 * data[0][1]) + data[0][0]) / 3.0;
                _y2 = _y1;
                _y1 = _y0;
                _y0 = nd;

                // Remove the oldest data point
                data.Pop(0);
                toReturn[0].Add(nd);
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
