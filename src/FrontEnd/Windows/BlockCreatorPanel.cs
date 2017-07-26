using SeniorDesign.Core;
using SeniorDesign.Core.Filters;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A window used to create new blocks
    /// </summary>
    public partial class BlockCreatorPanel : Form
    {

        /// <summary>
        ///     The core of the program being added into
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     The object currently being created
        /// </summary>
        private IConnectable _current;

        /// <summary>
        ///     Mapping between the filter name and creation type
        /// </summary>
        private Dictionary<string, Type> _typeMapping = new Dictionary<string, Type>();

        /// <summary>
        ///     Creates a new Block Creator panel for the specified Streamline Core
        /// </summary>
        public BlockCreatorPanel(StreamlineCore core)
        {
            _core = core;

            InitializeComponent();
            RefreshBlockListings();
        }

        
        /// <summary>
        ///     Method triggered whenever the user changes the type of block
        /// </summary>
        private void BlockTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Delete the old data
            _current = null;

            // Ensure that the type is a Data Filter
            var type = _typeMapping[(string) BlockTypeBox.SelectedItem];
            if (type == null || !typeof(DataFilter).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a DataFilter!");

            // Start creating the skeleton, and display it
            _current = (DataFilter) Activator.CreateInstance(type);
            _current.Name = _current.InternalName;
            BlockViewer.SetViewingComponent(_current);
        }

        /// <summary>
        ///     Refreshes the block types available for creation
        /// </summary>
        private void RefreshBlockListings()
        {
            _typeMapping.Clear();
            BlockTypeBox.Items.Clear();

            foreach (var plugin in _core.Plugins)
            {
                foreach (var connectable in plugin.FilterTypes)
                {
                    _typeMapping.Add(connectable.Key, connectable.Value);
                    BlockTypeBox.Items.Add(connectable.Key);
                }
            }
        }

        #region Saving and Loading

        /// <summary>
        ///     Method triggered when the user hits the "Save Block" menu item
        /// </summary>
        private void saveBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Method triggered when the user hits the "Save Block As" menu item
        /// </summary>
        private void saveBlockAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Method triggered when the user hits the "Load Block" menu item
        /// </summary>
        private void loadBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        #endregion

        /// <summary>
        ///     Method triggered when the user hits the "Save and Close" button
        /// </summary>
        private void AddBlockButton_Click(object sender, EventArgs e)
        {
            // Add the component to the core
            if (_current != null)
                _core.AddConnectable(_current);

            // Close the window
            this.Close();
        }

        /// <summary>
        ///     Method triggered when the user hits the "Close" button
        /// </summary>
        private void CancelCreationButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
