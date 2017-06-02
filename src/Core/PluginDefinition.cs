using System;
using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     A definition for a single loaded plugin, including every component that is a part of the plugin,
    ///     and some useful help information.
    /// </summary>
    internal class PluginDefinition
    {
        /// <summary>
        ///     The name given to the full plugin
        /// </summary>
        public string Name;

        /// <summary>
        ///     A description of the plugin
        /// </summary>
        public string Description;

        /// <summary>
        ///     A collection of the Media Controllers in this plugin
        /// </summary>
        public IDictionary<string, Type> MediaControllerTypes = new Dictionary<string, Type>();

        /// <summary>
        ///     A collection of the Filters in this plugin
        /// </summary>
        public IDictionary<string, Type> FilterTypes = new Dictionary<string, Type>();
    }
}
