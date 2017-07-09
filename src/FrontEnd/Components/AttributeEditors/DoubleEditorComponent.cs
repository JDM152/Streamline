using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Integer components
    /// </summary>
    public partial class DoubleEditorComponent : AttributeEditorComponent<UserConfigurableDoubleAttribute>
    {

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public DoubleEditorComponent(object owner, FieldInfo field, UserConfigurableDoubleAttribute attribute)
        {
            Owner = owner;
            Field = field;
            Attribute = attribute;

            InitializeComponent();
            UpdateComponent();
        }

        /// <summary>
        ///     Updates this attribute editor's properties
        /// </summary>
        public void UpdateComponent()
        {
            // Set the help text
            AttributeName.Text = Attribute.Name;
            UseTip.SetToolTip(InputControl, Attribute.Description);

            // Set the min/max
            InputControl.Minimum = (decimal) Attribute.Minimum;
            InputControl.Maximum = (decimal) Attribute.Maximum;

            InputControl.Value = (decimal) ((double) Field.GetValue(Owner));
        }

        /// <summary>
        ///     Method triggered whenever the input value changes
        /// </summary>
        private void InputControl_ValueChanged(object sender, System.EventArgs e)
        {
            // Set real value
            Field.SetValue(Owner, (int) InputControl.Value);
        }
    }
}
