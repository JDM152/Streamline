using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        ///     If the change in the components should be ignored
        /// </summary>
        private bool _ignoreUpdate = false;

        /// <summary>
        ///     If an output type is currently selected
        ///     (To prevent wiping when not changing Input/Output)
        /// </summary>
        private bool _isOutput = false;

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
            _current = new DataConnection(core);
            _current.Name = _current.InternalName;

            InitializeComponent();

            _ignoreUpdate = true;
            InputOutputBox.SelectedIndex = 0;
            _current.IsOutput = false;
            _ignoreUpdate = false;

            IOBlockViewer.AttributeList.AllowEnable = false;
            IOBlockViewer.SetViewingComponent(_current);
            RefreshBlockListings();
        }

        /// <summary>
        ///     Refreshes the block types available for creation
        /// </summary>
        private void RefreshBlockListings()
        {
            var isOutput = InputOutputBox.SelectedIndex > 0;

            var oldConverter = ConverterTypeBox.SelectedItem as Type;
            var oldMedia = MediaTypeBox.SelectedItem as Type;
            var oldPoller = PollerTypeBox.SelectedItem as Type;

            _typeMappingConverter.Clear();
            _typeMappingMedia.Clear();
            _typeMappingPoller.Clear();

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
                    // Prevent any illegal connections from showing up
                    var mdata = connectable.Value.GetCustomAttribute<MetadataDataStreamAttribute>();
                    if (mdata == null || (isOutput && !mdata.AllowAsOutput) || (!isOutput && !mdata.AllowAsInput)) continue;

                    _typeMappingMedia.Add(connectable.Key, connectable.Value);
                    MediaTypeBox.Items.Add(connectable.Key);
                }
            }

            // Restore all of the previously selected values
            _ignoreUpdate = true;

            if (oldConverter != null && ConverterTypeBox.Items.Contains(oldConverter))
                ConverterTypeBox.SelectedItem = oldConverter;
            else
                _current.Converter = null;
            if (oldMedia != null && MediaTypeBox.Items.Contains(oldMedia))
                MediaTypeBox.SelectedItem = oldMedia;
            else
                _current.MediaConnection = null;
            if (oldPoller != null && PollerTypeBox.Items.Contains(oldPoller))
                PollerTypeBox.SelectedItem = oldPoller;
            else
                _current.Poller = null;

            UpdateTypeBoxVisibility();

            IOBlockViewer.UpdateConverterComponent();
            IOBlockViewer.UpdatePollerComponent();



            _ignoreUpdate = false;
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
            // Verify that the block is valid
            if (_current == null || !_current.Validate())
            {
                MessageBox.Show("The new IO Block is incomplete.");
                return;
            }

            // Add the component to the core
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

        /// <summary>
        ///     Method triggered when the Media Type box changes
        /// </summary>
        private void MediaTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreUpdate) return;

            // Delete the old data
            if (_current.MediaConnection != null)
                try
                {
                    _current.MediaConnection.Dispose();
                } catch { }
            _current.MediaConnection = null;

            // Ensure that the type is a Data Stream
            if (MediaTypeBox.SelectedItem == null) return;
            var type = _typeMappingMedia[(string) MediaTypeBox.SelectedItem];
            if (type == null || !typeof(DataStream).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a Data Stream!");

            // Start creating the skeleton, and display it
            _current.MediaConnection = (DataStream) Activator.CreateInstance(type);

            UpdateTypeBoxVisibility();
        }

        /// <summary>
        ///     Updates each of the type boxes to change if they are visible or not
        /// </summary>
        private void UpdateTypeBoxVisibility()
        {
            IOBlockViewer.UpdateMediaComponent();

            // Make everything visible if no chance of collision
            if (_current.MediaConnection == null)
            {
                PollerTypeBox.Show();
                PollerTypeLabel.Show();
                ConverterTypeBox.Show();
                ConverterTypeLabel.Show();
                return;
            }

            // Disable the Poller if non-applicable
            if (!_current.MediaConnection.UsesGenericPollers)
            {
                PollerTypeBox.Hide();
                PollerTypeBox.SelectedIndex = -1;
                PollerTypeLabel.Hide();
            }
            else
            {
                PollerTypeBox.Show();
                PollerTypeLabel.Show();
            }

            // Disable the Converter if non-applicable
            if (!_current.MediaConnection.UsesGenericConverters)
            {
                ConverterTypeBox.Hide();
                ConverterTypeBox.SelectedIndex = -1;
                ConverterTypeLabel.Hide();
            }
            else
            {
                ConverterTypeBox.Show();
                ConverterTypeLabel.Show();
            }
        }

        /// <summary>
        ///     Method triggered when the Poller Type box changes
        /// </summary>
        private void PollerTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreUpdate) return;

            // Delete the old data
            _current.Poller = null;

            // Ensure that the type is a Poller
            if (PollerTypeBox.SelectedItem == null) return;
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
            if (_ignoreUpdate) return;

            // Delete the old data
            _current.Converter = null;

            // Ensure that the type is a Poller
            if (ConverterTypeBox.SelectedItem == null) return;
            var type = _typeMappingConverter[(string) ConverterTypeBox.SelectedItem];
            if (type == null || !typeof(DataConverter).IsAssignableFrom(type))
                throw new Exception("Invalid object type! Not a Data Converter!");

            // Start creating the skeleton, and display it
            _current.Converter = (DataConverter) Activator.CreateInstance(type);
            IOBlockViewer.UpdateConverterComponent();
        }

        /// <summary>
        ///     Method triggered when the Input/Output Type box changes
        /// </summary>
        private void InputOutputBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreUpdate) return;

            // Skip refreshing if the status did not actually change (prevent clearing settings)
            var isOutput = InputOutputBox.SelectedIndex > 0;
            if (isOutput == _isOutput)
                return;

            _isOutput = isOutput;
            _current.IsOutput = _isOutput;

            // Clear the Stream if the type is illegal
            if (_current.MediaConnection != null)
            {
                

                var mdata = _current.MediaConnection.GetType().GetCustomAttribute<MetadataDataStreamAttribute>();
                if (mdata == null || (isOutput && !mdata.AllowAsOutput) || (!isOutput && mdata.AllowAsInput))
                    MediaTypeBox.SelectedIndex = -1;

            }

            // Refresh the listings
            RefreshBlockListings();
        }
    }
}
