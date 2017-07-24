namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing an editable String, with several options
    ///     for bounds.
    /// </summary>
    public class UserConfigurableStringAttribute : UserConfigurableAttribute
    {

        /// <summary>
        ///     The largest number of characters that the user can enter (Defaults to 64)
        /// </summary>
        public int Maximum { get; set; } = 64;
    }
}
