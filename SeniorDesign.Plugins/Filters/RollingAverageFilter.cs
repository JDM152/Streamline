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
        ///     The number of seperate data sets required to use this filter
        /// </summary>
        public override int InputFieldCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return SmoothingFactor; } }

        /// <summary>
        ///     The number of seperate data sets that this outputs
        /// </summary>
        public override int OutputFieldCount { get { return 1; } }

        /// <summary>
        ///     Filters incoming data, applying any equations
        /// </summary>
        /// <param name="data">The input data as specified by the filter parameters</param>
        /// <returns>The filtered data</returns>
        public override double[][] FilterData(double[][] data)
        {
            // Return a 1x1 array with the average
            var currentData = data[0];
            var toReturn = new double[1][];
            toReturn[0] = new double[1];
            for (var k = 0; k < currentData.Length; k++)
                toReturn[0][0] += currentData[k];
            toReturn[0][0] /= currentData.Length;
            return toReturn;
        }

    }
}
