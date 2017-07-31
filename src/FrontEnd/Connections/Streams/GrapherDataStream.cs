using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Attributes.Specialized;
using SeniorDesign.Core.Connections.Streams;
using SeniorDesign.Core.Util;
using SeniorDesign.FrontEnd.Windows;
using System;
using System.Collections.Generic;

namespace SeniorDesign.FrontEnd.Connections.Streams
{
    /// <summary>
    ///     A connection to a Grapher Window that acts similar to an oscilloscope
    /// </summary>
    [MetadataDataStream(AllowAsInput = false, AllowAsOutput = true, GenericConverter = false)]
    public class GrapherDataStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The value that this stream outputs
        /// </summary>
        [UserConfigurableInteger(
            Name = "History",
            Description = "The number of values to graph",
            Minimum = 1
        )]
        public int History = 100;

        #endregion

        #region Function Buttons

        /// <summary>
        ///     Function the user can trigger to display the graph
        /// </summary>
        [FunctionButton(
            Name = "Open Grapher",
            Description = "Opens the Graph viewing window"
        )]
        public void ShowGraph()
        {
            if (_grapherWindow == null || _grapherWindow.IsDisposed)
            {
                _grapherWindow = new GrapherPanel();
                _grapherWindow.Show();
            }
            else
            {
                _grapherWindow.BringToFront();
            }
        }

        #endregion

        /// <summary>
        ///     The actual window with the grapher in it
        /// </summary>
        private GrapherPanel _grapherWindow = null;

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Grapher"; } }

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
            throw new NotImplementedException();
        }

        /// <summary>
        ///     If this type of data stream supports WriteDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public override bool CanWriteDirect { get { return true; } }

        /// <summary>
        ///     Writes directly from the stream, ignoring the Converter
        /// </summary>
        /// <param name="data">The data to write to the stream</param>
        public override void WriteDirect(DataPacket data)
        {
            // Pass on to the grapher (if visible and available)
            if (_grapherWindow != null && !_grapherWindow.IsDisposed)
                _grapherWindow.Grapher.Draw(data);
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            // Start constructing the data array
            var toReturn = new List<byte>(base.ToBytes());

            // Add all of the user configurable options
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(History));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            // Restore the base first
            base.Restore(data, ref offset);

            // Restore all of the user configurable options
            History = ByteUtil.GetIntFromSizedArray(data, ref offset);
        }

    }
}
