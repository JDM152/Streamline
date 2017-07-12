using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    /// <summary>
    ///     A base class for all the Editor components
    /// </summary>
    public interface IAttributeEditorComponent
    {
        /// <summary>
        ///     The field that is being edited
        /// </summary>
        FieldInfo Field { get; }

        /// <summary>
        ///     The object that this is editing the component of
        ///     (Not the field itself, but the object that owns the field)
        /// </summary>
        object Owner { get; }
    }

    /// <summary>
    ///     A base class for all of the Editor components.
    ///     This one actually contains the attribute
    /// </summary>
    public interface  IAttributeEditorComponent<T> : IAttributeEditorComponent where T : UserConfigurableAttribute
    {  
        /// <summary>
        ///     The attribute data for the field being edited
        /// </summary>
        T Attribute { get; }
    }

    /// <summary>
    ///     A series of static utilities for manipulating IAttributeEditorComponents
    /// </summary>
    public static class AttributeEditorHelper
    {
        /// <summary>
        ///     Gets a UserControl used to edit a particular component
        /// </summary>
        /// <param name="attr">The Attribute to get the editor for</param>
        /// <returns>The User Control type required for that attribute</returns>
        public static Type GetEditorForAttribute(UserConfigurableAttribute attr)
        {
            // Select from out list of types
            var type = attr.GetType();
            if (type == typeof(UserConfigurableBooleanAttribute))
                return typeof(BooleanEditorComponent);
            if (type == typeof(UserConfigurableIntegerAttribute))
                return typeof(IntegerEditorComponent);
            if (type == typeof(UserConfigurableDoubleAttribute))
                return typeof(DoubleEditorComponent);
            if (type == typeof(UserConfigurableSelectableListAttribute))
                return typeof(SelectableListEditorComponent);
            if (type == typeof(UserConfigurableStringAttribute))
                return typeof(StringEditorComponent);

            // Throw exception if not found
            throw new InvalidOperationException($"An attribute editor has not been defined for the type [{type}] in the GetEditorForAttribute function.");
        }
    }
}
