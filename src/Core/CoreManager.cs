using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Connections.MediaIO;
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
    public sealed class CoreManager
    {
        /// <summary>
        ///     The connection manager for input and output data
        /// </summary>
        private DataConnectionManager _dataConnectionManager;

        /// <summary>
        ///     The plugins being used by the program
        /// </summary>
        internal readonly IList<PluginDefinition> Plugins = new List<PluginDefinition>();

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
                using (var filestream = assembly.GetFile("Plugin.xml"))
                    using (var reader = new StreamReader(filestream))
                        xmlContents = reader.ReadToEnd();
            }
            catch
            {
                // Assembly does not have 
                errorStrings.Add("The Plugin.xml file could not be read from the assembly.");
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

                // Get the 'official' name of the object type
                name = node.Attributes["name"].Value;

                // Switch off for some processing based upon the node name
                switch (node.LocalName)
                {
                    case "media":
                        loadType = typeof(MediaController);
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
                    var realType = Type.GetType(className, false, false);
                    if (realType == null)
                    {
                        errorStrings.Add($"The class for {node.LocalName} [{name}], [{className}] could not be found.");
                        continue;
                    }

                    // Switch off for some processing based upon the node name
                    switch (node.LocalName)
                    {
                        case "media":
                            newPlugin.MediaControllerTypes.Add(name, realType);
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
    }
}
