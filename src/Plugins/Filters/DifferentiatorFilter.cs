using SeniorDesign.Core.Filters;

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
        /// </summary>
        public override int OutputCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return 2; } }

        /// <summary>
        ///     The last return value from this filter
        /// </summary>
        private double LastValue = 0.0;

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        public override void AcceptIncomingData(double[][] data)
        {
            // Return a 1x1 array with the average
            var currentData = data[0];
            var toReturn = new double[1][];
            toReturn[0] = new double[1];
            for (var k = 0; k < currentData.Length; k++)
                toReturn[0][0] += currentData[k];
            toReturn[0][0] /= currentData.Length;

            // Push to the next node
            foreach (var connection in NextConnections)
                connection.AcceptIncomingData(toReturn);
        }

    }
}
