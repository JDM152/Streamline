using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that computes the rolling average of a given number
    /// </summary>
    public class RollingAverageFilter : DataFilter
    {
        #region User Parameters

        /// <summary>
        ///     The number of points used in calculating the rolling average
        /// </summary>
        [UserConfigurableInteger(
            Name = "History Count",
            Description = "The number of previous steps to use in the average",
            Minimum = 1
        )]
        public int SmoothingFactor = 3;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Rolling Average Filter"; } }

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
        public override int InputLength { get { return SmoothingFactor; } }

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
