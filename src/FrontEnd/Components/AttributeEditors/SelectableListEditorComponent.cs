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
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public SelectableListEditorComponent(object owner, FieldInfo field, UserConfigurableSelectableListAttribute attribute)
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

            // Set the contents
            InputControl.Items.Clear();
            foreach (var item in Attribute.Values.Keys)
                InputControl.Items.Add(item);

            // Set the default selection
            InputControl.SelectedIndex = InputControl.Items.IndexOf(Field.GetValue(Owner));
        }

        /// <summary>
        ///     Method triggered when the selected choice changes
        /// </summary>
        private void InputControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Figure out which was selected, and set the value
            var name = (string) InputControl.Items[InputControl.SelectedIndex];
            Field.SetValue(Owner, Attribute.Values[name]);
        }
    }
}
