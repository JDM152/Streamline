namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing an editable Integer, with several options
    ///     for bounds.
    /// </summary>
    public class UserConfigurableIntegerAttribute : UserConfigurableAttribute
    {
        /// <summary>
        ///     The smallest number that the user can enter (Defaults to Min Int)
        /// </summary>
        public int Minimum { get; set; } = int.MinValue;

        /// <summary>
        ///     The largest number that the user can enter (Defaults to Max Int)
        /// </summary>
        public int Maximum { get; set; } = int.MaxValue;
    }
}
