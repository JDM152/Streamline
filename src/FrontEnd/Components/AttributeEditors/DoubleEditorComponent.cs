using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Util;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Integer components
    /// </summary>
    public partial class DoubleEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableDoubleAttribute>
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
        public UserConfigurableDoubleAttribute Attribute { get; protected set; }

        #endregion

        /// <summary>
        ///     If the value should be allowed to update or not
        /// </summary>
        private bool _suppressUpdate = false;

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public DoubleEditorComponent(object owner, FieldInfo field, UserConfigurableDoubleAttribute attribute)
        {
            _suppressUpdate = true;
            Owner = owner;
            Field = field;
            Attribute = attribute;

            InitializeComponent();
            UpdateComponent();
            _suppressUpdate = false;
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
            InputControl.Minimum = NumberUtil.ClampToDecimal(Attribute.Minimum);
            InputControl.Maximum = NumberUtil.ClampToDecimal(Attribute.Maximum);

            var realValue = (double) Field.GetValue(Owner);
            InputControl.Value = NumberUtil.ClampToDecimal(realValue);
        }

        /// <summary>
        ///     Method triggered whenever the input value changes
        /// </summary>
        private void InputControl_ValueChanged(object sender, System.EventArgs e)
        {
            // Set real value
            if (!_suppressUpdate)
                Field.SetValue(Owner, (double) InputControl.Value);
        }
    }
}
