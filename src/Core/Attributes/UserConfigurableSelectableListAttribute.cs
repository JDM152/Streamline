using System.Collections.Generic;

namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute representing a list of object values with strings
    /// </summary>
    public class UserConfigurableSelectableListAttribute : UserConfigurableAttribute
    {
        /// <summary>
        ///     The name->value pairs that can be selected for the list
        /// </summary>
        public IDictionary<string, object> Values;
    }
}
