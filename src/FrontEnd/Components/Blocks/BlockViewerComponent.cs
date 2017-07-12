using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.FrontEnd.Components.AttributeEditors;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks
{
    /// <summary>
    ///     A component used for viewing the data of a given block
    /// </summary>
    public partial class BlockViewerComponent : UserControl
    {

        /// <summary>
        ///     The component currently being viewed
        /// </summary>
        private IConnectable _selected = null;

        /// <summary>
        ///     Creates a new Block Viewer component
        /// </summary>
        public BlockViewerComponent()
        {
            InitializeComponent();
            ListBlockContent();
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(IConnectable component)
        {
            _selected = component;
            ListBlockContent();
        }

        /// <summary>
        ///     Updates the information about the currently selected block
        /// </summary>
        public void ListBlockContent()
        {
            // Only show things if the selected block is available
            if (_selected == null)
            {
                splitContainer1.Hide();
            }
            else
            {
                splitContainer1.Show();

                // Update the data on the top page
                BlockName.Text = _selected.Name;
                BlockTypeName.Text = _selected.InternalName + " : ID " + _selected.Id;

                // Update the collection of attribute editors on the bottom page
                AttributeEditorList.Controls.Clear();
                foreach (var field in _selected.GetType().GetFields())
                {
                    foreach (var attribute in field.GetCustomAttributes())
                    {

                        var configAttrib = attribute as UserConfigurableAttribute;
                        if (configAttrib == null)
                            continue;

                        // Create the editor for the attribute, and add it in
                        var editorType = AttributeEditorHelper.GetEditorForAttribute(configAttrib);
                        var editor = (UserControl) Activator.CreateInstance(editorType, _selected, field, configAttrib);
                        AttributeEditorList.Controls.Add(editor);
                    }
                }
            }
        }
    }
}
