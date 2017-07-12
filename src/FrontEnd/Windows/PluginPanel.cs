using SeniorDesign.Core;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A panel used to view and manipulate plugins
    /// </summary>
    public partial class PluginPanel : Form
    {

        /// <summary>
        ///     The core that this panel operates on
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     The plugin that is currently being viewed
        /// </summary>
        protected PluginDefinition CurrentPlugin;

        /// <summary>
        ///     Creates a new plugin panel for the corresponding Streamline Core
        /// </summary>
        public PluginPanel(StreamlineCore core)
        {
            _core = core;
            InitializeComponent();

            // List all of the plugins
            ListPlugins();
        }

        /// <summary>
        ///     Lists all of the available plugins on the selectable box
        /// </summary>
        public void ListPlugins()
        {
            PluginsList.Items.Clear();
            foreach (var plugin in _core.Plugins)
                PluginsList.Items.Add(plugin.Name);
        }

        /// <summary>
        ///     Lists all of the available contents of the CurrentPlugin plugin
        /// </summary>
        public void ListPluginContent()
        {
            // Clear the tree, bailing if nothing selected
            PluginContentsList.Nodes.Clear();
            if (CurrentPlugin == null)
                return;

            // Add each of the available plugin types
            GenTreeNode(CurrentPlugin.StreamTypes, "Streams");
            GenTreeNode(CurrentPlugin.PollerTypes, "Pollers");
            GenTreeNode(CurrentPlugin.DataConverterTypes, "Data Converters");
            GenTreeNode(CurrentPlugin.FilterTypes, "Filters");
        }

        /// <summary>
        ///     Utility method for adding a dictionary set to a tree
        /// </summary>
        private void GenTreeNode(IDictionary<string, Type> defs, string name)
        {
            var node = new TreeNode(name);
            foreach (var n in defs.Keys)
                node.Nodes.Add(n);

            PluginContentsList.Nodes.Add(node);
        }

        /// <summary>
        ///     Method triggered when the user selects the "Load Plugin" button
        /// </summary>
        private void LoadButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Method triggered when the user selects the "Remove Plugin" button
        /// </summary>
        private void RemovePlugin_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Method triggered when the user selects a plugin to inspect
        /// </summary>
        private void PluginsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Pick out the plugin that is to be viewed
            CurrentPlugin = _core.Plugins[PluginsList.SelectedIndex];
            ListPluginContent();
        }

    }
}
