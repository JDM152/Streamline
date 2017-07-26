using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.IO.Ports;
using System.Linq;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A Data Connection that can communicate over the COM Ports (Inclides USB)
    /// </summary>
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false)]
    public class SerialCOMStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The name for the port to connect to
        /// </summary>
        [UserConfigurableString(
            Name = "Port Name",
            Description = "The name of the COM port to communicate with"
        )]
        public string PortName {
            get { return _portName; }
            set {
                if (_portName == value) return;
                _portName = value;
                UpdateConnection();
            }
        }
        private string _portName = "COM1";

        /// <summary>
        ///     The speed at which data is transmitted
        /// </summary>
        [UserConfigurableInteger(
            Name = "Baud Rate",
            Description = "The number of bits transmitted per second",
            Minimum = 1
        )]
        public int BaudRate
        {
            get { return _baudRate; }
            set
            {
                if (_baudRate == value) return;
                _baudRate = value;
                UpdateConnection();
            }
        }
        private int _baudRate = 115200;

        /// <summary>
        ///     The type of parity bit used in communication
        /// </summary>
        [UserConfigurableSelectableList(
            Name = "Parity Bit",
            Description = "The type of parity bit used in communication",
            Values = new object[]
            {
                "None", Parity.None,
                "Even", Parity.Even,
                "Odd", Parity.Odd,
                "Mark", Parity.Mark,
                "Space", Parity.Space
            }
        )]
        public Parity ParityBit
        {
            get { return _parityBit; }
            set
            {
                if (_parityBit == value) return;
                _parityBit = value;
                UpdateConnection();
            }
        }
        private Parity _parityBit = Parity.None;

        /// <summary>
        ///     The number of data bits per packet
        /// </summary>
        [UserConfigurableInteger(
            Name = "Data Bits",
            Description = "The number of data bits per frame",
            Minimum = 1
        )]
        public int DataBits
        {
            get { return _dataBits; }
            set
            {
                if (_dataBits == value) return;
                _dataBits = value;
                UpdateConnection();
            }
        }
        private int _dataBits = 8;

        /// <summary>
        ///     The number of bits used to signify the end of a packet
        /// </summary>
        [UserConfigurableSelectableList(
            Name = "Stop Bit",
            Description = "The type of stop bit used to end a frame",
            Values = new object[]
            {
                "None", StopBits.None,
                "1", StopBits.One,
                "1.5", StopBits.OnePointFive,
                "2", StopBits.Two
            }
        )]
        public StopBits StopBit
        {
            get { return _stopBit; }
            set
            {
                if (_stopBit == value) return;
                _stopBit = value;
                UpdateConnection();
            }
        }
        private StopBits _stopBit = StopBits.One;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Serial COM Stream"; } }

        /// <summary>
        ///     The serial port that communicates with the device
        /// </summary>
        protected SerialPort Port = null;

        /// <summary>
        ///     Sets up a new COM Stream
        /// </summary>
        public SerialCOMStream()
        {
            UpdateConnection();
        }

        /// <summary>
        ///     Updates the connection based on any settings that have changed
        /// </summary>
        private void UpdateConnection()
        {
            if (string.IsNullOrEmpty(PortName))
                return;

            // Clean up if the port still exists
            if (Port != null)
            {
                if (Port.IsOpen)
                    Port.Close();
                Port.Dispose();
            }

            // Set the object to needing recompiled
            NeedsCompile = true;
        }

        /// <summary>
        ///     Checks if this stream can be read from.
        /// </summary>
        public override bool CanRead { get { return true; } }

        /// <summary>
        ///     Reads from the stream into a byte buffer
        /// </summary>
        /// <param name="buffer">The buffer to read into</param>
        /// <param name="offset">The offset into the buffer to start writing</param>
        /// <param name="count">The number of bytes to read into the buffer</param>
        /// <returns>The number of bytes read</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return Port.Read(buffer, offset, count);
        }

        /// <summary>
        ///     Checks if this stream can be written to.
        /// </summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>
        ///     Writes bytes out to the stream.
        /// </summary>
        /// <param name="buffer">The bytes to write to the stream</param>
        /// <param name="offset">The offset into the buffer to start</param>
        /// <param name="count">The number of bytes to write</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            Port.Write(buffer, offset, count);
        }

        /// <summary>
        ///     Checks to see if this is functional
        /// </summary>
        /// <returns>True if the state is valid, False if not</returns>
        public override bool Validate()
        {
            return Port != null;
        }

        /// <summary>
        ///     Checks if this object needs to be compiled before it is actually used
        /// </summary>
        public override bool CanCompile { get { return true; } }

        /// <summary>
        ///     Compiles this object for actual use
        /// </summary>
        public override void Compile()
        {
            NeedsCompile = false;
            ErrorStrings.Clear();

            // Create a new port with the user settings
            try
            {
                Port = new SerialPort(PortName, BaudRate, ParityBit, DataBits, StopBit);
                if (!Port.IsOpen)
                    Port.Open();
            }
            catch (Exception ex)
            {
                ErrorStrings.Add(ex.Message);
                Port = null;
            }

            // Update if this still needs compiled or not
            NeedsCompile = (Port == null || !Port.IsOpen);
        }
    }
}
