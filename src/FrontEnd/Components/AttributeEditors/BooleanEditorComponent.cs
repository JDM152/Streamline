﻿using SeniorDesign.Core.Attributes;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Boolean components
    /// </summary>
    public partial class BooleanEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableBooleanAttribute>
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
        public UserConfigurableBooleanAttribute Attribute { get; protected set; }

        #endregion

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public BooleanEditorComponent(object owner, FieldInfo field, UserConfigurableBooleanAttribute attribute)
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
