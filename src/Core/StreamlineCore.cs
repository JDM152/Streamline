using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using SeniorDesign.Core.Exceptions;
using SeniorDesign.Core.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     The main control class for the entire program.
    ///     Delegates work off to several smaller manager, each
    ///     responsible for their own work.
    /// </summary>
    public sealed class StreamlineCore
    {
        /// <summary>
        ///     The plugins being used by the program
        /// </summary>
        public readonly IList<PluginDefinition> Plugins = new List<PluginDefinition>();
        
        /// <summary>
        ///     A list of all of the available nodes (inputs, outputs, and filters)
        /// </summary>
        public readonly IList<IConnectable> Nodes = new List<IConnectable>();

        /// <summary>
        ///     The ID to assign the next new node
        /// </summary>
        private int _nodeIndex = 1;

        /// <summary>
        ///     A collection of extra metadata for each IConnectable
        /// </summary>
        private readonly IDictionary<IConnectable, IConnectableMetadata> _connectableMetadata = new Dictionary<IConnectable, IConnectableMetadata>();

        #region Plugin Management

        /// <summary>
        ///     Loads all of the plugin data from a particular assembly
        /// </summary>
        /// <param name="assembly">The assembly to load</param>
        /// <param name="errorStrings">A collection of strings to add any non-fatal errors to</param>
        public void LoadPluginsFromAssembly(Assembly assembly, ICollection<string> errorStrings)
        {
            // Check if the assmebly has the Plugin.xml file
            string xmlContents;
            try
            {
                // Search for the file
                var files = assembly.GetManifestResourceNames();
                string fullName = null;
                foreach (var file in files)
                    if (file.EndsWith("Plugin.xml"))
                    {
                        fullName = file;
                        break;
                    }

                if (string.IsNullOrEmpty(fullName))
                {
                    errorStrings.Add("The Plugin.xml file could not be read from the assembly");
                    return;
                }

                // Load the contents of the file
                using (var filestream = assembly.GetManifestResourceStream(fullName))
                    using (var reader = new StreamReader(filestream))
                        xmlContents = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                // Assembly does not have 
                errorStrings.Add("The Plugin.xml file could not be read from the assembly : " + ex);
                return;
            }

            // Read through the XML file to find the Plugin attribute
            var xml = new XmlDocument();
            xml.LoadXml(xmlContents);

            // Ensure that we have a plugin
            if (xml.DocumentElement.LocalName != "plugin")
            {
                errorStrings.Add("The Plugin.xml file does not have a plugin node as the root node.");
                return;
            }
            var newPlugin = new PluginDefinition();

            // Get the name and description of the plugin
            newPlugin.Name = xml.DocumentElement.GetAttribute("name") ?? "<Unnamed Plugin>";
            newPlugin.Description = xml.DocumentElement.GetAttribute("description") ?? "<No Description>";

            // Go through each of the child nodes
            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                string name, className;
                Type loadType = null;

                // Ignore comments
                if (node.NodeType == XmlNodeType.Comment)
                    continue;

                // Get the 'official' name of the object type
                if (node.Attributes["name"] == null)
                {
                    errorStrings.Add("Unnamed XML node " + node.Name);
                    continue;
                }
                name = node.Attributes["name"].Value;

                // Switch off for some processing based upon the node name
                switch (node.LocalName)
                {
                    case "stream":
                        loadType = typeof(Stream);
                        break;

                    case "poller":
                        loadType = typeof(PollingMechanism);
                        break;

                    case "converter":
                        loadType = typeof(DataConverter);
                        break;

                    case "filter":
                        loadType = typeof(DataFilter);
                        break;

                    default:
                        errorStrings.Add($"Unknown plugin object type [{node.LocalName}]");
                        break;
                }

                // Load a type as needed
                if (loadType != null)
                {
                    className = node.Attributes["class"].Value;
                    if (string.IsNullOrEmpty(className))
                    {
                        errorStrings.Add($"No class has been specified for {node.LocalName} [{name}]");
                        continue;
                    }

                    // Ensure that the type exists
                    var realType = assembly.GetType(className, false, true);
                    if (realType == null)
                    {
                        errorStrings.Add($"The class for {node.LocalName} [{name}], [{className}] could not be found.");
                        continue;
                    }

                    // Switch off for some processing based upon the node name
                    switch (node.LocalName)
                    {
                        case "stream":
                            newPlugin.StreamTypes.Add(name, realType);
                            break;

                        case "poller":
                            newPlugin.PollerTypes.Add(name, realType);
                            break;

                        case "converter":
                            newPlugin.DataConverterTypes.Add(name, realType);
                            break;

                        case "filter":
                            newPlugin.FilterTypes.Add(name, realType);
                            break;

                        default:
                            throw new Exception($"Plugin object type [{node.LocalName}] has been defined as requiring a class type, but lacks some code.");
                    }
                    
                }
            }

            // Add the plugin to the list
            Plugins.Add(newPlugin);
        }

        #endregion

        #region Connectable Management

        /// <summary>
        ///     Adds a new connection to the currently active ones
        /// </summary>
        /// <param name="obj">The connectable to add</param>
        public void AddConnectable(IConnectable obj)
        {
            // Register the node and add some metadata
            if (Nodes.Contains(obj))
                return;

            Nodes.Add(obj);
            obj.Id = _nodeIndex++;

            _connectableMetadata.Add(obj, new IConnectableMetadata());
        }

        /// <summary>
        ///     Removes a connection from the currently active ones
        /// </summary>
        /// <param name="obj">The connectable to remove</param>
        public void DeleteConnectable(IConnectable obj)
        {
            // Unregister the node and metadata
            if (Nodes.Contains(obj))
            {
                Nodes.Remove(obj);
                obj.Id = -1;
                _connectableMetadata.Remove(obj);
            }
        }

        /// <summary>
        ///     Passes data from a single connection to all available next connections,
        ///     performing all translations as needed.
        /// </summary>
        /// <param name="root">The IConnectable giving out the data</param>
        /// <param name="data">The data being sent</param>
        public void PassDataToNextConnectable(IConnectable root, DataPacket data)
        {
            // Grab the metadata for the connectable
            var meta = _connectableMetadata[root];

            // Pass on to each connection available
            foreach (var connection in root.NextConnections)
            {
                // Use the extra data not previously accepted
                var mdata = meta.LeftoverData[connection];
                mdata.Add(data);

                // Ensure that the channel count is valid
                if (connection.InputCount != -1 && data.ChannelCount != connection.InputCount)
                    throw new InvalidChannelCountException($"[{connection.Name}] expected {connection.InputCount} input channels, but was given {data.ChannelCount} by [{root.Name}]");

                // Accept the incoming data
                connection.AcceptIncomingData(this, mdata);
            }
        }

        #endregion
    }
}
