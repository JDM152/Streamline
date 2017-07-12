using SeniorDesign.Core;
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
            foreach (var block in _core.Nodes)
            {
                BlockList.Items.Add(block);
            }
        }

        /// <summary>
        ///     Method triggere when the user selects a block to edit
        /// </summary>
        private void BlockList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // List information about the selected block
            BlockViewComponent.SetViewingComponent((IConnectable) BlockList.SelectedItem);
        }
    }
}
