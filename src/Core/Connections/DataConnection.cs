using SeniorDesign.Core.Enums;

namespace SeniorDesign.Core.Connections
{
    /// <summary>
    ///     A single connection for input or output of data.
    ///     This is technically a container for the media type, 
    ///     the decoding, and the push/pull controller.
    /// </summary>
    public abstract class DataConnection
    {
        /// <summary>
        ///     If this data connection is currently active or not
        /// </summary>
        public virtual bool Enabled { get; set; }

        /// <summary>
        ///     An indentifier for this particular connection.
        ///     This is set whenever the connection has been registered to a manager.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A name for this particular data connection type.
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     The name given to this connection by the user to differentiate it
        ///     from the others.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Flags representing various static options for this data connection
        /// </summary>
        public abstract DataConnectionType ConnectionType { get; }

    }
}
