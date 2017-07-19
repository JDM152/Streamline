using SeniorDesign.Core.Connections;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks
{

    /// <summary>
    ///     A component used for viewing and assembling input/output blocks
    /// </summary>
    public partial class IOBlockViewerComponent : UserControl
    {

        /// <summary>
        ///     The component currently being viewed
        /// </summary>
        private DataConnection _selected = null;

        /// <summary>
        ///     If the updates to the editor fields should be ignored for now
        /// </summary>
        private bool _ignoreUpdates = false;

        /// <summary>
        ///     Creates a new IO Block viewer
        /// </summary>
        public IOBlockViewerComponent()
        {
            InitializeComponent();

            _ignoreUpdates = true;
            ListBlockContent();
            _ignoreUpdates = false;
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(DataConnection component)
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
                ConverterEditor.SetViewingComponent(null);
            }
            else
            {
                splitContainer1.Show();

                // Update the data on the top page
                BlockName.Text = _selected.Name;

                ConverterEditor.SetViewingComponent(_selected.Converter);
            }
        }

        /// <summary>
        ///     Updates only the contents of the media editor
        /// </summary>
        public void UpdateMediaComponent()
        {
            if (_ignoreUpdates) return;
            StreamEditor.SetViewingComponent(_selected.MediaConnection);
        }

        /// <summary>
        ///     Updates only the contents of the Converter editor
        /// </summary>
        public void UpdateConverterComponent()
        {
            if (_ignoreUpdates) return;
            ConverterEditor.SetViewingComponent(_selected.Converter);
        }

        /// <summary>
        ///     Updates only the contents of the Poller editor
        /// </summary>
        public void UpdatePollerComponent()
        {
            if (_ignoreUpdates) return;
            PollerEditor.SetViewingComponent(_selected.Poller);
        }
    }
}
