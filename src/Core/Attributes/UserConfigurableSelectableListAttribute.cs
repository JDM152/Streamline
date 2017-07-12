namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing a list of object values with strings
    /// </summary>
    public class UserConfigurableSelectableListAttribute : UserConfigurableAttribute
    {
        /// <summary>
        ///     The name and value pairs that can be selected for the list.
        ///     Even indecies are the strings that will be displayed,
        ///     Odd indecies are the objects that correspond to the value.
        ///     Duplicate string names will cause unexpected behaviour.
        /// </summary>
        public object[] Values;
    }
}
