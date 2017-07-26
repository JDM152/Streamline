using SeniorDesign.Core.Connections.Streams;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks.IOBlocks
{
    /// <summary>
    ///     An editor for selecting and modifying streams for an IO Block
    /// </summary>
    public partial class StreamEditorComponent : UserControl
    {

        /// <summary>
        ///     The Stream that is being edited
        /// </summary>
        private DataStream _selected;

        /// <summary>
        ///     Creates a new Stream Editor
        /// </summary>
        public StreamEditorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(DataStream component)
        {
            _selected = component;
            ErrorList.SetComponent(_selected);
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
    }
}
