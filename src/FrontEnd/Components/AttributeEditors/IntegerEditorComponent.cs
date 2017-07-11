using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Integer components
    /// </summary>
    public partial class IntegerEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableIntegerAttribute>
    {

        #region IAttributeEditorComponent

        /// <summary>
        ///     The field that is being edited
        /// </summary>
        public FieldInfo Field { get; protected set; }

        /// <summary>
        ///     The object that this is editing the component of
        ///     (Not the field itself, but the object that owns the field)
        /// </summary>
        public object Owner { get; protected set; }

        /// <summary>
        ///     The attribute data for the field being edited
        /// </summary>
        public UserConfigurableIntegerAttribute Attribute { get; protected set; }

        #endregion

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
