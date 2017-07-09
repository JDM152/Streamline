using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Integer components
    /// </summary>
    public partial class IntegerEditorComponent : AttributeEditorComponent<UserConfigurableIntegerAttribute>
    {

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public IntegerEditorComponent(object owner, FieldInfo field, UserConfigurableIntegerAttribute attribute)
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
            InputControl.Minimum = Attribute.Minimum;
            InputControl.Maximum = Attribute.Maximum;

            InputControl.Value = (decimal) ((double) Field.GetValue(Owner));
        }

        /// <summary>
        ///     Method triggered whenever the input value changes
        /// </summary>
        private void InputControl_ValueChanged(object sender, System.EventArgs e)
        {
            // Force to Int
            var temp = (int) Math.Floor(InputControl.Value);
            if (temp != InputControl.Value)
            {
                InputControl.Value = temp;
                return;
            }

            // Set real value
            Field.SetValue(Owner, temp);
        }
    }
}
