namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing an editable Double, with several options
    ///     for bounds.
    /// </summary>
    public class UserConfigurableDoubleAttribute : UserConfigurableAttribute
    {
        /// <summary>
        ///     The smallest number that the user can enter (Defaults to Min Int)
        /// </summary>
        public double Minimum { get; set; } = double.MinValue;

        /// <summary>
        ///     The largest number that the user can enter (Defaults to Max Int)
        /// </summary>
        public double Maximum { get; set; } = double.MaxValue;
    }
}
