using SeniorDesign.Core;
using System;
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
        ///     Creates a new Block Creator panel for the specified Streamline Core
        /// </summary>
        public BlockCreatorPanel(StreamlineCore core)
        {
            _core = core;

            InitializeComponent();
        }

        
        /// <summary>
        ///     Method triggered whenever the user changes the type of block
        /// </summary>
        private void BlockTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Refreshes the block types available for creation
        /// </summary>
        private void RefreshBlockListings()
        {
            BlockTypeBox.Items.Clear();
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


    }
}
