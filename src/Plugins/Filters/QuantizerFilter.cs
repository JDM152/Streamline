using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;
using System;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that quantizes a signal
    /// </summary>
    public class QuantizerFilter : DataFilter
    {
        #region User Parameters

        /// <summary>
        ///     The lowest value in the range
        /// </summary>
        [UserConfigurableDouble(
            Name = "Minimum",
            Description = "The minimum value in the range (values below will snap to this)."
        )]
        public double Minimum = 0;

        /// <summary>
        ///     The highest value in the range
        /// </summary>
        [UserConfigurableDouble(
            Name = "Maximum",
            Description = "The maximum value in the range (values above will snap to this)."
        )]
        public double Maximum = 100;

        /// <summary>
        ///     The size of the steps
        /// </summary>
        [UserConfigurableDouble(
            Name = "Step Size",
            Description = "The size of each step in the range.",
            Minimum = 0.00000001
        )]
        public double StepSize = 10;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Quantizer"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return 1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        /// </summary>
        public override int OutputCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return 1; } }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        public override void AcceptIncomingData(double[][] data)
        {
            // Return a 1x1 array with the quantized value
            var currentData = data[0];
            var toReturn = new double[1][];
            toReturn[0] = new double[1];
            if (currentData[0] < Minimum)
                toReturn[0][0] = Minimum;
            else if (currentData[0] > Maximum)
                toReturn[0][0] = Maximum;
            else
                toReturn[0][0] = Math.Floor((currentData[0] - Minimum) / StepSize) * StepSize + Minimum;

            // Push to the next node
            foreach (var connection in NextConnections)
                connection.AcceptIncomingData(toReturn);
        }

    }
}
