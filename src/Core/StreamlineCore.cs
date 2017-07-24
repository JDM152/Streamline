using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using SeniorDesign.Core.Connections.Streams;
using SeniorDesign.Core.Exceptions;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
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
                        loadType = typeof(DataStream);
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

            // Get a name to call the object if not specified
            if (string.IsNullOrEmpty(obj.Name))
                obj.Name = obj.InternalName + " " + obj.Id;

            _connectableMetadata.Add(obj, new IConnectableMetadata());
        }

        /// <summary>
        ///     Removes a connection from the currently active ones
        /// </summary>
        /// <param name="obj">The connectable to remove</param>
        public void DeleteConnectable(IConnectable obj)
        {
            if (!Nodes.Contains(obj))
                return;

            var mdata = _connectableMetadata[obj];

            // Unregister the node and metadata
            Nodes.Remove(obj);
            obj.Id = -1;
            _connectableMetadata.Remove(obj);

            // Remove the node from all connections
            foreach (var node in Nodes)
            {
                node.NextConnections.Remove(obj);
                _connectableMetadata[node].IncomingConnections.Remove(obj);
            }
        }

        /// <summary>
        ///     Attempts to connect two connectable components.
        ///     This ensures compatibility, and sets some metadata.
        /// </summary>
        /// <param name="original">The component that is the source</param>
        /// <param name="toAdd">The component to connect to the original</param>
        /// <returns>True if the connection was able to be made</returns>
        public bool ConnectConnectables(IConnectable original, IConnectable toAdd)
        {
            // Ensure the connection is legal
            if (!CanConnectConnectables(original, toAdd))
                return false;

            // Make the connection
            original.NextConnections.Add(toAdd);
            _connectableMetadata[toAdd].IncomingConnections.Add(original);
            return true;
        }

        /// <summary>
        ///     Checks to see if two components can connect.
        /// </summary>
        /// <param name="original">The component that is the source</param>
        /// <param name="toAdd">THe component to connect to the original</param>
        /// <returns>If the connection is legal</returns>
        public bool CanConnectConnectables(IConnectable original, IConnectable toAdd)
        {
            // Skip if already connected
            if (original.NextConnections.Contains(toAdd))
                return false;

            // Skip check if arbitrary input
            if (toAdd.InputCount != -1)
            {
                // Ensure that number of channels match
                if (toAdd.InputCount != original.OutputCount)
                    return false;

                // Ensure that nothing else is connected
                if (_connectableMetadata[toAdd].IncomingConnections.Count >= toAdd.InputCount)
                    return false;
            }

            // Can connect
            return true;
        }

        /// <summary>
        ///     Attempts to disconnect two connectable components.
        ///     This ensures compatibility, and sets some metadata
        /// </summary>
        /// <param name="original">The component that is the root</param>
        /// <param name="toRemove">The component that is connected</param>
        /// <returns>True if the component was able to be removed</returns>
        public bool DisconnectConnectables(IConnectable original, IConnectable toRemove)
        {

            // Ensure that the two are actually connected
            if (!original.NextConnections.Contains(toRemove))
                return false;

            // Remove the connection
            original.NextConnections.Remove(toRemove);
            _connectableMetadata[toRemove].IncomingConnections.Remove(original);
            return true;
        }

        /// <summary>
        ///     Gets a list of all the available connections for a particular block
        /// </summary>
        /// <param name="original">The block to get all of the available connections for</param>
        /// <returns>A list of potential connections</returns>
        public List<IConnectable> GetPotentialConnections(IConnectable original)
        {
            // Start with all available nodes
            var toReturn = new List<IConnectable>();
            toReturn.AddRange(Nodes);

            // Remove this node and all of the connections that it already has
            toReturn.Remove(original);
            foreach (var node in original.NextConnections)
                toReturn.Remove(node);

            // Remove all of the nodes that cannot accept more connections
            var toRemove = new List<IConnectable>();
            foreach (var node in toReturn)
                if (!CanConnectConnectables(original, node))
                    toRemove.Add(node);
            foreach (var node in toRemove)
                Nodes.Remove(node);

            return toReturn;
        }

        /// <summary>
        ///     Passes data from a single connection to all available connections,
        ///     performing all translations as needed
        /// </summary>
        /// <param name="root">The IConnetable giving out the data</param>
        /// <param name="data">The data being sent</param>
        public void PassDataToNextConnectable(IConnectable root, double[][] data)
        {
            PassDataToNextConnectable(root, new DataPacket(data));
        }

        /// <summary>
        ///     Passes data from a single connection to all available next connections,
        ///     performing all translations as needed.
        ///     Note that the data packet passed will not be altered at all.
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
                var mdata = meta.GetLeftoverData(connection);
                mdata.Add(data);

                // Do nothing if no data
                if (mdata.ChannelCount == 0)
                    return;

                // Ensure that the channel count is valid
                if (connection.InputCount != -1 && mdata.ChannelCount != connection.InputCount)
                    throw new InvalidChannelCountException($"[{connection.Name}] expected {connection.InputCount} input channels, but was given {mdata.ChannelCount} by [{root.Name}]");

                // Accept the incoming data
                connection.AcceptIncomingData(this, mdata);
            }
        }

        #endregion

        #region Project Schematic Management

        /// <summary>
        ///     Saves core settings to a file
        /// </summary>
        /// <param name="filename">The file to save the core settings to</param>
        public void SaveCoreSettings(string filename)
        {

        }

        /// <summary>
        ///     Loads core settings from a specified file
        /// </summary>
        /// <param name="filename">The file containing the core settings</param>
        public void LoadCoreSettings(string filename)
        {

        }

        /// <summary>
        ///     Saves the schematic for the current project to a file
        /// </summary>
        /// <param name="filename">The file to save the schematic to</param>
        public void SaveProjectSchematic(string filename)
        {
            using (var toSave = File.OpenWrite(filename))
            {

                // Save each of the nodes in the project
                foreach (var node in Nodes)
                {
                    // Only save anything if the node is restorable
                    var restorable = node as IRestorable;
                    if (restorable == null) continue;

                    // Write out that this is a node, and the byte contents of the object
                    var cont = restorable.ToBytes();
                    var contType = ByteUtil.GetSizedArrayRepresentation(node.GetType().AssemblyQualifiedName);
                    toSave.WriteByte(0x01);
                    toSave.Write(contType, 0, contType.Length);
                    toSave.Write(cont, 0, cont.Length);
                }

                // Go through again and save the list of connections for every single node
                foreach (var node in Nodes)
                {
                    // Only save anything if the node is restorable
                    var restorable = node as IRestorable;
                    if (restorable == null) continue;

                    // Write out that this is a connection, and the connected nodes
                    var nodeId = ByteUtil.GetSizedArrayRepresentation(node.Id);
                    var nodeSize = ByteUtil.GetSizedArrayRepresentation(node.NextConnections.Count);
                    toSave.WriteByte(0x02);
                    toSave.Write(nodeId, 0, nodeId.Length);
                    toSave.Write(nodeSize, 0, nodeSize.Length);
                    foreach (var connection in node.NextConnections)
                    {
                        var con = ByteUtil.GetSizedArrayRepresentation(connection.Id);
                        toSave.Write(con, 0, con.Length);
                    }
                }

            }
        }

        /// <summary>
        ///     Loads the schematic from a specified file as the current schematic.
        /// </summary>
        /// <param name="filename">The file to load the schematic from</param>
        public void LoadProjectSchematic(string filename)
        {
            ClearProjectSchematic();
            var nodeMapping = new Dictionary<int, IConnectable>();

            var data = File.ReadAllBytes(filename);
            int pos = 0;

            // Read each byte for instructions
            while (pos < data.Length)
            {
                switch (data[pos++])
                {
                    case 0x01: // Node definition
                        var stype = ByteUtil.GetStringFromSizedArray(data, ref pos);
                        var type = Type.GetType(stype, true, true);
                        var cobj = (IConnectable) Activator.CreateInstance(type);
                        var robj = cobj as IRestorable;
                        robj.Restore(data, ref pos);
                        nodeMapping.Add(cobj.Id, cobj);
                        Nodes.Add(cobj);
                        if (cobj.Id >= _nodeIndex)
                            _nodeIndex = cobj.Id + 1;
                        break;

                    case 0x02: // Connection
                        var nodeId = ByteUtil.GetIntFromSizedArray(data, ref pos);
                        var nodeSize = ByteUtil.GetIntFromSizedArray(data, ref pos);
                        var parentNode = nodeMapping[nodeId];
                        for (var k = 0; k < nodeSize; k++)
                            ConnectConnectables(parentNode, nodeMapping[nodeId]);
                        break;

                    default: // Unknown
                        throw new InvalidSchematicException($"The schematic file {filename} is corrupted, and cannot be loaded.");
                }
            }
        }

        /// <summary>
        ///     Clears everything from the project
        /// </summary>
        public void ClearProjectSchematic()
        {
            var nodeList = new List<IConnectable>(Nodes);
            foreach (var node in nodeList)
                DeleteConnectable(node);

        }

        #endregion
    }
}
