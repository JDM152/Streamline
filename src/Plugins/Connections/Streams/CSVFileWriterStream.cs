using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.IO;
using System.Text;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A wrapper for a Data Connection where data values are read off a CSV file
    /// </summary>
    [MetadataDataStream(AllowAsInput = false, AllowAsOutput = true, GenericPoller = false, GenericConverter = false)]
    public class CSVFileWriterStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The name of the file that is being read
        /// </summary>
        [UserConfigurableFile(
            Name = "Output File",
            Description = "The file to append to",
            Filter = "CSV File|*.csv|All Files|*.*",
            IsSave = true
        )]
        public string Filename
        {
            get { return _filename; }
            set
            {
                if (_filename == value) return;
                _filename = value;
                ChangeFile(_filename);
            }
        }
        private string _filename;

        #endregion

        /// <summary>
        ///     Creates a new CSVFileStream.
        ///     A file must be specified before it can be used
        /// </summary>
        public CSVFileWriterStream()
        {
            ErrorStrings.Add($"Select a file to write to");
            InvokeOnErrorStringsChanged();
        }

        /// <summary>
        ///     Changes the file that is being read, setting up the new reader
        /// </summary>
        /// <param name="newFile"></param>
        private void ChangeFile(string newFile)
        {
            ErrorStrings.Clear();

            // Don't accept empty strings
            if (string.IsNullOrEmpty(newFile))
                ErrorStrings.Add("Select a file to write to");

            InvokeOnErrorStringsChanged();
        }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "CSV File Writer Stream"; } }

        /// <summary>
        ///     Checks if this stream can be read from
        /// </summary>
        public override bool CanRead { get { return false; } }


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
            if (_isWriting) return;
            lock (_lock)
            {
                _isWriting = true;
                // Append to the file
                var str = new StringBuilder();
                while (data[0].Count > 0)
                    str.Append(data.Pop(0) + ", ");
                File.AppendAllText(Filename, str.ToString());
                _isWriting = false;
            }
        }

        private object _lock = new object();
        private bool _isWriting = false;

        /// <summary>
        ///     Checks to see if this is functional
        /// </summary>
        /// <returns>True if the state is valid, False if not</returns>
        public override bool Validate()
        {
            ErrorStrings.Clear();
            if (!string.IsNullOrEmpty(Filename))
                return true;

            ErrorStrings.Add($"Select a file to write to");
            InvokeOnErrorStringsChanged();
            return false;
        }
    }
}
