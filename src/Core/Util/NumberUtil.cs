namespace SeniorDesign.Core.Util
{
    /// <summary>
    ///     A series of static utilities for number manipulation
    /// </summary>
    public static class NumberUtil
    {

        /// <summary>
        ///     Clamps a Double to the range supported by a decimal
        /// </summary>
        /// <param name="d">The double to convert</param>
        /// <returns>The clamped decimal</returns>
        public static decimal ClampToDecimal(double d)
        {
            return d <= (double) decimal.MinValue ? decimal.MinValue : d >= (double) decimal.MaxValue ? decimal.MaxValue : (decimal) d;
        }

    }
}
