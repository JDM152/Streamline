using SeniorDesign.Core.Connections;
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
using System.Threading;
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
        ///     The time between ticks in milliseconds.
        ///     Set to 0 for continuous ticking.
        /// </summary>
        public int TickTime = 100;

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

        /// <summary>
        ///     The list of connectables that start every cycle
        /// </summary>
        private readonly List<PollingMechanism> _tickers = new List<PollingMechanism>();

        /// <summary>
        ///     The position into the tickers list that this is currently on
        /// </summary>
        private int _tickersPosition = -1;

        /// <summary>
        ///     If the tickers should be polled instead of the filters
        /// </summary>
        private bool _tickerMode = true;

        /// <summary>
        ///     The queue of IConnectables that need to be executed next
        /// </summary>
        private readonly Queue<IConnectable> _executionQueue = new Queue<IConnectable>();

        /// <summary>
        ///     The timer used to schedule ticks
        /// </summary>
        private Timer _tickTimer;

        #region Events

        /// <summary>
        ///     Event triggered whenever a new block is added
        /// </summary>
        public event EventHandler<IConnectable> OnBlockAdded;

        /// <summary>
        ///     Event triggered whenever a block is deleted
        /// </summary>
        public event EventHandler<IConnectable> OnBlockDeleted;

        /// <summary>
        ///     Event triggered whenever two blocks are connected
        /// </summary>
        public event EventHandler<Tuple<IConnectable, IConnectable>> OnBlocksConnected;

        /// <summary>
        ///     Event triggered whenever two blocks are disconnected
        /// </summary>
        public event EventHandler<Tuple<IConnectable, IConnectable>> OnBlocksDisconnected;

        /// <summary>
        ///     Event triggered whenever a block is enabled
        /// </summary>
        public event EventHandler<IConnectable> OnBlockEnabled;

        /// <summary>
        ///     Event triggered whenever a block is disabled
        /// </summary>
        public event EventHandler<IConnectable> OnBlockDisabled;

        /// <summary>
        ///     Method triggered whenever a block is ticked
        /// </summary>
        public event EventHandler<IConnectable> OnBlockActivated;

        #endregion

        /// <summary>
        ///     Creates a new Streamline Core
        /// </summary>
        public StreamlineCore()
        {
            // Create the tick timer, but don't start it yet
            _tickTimer = new Timer((o) => TickCycle(), null, Timeout.Infinite, Timeout.Infinite);
        }

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
        ///     Continues ticking for the current cycle
        /// </summary>
        private void TickCycle()
        {
            // Stop polling if nothing left anywhere
            if (_executionQueue.Count <= 0 && _tickers.Count <= 0)
                return;

            // Perform the polling as needed
            if (_executionQueue.Count <= 0)
                _tickersPosition = 0;

            if (_tickerMode)
            {
                // Continue polling where left off last poll
                while (_tickersPosition < _tickers.Count)
                {
                    // Attempt to run the next available ticker
                    if (_tickers[_tickersPosition].Connection.Enabled)
                    {
                        try
                        {
                            OnBlockActivated?.Invoke(this, _tickers[_tickersPosition].Connection);
                            _tickers[_tickersPosition++].Poll();
                            break;
                        }
                        catch (Exception ex)
                        {
                            DisableConnectable(_tickers[_tickersPosition++].Connection);
                        }
                    }
                }

                // If no poller found, must be at end of list. Make way for the executables
                if (_tickersPosition >= _tickers.Count)
                {
                    _tickersPosition = -1;
                    _tickerMode = false;
                }

            }
            else
            {
                // Move on and execute 
                while (_executionQueue.Count > 0)
                {
                    var node = _executionQueue.Dequeue();
                    if (!node.Enabled) continue;

                    // Accept the incoming data
                    try
                    {
                        OnBlockActivated?.Invoke(this, node);
                        node.AcceptIncomingData(this, _connectableMetadata[node].LeftoverData);
                    }
                    catch (Exception ex)
                    {
                        DisableConnectable(node);
                    }

                    // Only break out if schedule a tick later
                    if (TickTime > 0)
                        break;
                }

                // Start with the inputs again next time
                if (_executionQueue.Count <= 0)
                    _tickerMode = true; 
            }

            // Schedule the next tick
            _tickTimer.Change(TickTime, Timeout.Infinite);
        }

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

            // Check to see if the connection has a ticker
            var dc = obj as DataConnection;
            if (dc != null && dc.Poller != null && dc.Poller.IsTickPoller)
            {
                // Add the ticker and start the timer as needed
                _tickers.Add(dc.Poller);
                if (_tickers.Count == 1)
                    _tickTimer.Change(TickTime, Timeout.Infinite);
            }

            OnBlockAdded?.Invoke(this, obj);
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

            // Remove the node from all connections
            foreach (var node in Nodes)
            {
                DisconnectConnectables(obj, node);
                DisconnectConnectables(node, obj);
            }

            // Unregister the node and metadata
            Nodes.Remove(obj);
            obj.Id = -1;
            _connectableMetadata.Remove(obj);

            // Check to see if the connection has a ticker
            var dc = obj as DataConnection;
            if (dc != null && dc.Poller != null && dc.Poller.IsTickPoller)
            {
                _tickers.Remove(dc.Poller);
                if (_tickers.Count <= 0)
                    _tickTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            OnBlockDeleted?.Invoke(this, obj);
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
            OnBlocksConnected?.Invoke(this, new Tuple<IConnectable, IConnectable>(original, toAdd));
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
            OnBlocksDisconnected?.Invoke(this, new Tuple<IConnectable, IConnectable>(original, toRemove));
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
                toReturn.Remove(node);

            return toReturn;
        }

        /// <summary>
        ///     Enables a connectable so that it can be used
        /// </summary>
        /// <param name="toEnable">The connectable to enable</param>
        public void EnableConnectable(IConnectable toEnable)
        {
            toEnable.Enabled = true;
            OnBlockEnabled?.Invoke(this, toEnable);
        }

        /// <summary>
        ///     Enables a connectable so that it can be used
        /// </summary>
        /// <param name="toDisable">The connectable to enable</param>
        public void DisableConnectable(IConnectable toDisable)
        {
            toDisable.Enabled = false;
            OnBlockDisabled?.Invoke(this, toDisable);
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
            // Pass on to each connection available
            foreach (var connection in root.NextConnections)
            {
                // Add the data
                _connectableMetadata[connection].LeftoverData.Add(data);

                // Enqueue the new node to be run
                _executionQueue.Enqueue(connection);
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
            if (File.Exists(filename))
                File.Delete(filename);
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

                    // Append additional data for specialized types
                    var dc = node as DataConnection;
                    if (dc != null)
                    {
                        // Add the media connection only if it exists
                        if (dc.MediaConnection != null)
                        {
                            cont = dc.MediaConnection.ToBytes();
                            contType = ByteUtil.GetSizedArrayRepresentation(dc.MediaConnection.GetType().AssemblyQualifiedName);
                            toSave.WriteByte(0x03);
                            toSave.Write(contType, 0, contType.Length);
                            toSave.Write(cont, 0, cont.Length);
                        }

                        // Add the poller only if it exists
                        if (dc.Poller != null)
                        {
                            cont = dc.Poller.ToBytes();
                            contType = ByteUtil.GetSizedArrayRepresentation(dc.Poller.GetType().AssemblyQualifiedName);
                            toSave.WriteByte(0x04);
                            toSave.Write(contType, 0, contType.Length);
                            toSave.Write(cont, 0, cont.Length);
                        }

                        // Add the converter only if it exists
                        if (dc.Converter != null)
                        {
                            cont = dc.Converter.ToBytes();
                            contType = ByteUtil.GetSizedArrayRepresentation(dc.Converter.GetType().AssemblyQualifiedName);
                            toSave.WriteByte(0x05);
                            toSave.Write(contType, 0, contType.Length);
                            toSave.Write(cont, 0, cont.Length);
                        }
                    }
                }

                // Go through again and save the list of connections for every single node
                foreach (var node in Nodes)
                {
                    // Only save anything if the node is restorable
                    var restorable = node as IRestorable;
                    if (restorable == null) continue;
                    if (node.NextConnections.Count <= 0) continue;

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
            IConnectable lastConnectable = null;
            DataConnection lastDataConnection = null;

            var data = File.ReadAllBytes(filename);
            int pos = 0;
            string stype;
            Type type;

            var errorStr = $"The schematic file {filename} is corrupted, and cannot be loaded.";

            // Read each byte for instructions
            while (pos < data.Length)
            {
                switch (data[pos++])
                {
                    case 0x01: // Node definition
                        stype = ByteUtil.GetStringFromSizedArray(data, ref pos);
                        type = Type.GetType(stype, true, true);
                        var cobj = (IConnectable) Activator.CreateInstance(type, this);
                        var robj = cobj as IRestorable;
                        robj.Restore(data, ref pos);
                        nodeMapping.Add(cobj.Id, cobj);
                        AddConnectable(cobj);
                        if (cobj.Id >= _nodeIndex)
                            _nodeIndex = cobj.Id + 1;

                        lastConnectable = cobj;
                        lastDataConnection = cobj as DataConnection;
                        break;

                    case 0x02: // Connection
                        var nodeId = ByteUtil.GetIntFromSizedArray(data, ref pos);
                        var nodeSize = ByteUtil.GetIntFromSizedArray(data, ref pos);
                        var parentNode = nodeMapping[nodeId];
                        for (var k = 0; k < nodeSize; k++)
                        {
                            nodeId = ByteUtil.GetIntFromSizedArray(data, ref pos);
                            ConnectConnectables(parentNode, nodeMapping[nodeId]);
                        }
                        break;

                    case 0x03: // DataConnection->MediaConnection
                        if (lastDataConnection == null)
                            throw new InvalidSchematicException(errorStr);

                        stype = ByteUtil.GetStringFromSizedArray(data, ref pos);
                        type = Type.GetType(stype, true, true);
                        var mobj = (DataStream) Activator.CreateInstance(type);
                        lastDataConnection.MediaConnection = mobj;

                        break;

                    case 0x04: // DataConnection->Poller
                        if (lastDataConnection == null)
                            throw new InvalidSchematicException(errorStr);

                        stype = ByteUtil.GetStringFromSizedArray(data, ref pos);
                        type = Type.GetType(stype, true, true);
                        var pobj = (PollingMechanism) Activator.CreateInstance(type, this);
                        lastDataConnection.Poller = pobj;

                        break;

                    case 0x05: // DataConnection->Converter
                        if (lastDataConnection == null)
                            throw new InvalidSchematicException(errorStr);

                        stype = ByteUtil.GetStringFromSizedArray(data, ref pos);
                        type = Type.GetType(stype, true, true);
                        var cvobj = (DataConverter) Activator.CreateInstance(type);
                        lastDataConnection.Converter = cvobj;

                        break;

                    default: // Unknown
                        throw new InvalidSchematicException(errorStr);
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
