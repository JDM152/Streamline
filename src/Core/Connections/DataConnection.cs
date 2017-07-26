using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using SeniorDesign.Core.Connections.Streams;
using SeniorDesign.Core.Util;
using SeniorDesign.Plugins.Util;
using System.Collections.Generic;

namespace SeniorDesign.Core.Connections
{
    /// <summary>
    ///     A single connection for input or output of data.
    ///     This is technically a container for the media type, 
    ///     the decoding, and the push/pull controllers.
    /// </summary>
    public class DataConnection : IConnectable, IRestorable
    {
        /// <summary>
        ///     If this data connection is currently active or not
        /// </summary>
        [UserConfigurableBoolean(
            Name = "Enabled",
            Description = "If the data connection is currently polling for data"
        )]
        public bool Enabled
        {
            get { return _enabled; }
            set {
                _enabled = value;
                EnablePolling(value);
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
        ///     The X position of this module in the block editor
        /// </summary>
        public int PositionX { get; set; }

        /// <summary>
        ///     The Y position of this module in the block editor
        /// </summary>
        public int PositionY { get; set; }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     This will either be 0 for an input, or -1 for an output
        /// </summary>
        public int InputCount { get { return IsOutput ? -1 : 0; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     This is decided by the converter
        /// </summary>
        public int OutputCount { get {
                if (!IsOutput) return 0;

                if (Converter == null)
                    return MediaConnection.DirectOutputCount;
                else
                    return Converter.DecodeDataCount;
            } }

        /// <summary>
        ///     The physical connection that can send and recieve data
        /// </summary>
        public DataStream MediaConnection
        {
            get { return _mediaConnection; }
            set
            {
                // Disable the poller before changing, to be safe
                if (_poller != null) _poller.Disable();

                _mediaConnection = value;

                if (_mediaConnection != null)
                {
                    // Remove the converter if no longer legal
                    if (_converter != null && !_mediaConnection.VerifyConverter(_converter))
                        _converter = null;

                    // Remove the poller if no longer legal
                    if (_poller != null && !_mediaConnection.VerifyPoller(_poller))
                        _poller = null;
                }

            }
        }
        private DataStream _mediaConnection;

        /// <summary>
        ///     The pipe to change the way the byte input and output is decoded and encoded
        /// </summary>
        public DataConverter Converter
        {
            get { return _converter; }
            set
            {
                // Raise an error if the converter is forbidden
                if (MediaConnection != null && !MediaConnection.VerifyConverter(value))
                    throw new System.Exception($"A generic converter was passed to a DataStream {MediaConnection.InternalName} while the DataStream provides its own converter."); ;

                _converter = value;
            }
        }
        private DataConverter _converter;

        /// <summary>
        ///     The mechanism that polls for the data
        /// </summary>
        public PollingMechanism Poller {
            get { return _poller; }
            set
            {
                // Raise an error if the poller is forbidden
                if (MediaConnection != null && !MediaConnection.VerifyPoller(value))
                    throw new System.Exception($"A generic poller was passed to a DataStream {MediaConnection.InternalName} while the DataStream provides its own poller."); ;

                _poller = value;
                if (_poller != null) {
                    _poller.Connection = this;
                }
            }
        }
        private PollingMechanism _poller;

        /// <summary>
        ///     The next connections in the connectable graph
        /// </summary>
        public IList<IConnectable> NextConnections { get; protected set; } = new List<IConnectable>();

        /// <summary>
        ///     True if this connection is an output, false if it is an input
        /// </summary>
        public bool IsOutput = false;

        /// <summary>
        ///     The data that the decoder was unable to use
        /// </summary>
        private byte[] _leftoverInputData;

        /// <summary>
        ///     Enables or disables Polling
        /// </summary>
        /// <param name="status">True to enable, false to disable</param>
        public void EnablePolling(bool status = true)
        {
            if (_poller == null) return;
            if (status) _poller.Enable();
            else _poller.Disable();
        }

        /// <summary>
        ///     Polls the data connection for any new data.
        ///     This is specifically for the Polling Mechanism
        /// </summary>
        public void Poll(StreamlineCore core)
        {
            // Check if we can read directly
            if (Converter == null && MediaConnection.CanReadDirect)
            {
                // Read a data packet directly from the connection
                core.PassDataToNextConnectable(this, MediaConnection.ReadDirect(CoreSettings.InputBuffer));
            }
            else
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

            // Don't accept anything if not enabled
            if (!Enabled)
            {
                data.Clear();
                return;
            }

            // Check if the data can be written directly
            if (Converter == null && MediaConnection.CanWriteDirect)
            {
                // Send the data directly
                MediaConnection.WriteDirect(data);
            }
            else
            {
                // Encode the data
                var encodedData = Converter.EncodeData(data);

                // Pass it on to be output
                MediaConnection.Write(encodedData, 0, encodedData.Length);
            }
        }

        /// <summary>
        ///     Checks to see if this Data Connection is functional
        /// </summary>
        /// <returns>True if the state is valid, False if not</returns>
        public bool Validate()
        {
            if (MediaConnection == null)
                return false;

            if (Poller == null && MediaConnection.UsesGenericPollers)
                return false;

            if (Converter == null && MediaConnection.UsesGenericConverters)
                return false;

            if (!MediaConnection.Validate())
                return false;

            if (Poller != null && !Poller.Validate())
                return false;

            if (Converter != null && !Converter.Validate())
                return false;

            return true;
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public virtual byte[] ToBytes()
        {
            var toReturn = new List<byte>();

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Id));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Name));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(IsOutput));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(PositionX));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(PositionY));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public virtual void Restore(byte[] data, ref int offset)
        {
            Id = ByteUtil.GetIntFromSizedArray(data, ref offset);
            Name = ByteUtil.GetStringFromSizedArray(data, ref offset);
            IsOutput = ByteUtil.GetBoolFromSizedArray(data, ref offset);
            PositionX = ByteUtil.GetIntFromSizedArray(data, ref offset);
            PositionY = ByteUtil.GetIntFromSizedArray(data, ref offset);
        }

        /// <summary>
        ///     Gets the name of this particular object
        /// </summary>
        /// <returns>The object's name as given by the user</returns>
        public override string ToString()
        {
            return Name;
        }

    }
}
