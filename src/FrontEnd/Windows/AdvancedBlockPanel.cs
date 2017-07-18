﻿using SeniorDesign.Core;
using System.Collections.Generic;
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
        ///     The IConnectable currently being viewed
        /// </summary>
        private IConnectable _selected;

        /// <summary>
        ///     Mapping between index and component in the editor list
        /// </summary>
        private Dictionary<int, IConnectable> _componentMapping = new Dictionary<int, IConnectable>();

        /// <summary>
        ///     Creates a new Advanced Block panel with a given core
        /// </summary>
        /// <param name="core">The Streamline core this operates under</param>
        public AdvancedBlockPanel(StreamlineCore core)
        {
            _core = core;

            InitializeComponent();

            ListBlocks();
        }

        /// <summary>
        ///     Lists all of the available blocks
        /// </summary>
        public void ListBlocks()
        {
            BlockList.Items.Clear();
            _componentMapping.Clear();
            foreach (var block in _core.Nodes)
            {
                BlockList.Items.Add(block);
                _componentMapping.Add(BlockList.Items.IndexOf(block), block);
            }
        }

        /// <summary>
        ///     Method triggere when the user selects a block to edit
        /// </summary>
        private void BlockList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (BlockList.SelectedIndex < 0 || !_componentMapping.ContainsKey(BlockList.SelectedIndex))
                return;

            // List information about the selected block
            _selected = _componentMapping[BlockList.SelectedIndex];
            BlockViewComponent.SetViewingComponent(_selected);
        }

        /// <summary>
        ///     Method triggered when the user selects the "Add Block" button
        /// </summary>
        private void AddBlockButton_Click(object sender, System.EventArgs e)
        {
            // Show the Add Block Panel
            new BlockCreatorPanel(_core).ShowDialog();

            // Refresh the listings
            ListBlocks();
            BlockViewComponent.SetViewingComponent(null);
        }

        /// <summary>
        ///     Method triggered when the usesr selects the "Add Input/Output" button
        /// </summary>
        private void AddIOButton_Click(object sender, System.EventArgs e)
        {
            // Show the Add IO Panel
            new IOBlockCreatorPanel(_core).ShowDialog();

            // Refresh the listings
            ListBlocks();
            BlockViewComponent.SetViewingComponent(null);
        }

        /// <summary>
        ///     Method triggered when the user selects the "Delete Block" button
        /// </summary>
        private void DeleteBlockButton_Click(object sender, System.EventArgs e)
        {
            // Remove the selected block
            if (_selected == null)
                return;
            _core.DeleteConnectable(_selected);
            _selected = null;

            // Refresh the listings
            ListBlocks();
            BlockViewComponent.SetViewingComponent(null);
        }
    }
}
