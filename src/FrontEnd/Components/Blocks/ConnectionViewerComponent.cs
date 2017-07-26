using SeniorDesign.Core;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks
{

    /// <summary>
    ///     An editor that allows the connections for a block to be changed
    /// </summary>
    public partial class ConnectionViewerComponent : UserControl
    {

        /// <summary>
        ///     The connectable currently being edited
        /// </summary>
        private IConnectable _current;

        /// <summary>
        ///     The core that owns the editing connectable
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     Creates a new connection viewer
        /// </summary>
        public ConnectionViewerComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Changes the connectable that is being edited
        /// </summary>
        /// <param name="core">The core the connectable belongs in</param>
        /// <param name="connectable">The connectable that is being edited</param>
        public void SetViewingComponent(StreamlineCore core, IConnectable connectable)
        {
            _core = core;
            _current = connectable;
            ListBlockContent();
        }

        /// <summary>
        ///     Updates the information about the component currently being viewed
        /// </summary>
        public void ListBlockContent()
        {
            BlocksList.Items.Clear();
            ConnectionsList.Items.Clear();
            if (_current == null || _core == null)
            {
                AddButton.Enabled = false;
                RemoveButton.Enabled = false;
                return;
            }

            // Go through and add all of the components that are currently connected
            foreach (var item in _current.NextConnections)
                ConnectionsList.Items.Add(item);

            // Only add the ones to connect that the core approves
            var toAdd = _core.GetPotentialConnections(_current);
            foreach (var item in toAdd)
                BlocksList.Items.Add(item);

            // Only allow adding and removing if any respective items
            AddButton.Enabled = BlocksList.Items.Count > 0;
            RemoveButton.Enabled = ConnectionsList.Items.Count > 0;
        }

        /// <summary>
        ///     Method triggered when the user hits the "Add" button
        /// </summary>
        private void AddButton_Click(object sender, System.EventArgs e)
        {
            // Do nothing if nothing selected to add
            if (BlocksList.SelectedItem == null) return;

            // Add it to the connections
            if (_core.ConnectConnectables(_current, (IConnectable) BlocksList.SelectedItem))
                ListBlockContent();
        }

        /// <summary>
        ///     Method triggered when the user hits the "Remove" button
        /// </summary>
        private void RemoveButton_Click(object sender, System.EventArgs e)
        {
            // Do nothing if nothing selected to remove
            if (ConnectionsList.SelectedItem == null) return;

            // Remove it from the connections
            if (_core.DisconnectConnectables(_current, (IConnectable) BlocksList.SelectedItem))
                ListBlockContent();
        }
    }
}
