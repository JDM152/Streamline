namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing a selectable File, with several options
    ///     for bounds.
    /// </summary>
    public class UserConfigurableFileAttribute : UserConfigurableAttribute
    {

        /// <summary>
        ///     The filters available for the file selection
        /// </summary>
        public string Filter { get; set; }
    }
}
