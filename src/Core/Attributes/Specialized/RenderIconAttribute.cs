using System;

namespace SeniorDesign.Core.Attributes.Specialized
{
    /// <summary>
    ///     An attribute that indicates that this object uses an icon when drawn
    /// </summary>
    public class RenderIconAttribute : Attribute
    {
        /// <summary>
        ///     The name of the file for the icon
        /// </summary>
        public string Filename {get;set;}

    }
}
