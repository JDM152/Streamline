using System;

namespace SeniorDesign.Core.Attributes
{
    /// <summary>
    ///     An attribute used to designate a field that a user can edit
    /// </summary>
    public abstract class UserConfigurableAttribute : Attribute
    {
        /// <summary>
        ///     A name given to this value for the user to see
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     A description for using this field
        /// </summary>
        public string Description { get; set; }
    }
}
