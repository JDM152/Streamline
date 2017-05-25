using SeniorDesign.Core.Exceptions;
using System.Collections.Generic;

namespace SeniorDesign.Core.Connections
{
    /// <summary>
    ///     The class responsible for managing all available input and output data connections
    /// </summary>
    public class DataConnectionManager
    {
        /// <summary>
        ///     The input data connections managed by this class
        /// </summary>
        protected IList<DataConnection> InputConnections = new List<DataConnection>();

        /// <summary>
        ///     The output data connections managed by this class
        /// </summary>
        protected IList<DataConnection> OutputConnections = new List<DataConnection>();

        /// <summary>
        ///     Creates a new Data Connection Manager, which will be used to manage all of the incoming
        ///     and outgoing data connections.
        /// </summary>
        public DataConnectionManager()
        {

        }

        /// <summary>
        ///     Registers a new data connection as an input data connection
        /// </summary>
        /// <param name="dataConnection">The data connection to register with the system</param>
        public void RegisterInputDataConnection(DataConnection dataConnection)
        {
            // Ensure that the connection type is valid
            if ((dataConnection.ConnectionType & Enums.DataConnectionType.AllowAsInput) == 0)
                throw new InvalidDataConnectionTypeException($"A connection of type [{dataConnection.InternalName}] cannot be used as an input.");
            if (InputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] is already registered as an input.");
            if (OutputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] is already registered as an output.");

            // Add it to the collection
            InputConnections.Add(dataConnection);
            dataConnection.Id = OutputConnections.IndexOf(dataConnection);
        }

        /// <summary>
        ///     Registers a new data connection as an output data connection
        /// </summary>
        /// <param name="dataConnection">The data connection to register with the system</param>
        public void RegisterOutputDataConnection(DataConnection dataConnection)
        {
            // Ensure that the connection type is valid
            if ((dataConnection.ConnectionType & Enums.DataConnectionType.AllowAsOutput) == 0)
                throw new InvalidDataConnectionTypeException($"A connection of type [{dataConnection.InternalName}] cannot be used as an output.");
            if (OutputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] is already registered as an output.");
            if (InputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] is already registered as an input.");

            // Add it to the collection
            OutputConnections.Add(dataConnection);
            dataConnection.Id = OutputConnections.IndexOf(dataConnection);
        }

        /// <summary>
        ///     Unregisters an input data connection from the system
        /// </summary>
        /// <param name="dataConnection">The data connection to remove</param>
        public void UnregisterInputDataConnection(DataConnection dataConnection)
        {
            // Check if the connection is registered
            if (!InputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] has not been registered as an input.");

            // Remove from the listing
            InputConnections.Remove(dataConnection);
            dataConnection.Id = -1;
        }

        /// <summary>
        ///     Unregisters an output data connection from the system
        /// </summary>
        /// <param name="dataConnection">The data connection to remove</param>
        public void UnregisterOutputDataConnection(DataConnection dataConnection)
        {
            // Check if the connection is registered
            if (!OutputConnections.Contains(dataConnection))
                throw new InvalidOperationException($"The Data Connection [{dataConnection.Name}] has not been registered as an output.");

            // Remove from the listing
            OutputConnections.Remove(dataConnection);
            dataConnection.Id = -1;
        }
    }
}
