﻿using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Selectable List components
    /// </summary>
    public partial class SelectableListEditorComponent : AttributeEditorComponent<UserConfigurableSelectableListAttribute>
    {

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
            UseTip.SetToolTip(CheckBox, Attribute.Description);

            CheckBox.Checked = (bool) Field.GetValue(Owner);
        }

        /// <summary>
        ///     Method triggered whenever the input value changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Set real value
            Field.SetValue(Owner, CheckBox.Checked);
        }
    }
}
