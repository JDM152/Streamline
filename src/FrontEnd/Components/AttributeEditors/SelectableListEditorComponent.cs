using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Selectable List components
    /// </summary>
    public partial class SelectableListEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableSelectableListAttribute>
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
        public UserConfigurableSelectableListAttribute Attribute { get; protected set; }

        #endregion

        /// <summary>
        ///     If the value should be allowed to update or not
        /// </summary>
        private bool _suppressUpdate = false;

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public SelectableListEditorComponent(object owner, FieldInfo field, UserConfigurableSelectableListAttribute attribute)
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

            // Set the contents
            InputControl.Items.Clear();
            for (var k = 0; k < Attribute.Values.Length; k+=2)
                InputControl.Items.Add(Attribute.Values[k]);

            // Set the default selection
            InputControl.SelectedIndex = InputControl.Items.IndexOf(Field.GetValue(Owner));
        }

        /// <summary>
        ///     Method triggered when the selected choice changes
        /// </summary>
        private void InputControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressUpdate)
                return;

            // Figure out which was selected, and set the value
            var name = (string) InputControl.Items[InputControl.SelectedIndex];
            for (var k = 0; k < Attribute.Values.Length; k += 2)
                if ((string) Attribute.Values[k] == name)
                {
                    Field.SetValue(Owner, Attribute.Values[k+1]);
                    return;
                }
        }
    }
}
