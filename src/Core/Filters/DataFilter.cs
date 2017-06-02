namespace SeniorDesign.Core.Filters
{
    /// <summary>
    ///     A single filter that accepts input values, and produces output values.
    /// </summary>
    public abstract class DataFilter
    {
        /// <summary>
        ///     The number of seperate data sets required to use this filter
        /// </summary>
        public abstract int InputFieldCount { get; }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public abstract int InputLength { get; }

        /// <summary>
        ///     The number of seperate data sets that this outputs
        /// </summary>
        public abstract int OutputFieldCount { get; }

        /// <summary>
        ///     Filters incoming data, applying any equations
        /// </summary>
        /// <param name="data">The input data as specified by the filter parameters</param>
        /// <returns>The filtered data</returns>
        public abstract double[][] FilterData(double[][] data);
    }
}
