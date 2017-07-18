using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A window used to create new IO Blocks
    /// </summary>
    public partial class IOBlockCreatorPanel : Form
    {
        /// <summary>
        ///     The core of the program being added into
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     The object currently being created.
        ///     This has all three sub-parts contained within it
        /// </summary>
        private DataConnection _current;

        /// <summary>
        ///     Mapping between the media name and creation type
        /// </summary>
        private Dictionary<string, Type> _typeMappingMedia = new Dictionary<string, Type>();

        /// <summary>
        ///     Mapping between the poller name and creation type
        /// </summary>
        private Dictionary<string, Type> _typeMappingPoller = new Dictionary<string, Type>();

        /// <summary>
        ///     Mapping between the converter name and creation type
        /// </summary>
        private Dictionary<string, Type> _typeMappingConverter = new Dictionary<string, Type>();

        /// <summary>
        ///     Creates a new IO Block Creator panel for the specified Streamline Core
        /// </summary>
        public IOBlockCreatorPanel(StreamlineCore core)
        {
            _core = core;
            _current = new DataConnection();

            InitializeComponent();
            RefreshBlockListings();

            IOBlockViewer.SetViewingComponent(_current);
        }

        /// <summary>
        ///     Refreshes the block types available for creation
        /// </summary>
        private void RefreshBlockListings()
        {
            MediaTypeBox.Items.Clear();
            PollerTypeBox.Items.Clear();
            ConverterTypeBox.Items.Clear();

            foreach (var plugin in _core.Plugins)
            {
                foreach (var connectable in plugin.DataConverterTypes)
                {
                    _typeMappingConverter.Add(connectable.Key, connectable.Value);
                    ConverterTypeBox.Items.Add(connectable.Key);
                }
                foreach (var connectable in plugin.PollerTypes)
                {
                    _typeMappingPoller.Add(connectable.Key, connectable.Value);
                    PollerTypeBox.Items.Add(connectable.Key);
                }
                foreach (var connectable in plugin.StreamTypes)
                {
                    _typeMappingMedia.Add(connectable.Key, connectable.Value);
                    MediaTypeBox.Items.Add(connectable.Key);
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

            // Make the block enabled
            _current.EnablePolling(true);

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

        /// <summary>
        ///     Method triggered when the Media Type box changes
        /// </summary>
        private void MediaTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Delete the old data
            if (_current.MediaConnection != null)
                try
                {
                    _current.MediaConnection.Dispose();
                } catch { }
            _current.MediaConnection = null;

            // Ensure that the type is a Stream
            var type = _typeMappingMedia[(string) MediaTypeBox.SelectedItem];
            if (type == null || !typeof(Stream).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a Stream!");

            // Start creating the skeleton, and display it
            _current.MediaConnection = (Stream) Activator.CreateInstance(type);
            IOBlockViewer.UpdateMediaComponent();
        }

        /// <summary>
        ///     Method triggered when the Poller Type box changes
        /// </summary>
        private void PollerTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Delete the old data
            _current.Poller = null;

            // Ensure that the type is a Poller
            var type = _typeMappingPoller[(string) PollerTypeBox.SelectedItem];
            if (type == null || !typeof(PollingMechanism).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a Polling Mechanism!");

            // Start creating the skeleton, and display it
            _current.Poller = (PollingMechanism) Activator.CreateInstance(type, _core);
            IOBlockViewer.UpdatePollerComponent();
        }

        /// <summary>
        ///     Method triggered when the Converter Type box changes
        /// </summary>
        private void ConverterTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Delete the old data
            _current.Converter = null;

            // Ensure that the type is a Poller
            var type = _typeMappingConverter[(string) ConverterTypeBox.SelectedItem];
            if (type == null || !typeof(DataConverter).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a Data Converter!");

            // Start creating the skeleton, and display it
            _current.Converter = (DataConverter) Activator.CreateInstance(type);
            IOBlockViewer.UpdateConverterComponent();
        }
    }
}
