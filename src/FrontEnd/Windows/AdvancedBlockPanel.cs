using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.FrontEnd.Components.AttributeEditors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A block editor without the graphics.
    ///     Better for performance.
    /// </summary>
    public partial class AdvancedBlockPanel : Form
    {
        /// <summary>
        ///     The core that this panel uses to operate
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     The component currently being viewed
        /// </summary>
        private IConnectable _selected = null;

        /// <summary>
        ///     Creates a new Advanced Block panel with a given core
        /// </summary>
        /// <param name="core">The Streamline core this operates under</param>
        public AdvancedBlockPanel(StreamlineCore core)
        {
            _core = core;

            InitializeComponent();

            ListBlocks();
            ListBlockContent();
        }

        /// <summary>
        ///     Lists all of the available blocks
        /// </summary>
        public void ListBlocks()
        {
            BlockList.Items.Clear();
            foreach (var block in _core.Nodes)
            {
                BlockList.Items.Add(block);
            }
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

        /// <summary>
        ///     Method triggere when the user selects a block to edit
        /// </summary>
        private void BlockList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // List information about the selected block
            _selected = (IConnectable) BlockList.SelectedItem;
            ListBlockContent();
        }
    }
}
