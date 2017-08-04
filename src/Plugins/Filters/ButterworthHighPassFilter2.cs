using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Attributes.Specialized;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
using System;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that simulates a low-pass Butterworth analog filter
    /// </summary>
    [RenderIcon(Filename = "ButterworthHighPass2")]
    public class ButterworthHighPassFilter2 : DataFilter
    {

        #region User Configuration

        /// <summary>
        ///     The frequency to cut off for the low pass
        /// </summary>
        [UserConfigurableDouble(
            Name = "Cutoff Frequency",
            Description = "The value at which the cutoff occurs"
        )]
        public double CutoffFrequency
        {
            get { return _cutoffFrequency / (2 * Math.PI); }
            set
            {
                _cutoffFrequency = value * (2 * Math.PI);
                RecalcValues();
            }
        }
        private double _cutoffFrequency = 0;

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

        #region Pre-Calculated Values

        private double _wp = 0;

        private double _b2 = 0;
        private double _b1 = 0;
        private double _b0 = 0;

        private double _a1 = 0;
        private double _a0 = 0;

        /// <summary>
        ///     Recalculates all of the values that are dependent on the user input
        /// </summary>
        private void RecalcValues()
        {
            _wp = (2.0 / _samplingPeriod) * Math.Tan((_cutoffFrequency * _samplingPeriod) / 2.0);
            var twp = _wp * _samplingPeriod;
            var t2wp2 = twp * twp;

            var bottom = 4 + 2 * Math.Sqrt(2) * twp + t2wp2;
            _b2 = 4 / bottom;
            _b1 = -2 * _b2;
            _b0 = _b2;

            _a1 = (2 * t2wp2 - 8) / bottom;
            _a0 = (4 - 2 * Math.Sqrt(2) * twp + t2wp2) / bottom;

        }

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Butterworth 2nd Order Low-Pass Filter"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return 1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     -1 means the inputs match the outputs
        /// </summary>
        public override int OutputCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return 2; } }

        /// <summary>
        ///     The previous output value
        /// </summary>
        private double _y0 = 0;

        /// <summary>
        ///     The previous previous output value
        /// </summary>
        private double _y1 = 0;

        /// <summary>
        ///     Creates a new Butterworth Low-Pass Filter
        /// </summary>
        public ButterworthHighPassFilter2(StreamlineCore core) : base(core) { }

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
                var nd = _b2 * data[0][2] + _b1 * data[0][1] + _b0 * data[0][0] + _a0 * _y0 + _a1 * _y1;
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

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(SamplingPeriod));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(CutoffFrequency));

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

            SamplingPeriod = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
            CutoffFrequency = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
        }

    }
}
