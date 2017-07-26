using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections;
using SeniorDesign.FrontEnd.Components.AttributeEditors;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components
{
    /// <summary>
    ///     A component that can edit and auto-update attributes for anything with User Configurable Attributes
    /// </summary>
    public partial class AttributeListComponent : UserControl
    {
        /// <summary>
        ///     The object whose attributes are being edited
        /// </summary>
        private object _owner;

        /// <summary>
        ///     Creates a new list of attribute editors
        /// </summary>
        public AttributeListComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Changes what object is being edited
        /// </summary>
        /// <param name="owner">The object to edit the attributes of</param>
        public void SetComponent(object owner)
        {
            if (_owner == owner) return;

            _owner = owner;
            UpdateAttributeEditors();
        }

        /// <summary>
        ///     Updates the attribute editors that are visible
        /// </summary>
        private void UpdateAttributeEditors()
        {
            // Update the collection of attribute editors on the bottom page
            AttributeEditorList.Controls.Clear();
            if (_owner == null)
                return;

            // Add every available field
            foreach (var field in _owner.GetType().GetFields())
            {
                foreach (var attribute in field.GetCustomAttributes())
                {

                    var configAttrib = attribute as UserConfigurableAttribute;
                    if (configAttrib == null)
                        continue;

                    // Create the editor for the attribute, and add it in
                    var editorType = AttributeEditorHelper.GetEditorForAttribute(configAttrib);
                    var editor = (UserControl) Activator.CreateInstance(editorType, _owner, new WrappedAttributeInfo(field), configAttrib);
                    AttributeEditorList.Controls.Add(editor);
                }
            }

            // Add every available property
            foreach (var prop in _owner.GetType().GetProperties())
            {
                foreach (var attribute in prop.GetCustomAttributes())
                {

                    var configAttrib = attribute as UserConfigurableAttribute;
                    if (configAttrib == null)
                        continue;

                    // Create the editor for the attribute, and add it in
                    var editorType = AttributeEditorHelper.GetEditorForAttribute(configAttrib);
                    var editor = (UserControl) Activator.CreateInstance(editorType, _owner, new WrappedAttributeInfo(prop), configAttrib);
                    AttributeEditorList.Controls.Add(editor);
                }
            }

            // Add a button for recompiling if needed
            var dcc = _owner as IDataConnectionComponent;
            if (dcc != null && dcc.CanCompile)
                AttributeEditorList.Controls.Add(new CompileButtonComponent(dcc));
        }
    }
}
