using SeniorDesign.Core.Connections.Converter;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks.IOBlocks
{
    /// <summary>
    ///     An editor for creating and modifying converters for IO blocks
    /// </summary>
    public partial class ConverterEditorComponent : UserControl
    {

        /// <summary>
        ///     The Data Converter that is being edited
        /// </summary>
        private DataConverter _selected;

        /// <summary>
        ///     Creates a new Converter Editor
        /// </summary>
        public ConverterEditorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(DataConverter component)
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
    }
}
