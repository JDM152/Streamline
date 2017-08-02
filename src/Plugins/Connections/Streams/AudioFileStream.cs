using NAudio.Wave;
using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using SeniorDesign.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A Data Connection that reads in various audio file formats
    /// </summary>
    [MetadataDataStream(AllowAsInput = true, AllowAsOutput = false, GenericPoller = true, GenericConverter = false)]
    public class AudioFileStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The name of the file that is being read
        /// </summary>
        [UserConfigurableFile(
            Name = "Audio File",
            Description = "The audio file to load",
            Filter = "Supported Files|*.wav|All Files|*.*"
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
        public AudioFileStream()
        {
            ErrorStrings.Add($"Select a valid audio file");
            InvokeOnErrorStringsChanged();
        }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Audio File Stream"; } }

        /// <summary>
        ///     The reader that reads off the file data
        /// </summary>
        protected WaveStream Reader = null;

        /// <summary>
        ///     The sampler that grabs individual sample points
        /// </summary>
        protected ISampleProvider Sampler = null;

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
        ///     Gets the length of the available stream.
        /// </summary>
        public override long Length { get { return Reader.Length; } }

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
            Sampler = null;

            // Create the new reader
            var extension = Path.GetExtension(newFile).ToLower();
            switch (extension)
            {
                case ".wav":
                    try
                    {
                        Reader = new WaveFileReader(newFile);
                        Sampler = Reader.ToSampleProvider();
                    }
                    catch (Exception ex)
                    {
                        ErrorStrings.Add(ex.Message);
                    }
                    break;

                default: // Unknown format
                    ErrorStrings.Add($"The audio format {extension} is not supported.");
                    break;
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
            if (Sampler == null) return toReturn;
            toReturn.AddChannel();

            // Read the required number of points off of the stream
            count *= 4;
            var buffer = new float[count];
            count = Sampler.Read(buffer, 0, count);
            if (count <= 0)
                return toReturn;
            for (var k = 0; k < count; k++)
                toReturn[0].Add((double) buffer[k]);

            return toReturn;
        }

        /// <summary>
        ///     Checks to see if this is functional
        /// </summary>
        /// <returns>True if the state is valid, False if not</returns>
        public override bool Validate()
        {
            ErrorStrings.Clear();
            if (Sampler != null)
                return true;

            ErrorStrings.Add($"Select a valid audio file");
            InvokeOnErrorStringsChanged();
            return false;
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            var toReturn = new List<byte>(base.ToBytes());

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Filename));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            base.Restore(data, ref offset);

            Filename = ByteUtil.GetStringFromSizedArray(data, ref offset);
        }
    }
}
