using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.MediaIO;

namespace SeniorDesign.Core.Connections
{
    /// <summary>
    ///     A single connection for input or output of data.
    ///     This is technically a container for the media type, 
    ///     the decoding, and the push/pull controllers.
    /// </summary>
    public class DataConnection : IConnectable
    {
        /// <summary>
        ///     If this data connection is currently active or not
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        ///     An indentifier for this particular connection.
        ///     This is set whenever the connection has been registered to a manager.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     A name for this particular data connection type.
        /// </summary>
        public string InternalName { get { return "Data Connection"; } }

        /// <summary>
        ///     The name given to this connection by the user to differentiate it
        ///     from the others.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The physical connection that can send and recieve data
        /// </summary>
        public MediaController MediaConnection;

        /// <summary>
        ///     The pipe to change the way the byte input and output is decoded and encoded
        /// </summary>
        public DataConverter Converter;

    }
}
