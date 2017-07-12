using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using SeniorDesign.Core.Util;
using SeniorDesign.Plugins.Util;
using System.Collections.Generic;
using System.IO;

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
        public bool Enabled
        {
            get { return _enabled; }
            set {
                _enabled = value;
                if (_enabled)
                    _poller.Enable();
                else
                    _poller.Disable();
            }
        }
        private bool _enabled;

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
        ///     The number of input connections this connectable accepts.
        ///     This will either be 0 for an input, or -1 for an output
        /// </summary>
        public int InputCount { get { return IsOutput ? -1 : 0; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     This is decided by the converter
        /// </summary>
        public int OutputCount { get { return Converter.DecodeDataCount; } }

        /// <summary>
        ///     The physical connection that can send and recieve data
        /// </summary>
        public Stream MediaConnection;

        /// <summary>
        ///     The pipe to change the way the byte input and output is decoded and encoded
        /// </summary>
        public DataConverter Converter;

        /// <summary>
        ///     The next connections in the connectable graph
        /// </summary>
        public IList<IConnectable> NextConnections { get; protected set; } = new List<IConnectable>();

        /// <summary>
        ///     The mechanism that polls for the data
        /// </summary>
        public PollingMechanism Poller {
            get { return _poller; }
            set
            {
                _poller = value;
                if (_poller != null) {
                    _poller.Connection = this;
                    _poller.Enable();
                }
            }
        }
        private PollingMechanism _poller;

        /// <summary>
        ///     True if this connection is an output, false if it is an input
        /// </summary>
        public bool IsOutput = false;

        /// <summary>
        ///     The data that the decoder was unable to use
        /// </summary>
        private byte[] _leftoverInputData;

        /// <summary>
        ///     Polls the data connection for any new data.
        ///     This is specifically for the Polling Mechanism
        /// </summary>
        public void Poll(StreamlineCore core)
        {
            // Grab all available bytes, and pass it to the decoder
            var data = MediaConnection.ReadToEnd(CoreSettings.InputBuffer);
            if (_leftoverInputData != null)
                _leftoverInputData = _leftoverInputData.Concat(data);
            else
                _leftoverInputData = data;
            var decodedData = Converter.DecodeData(ref _leftoverInputData);

            // Pass the data on to the next step
            core.PassDataToNextConnectable(this, new DataPacket(decodedData));
        }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     If this is an input, it will throw.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        public void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Throw if illegal
            if (!IsOutput)
                throw new System.Exception("An input cannot accept data from other program nodes.");

            // Encode the data
            if (_leftoverOutputData != null)
                for (var k = 0; k < _leftoverOutputData.Length; k++)
                    _leftoverOutputData[k] = _leftoverOutputData[k].Concat(data[k]);
            else
                _leftoverOutputData = data;
            var encodedData = Converter.EncodeData(ref _leftoverOutputData);

            // Pass it on to be output
            MediaConnection.Write(encodedData, 0, encodedData.Length);
        }

    }
}
