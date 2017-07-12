using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Integer components
    /// </summary>
    public partial class StringEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableStringAttribute>
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
        public UserConfigurableStringAttribute Attribute { get; protected set; }

        #endregion

        /// <summary>
        ///     If the value should be allowed to update or not
        /// </summary>
        private bool _suppressUpdate = false;

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public StringEditorComponent(object owner, FieldInfo field, UserConfigurableStringAttribute attribute)
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
            UseTip.SetToolTip(InputText, Attribute.Description);

            // Set the max
            InputText.MaxLength = Attribute.Maximum;

            InputText.Text = (string) Field.GetValue(Owner);
        }

        /// <summary>
        ///     Method triggered whenever the input value changes
        /// </summary>
        private void InputText_TextChanged(object sender, EventArgs e)
        {
            if (_suppressUpdate)
                return;

            // Set real value
            Field.SetValue(Owner, InputText.Text);
        }
    }
}
