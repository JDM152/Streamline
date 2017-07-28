using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.IO;
using System.Text;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A Data Connection that reads in various audio file formats
    /// </summary>
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false, GenericPoller = true, GenericConverter = false)]
    public class CSVFileReaderStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The name of the file that is being read
        /// </summary>
        [UserConfigurableFile(
            Name = "CSV File",
            Description = "The csv file to load",
            Filter = "CSV Files|*.csv|All Files|*.*"
        )]
        public string Filename {
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
        ///     Creates a new AudioFileStream.
        ///     A file must be specified before it can be used
        /// </summary>
        public CSVFileReaderStream()
        {
            ErrorStrings.Add($"Select a valid csv file");
            InvokeOnErrorStringsChanged();
        }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "CSV File Reader Stream"; } }

        /// <summary>
        ///     The reader that reads off the file data
        /// </summary>
        protected StreamReader Reader = null;

        /// <summary>
        ///     Checks if this stream can be read from.
        /// </summary>
        public override bool CanRead { get { return Reader != null; } }

        /// <summary>
        ///     Checks if the position of the stream can be changed.
        /// </summary>
        public override bool CanSeek { get { return false; } }

        /// <summary>
        ///     Checks if this stream can be written to.
        /// </summary>
        public override bool CanWrite { get { return false; } }

        /// <summary>
        ///     Changes the file that is being read, setting up the new reader
        /// </summary>
        /// <param name="newFile"></param>
        private void ChangeFile(string newFile)
        {
            ErrorStrings.Clear();

            // Dispose of the old reader as needed
            try
            {
                if (Reader != null)
                    Reader.Dispose();
            } catch { }
            Reader = null;

            // Create the new reader
            try
            {
                Reader = File.OpenText(newFile);
            }
            catch (Exception ex)
            {
                ErrorStrings.Add(ex.Message);
            }

            InvokeOnErrorStringsChanged();
        }

        /// <summary>
        ///     If this type of data stream supports ReadDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public override bool CanReadDirect { get { return true; } }

        /// <summary>
        ///     Reads directly from the stream, ignoring the Converter, and providing 
        /// </summary>
        /// <param name="count">The number of points to poll</param>
        /// <returns>The data packet with at most count data points added</returns>
        public override DataPacket ReadDirect(int count)
        {
            var toReturn = new DataPacket();
            if (Reader == null) return toReturn;
            toReturn.AddChannel();

            // Read the required number of points off of the stream
            var str = new StringBuilder();
            double realVal;
            string realStr;
            while (!Reader.EndOfStream && count > 0)
            {
                var symbol = (char) Reader.Read();
                if (symbol == ',')
                {
                    realStr = str.ToString();
                    if (double.TryParse(realStr.Trim(), out realVal))
                    {
                        toReturn[0].Add(realVal);
                        count--;
                    }
                    str.Clear();
                }
                else
                {
                    str.Append(symbol);
                }
            }

            // Push the last little bit if ran out of room
            if (str.Length > 0)
            {
                realStr = str.ToString();
                if (double.TryParse(realStr.Trim(), out realVal))
                {
                    toReturn[0].Add(realVal);
                    count--;
                }
            }

            return toReturn;
        }

        /// <summary>
        ///     Checks to see if this is functional
        /// </summary>
        /// <returns>True if the state is valid, False if not</returns>
        public override bool Validate()
        {
            ErrorStrings.Clear();
            if (Reader != null)
                return true;

            ErrorStrings.Add($"Select a valid CSV file");
            InvokeOnErrorStringsChanged();
            return false;
        }
    }
}
