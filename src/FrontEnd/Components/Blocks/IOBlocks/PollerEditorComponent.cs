using SeniorDesign.Core.Connections.Pollers;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks.IOBlocks
{
    /// <summary>
    ///     An editor for selecting and modifying streams for an IO Block
    /// </summary>
    public partial class PollerEditorComponent : UserControl
    {

        /// <summary>
        ///     The Polling Mechanism that is being edited
        /// </summary>
        private PollingMechanism _selected;

        /// <summary>
        ///     Creates a new Poller Editor
        /// </summary>
        public PollerEditorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(PollingMechanism component)
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
                AttributeList.SetComponent(null);
            }
            else
            {
                splitContainer1.Show();

                // Update the data on the top page
                BlockTypeName.Text = _selected.InternalName;

                AttributeList.SetComponent(_selected);
            }
        }

        /// <summary>
        ///     Clears the selected component
        /// </summary>
        public void ClearChoice()
        {
            _selected = null;
            ListBlockContent();
        }
    }
}
