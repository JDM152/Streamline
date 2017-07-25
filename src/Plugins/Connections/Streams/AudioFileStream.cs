﻿using NAudio.Wave;
using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
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
        ///     Gets the current position in the stream.
        /// </summary>
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Changes the file that is being read, setting up the new reader
        /// </summary>
        /// <param name="newFile"></param>
        private void ChangeFile(string newFile)
        {
            // Dispose of the old reader as needed
            if (Reader != null)
                Reader.Dispose();

            // Create the new reader
            var extension = Path.GetExtension(newFile).ToLower();
            switch (extension)
            {
                case ".mp3":
                    Reader = new Mp3FileReader(newFile);
                    break;

                case ".wav":
                    Reader = new WaveFileReader(newFile);
                    Sampler = Reader.ToSampleProvider();
                    break;

                default: // Unknown format
                    throw new Exception($"The audio format {extension} is not supported.");
            }
        }

        /// <summary>
        ///     Flushes all of the input from the buffer to the output.
        /// </summary>
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Reads from the stream into a byte buffer
        /// </summary>
        /// <param name="buffer">The buffer to read into</param>
        /// <param name="offset">The offset into the buffer to start writing</param>
        /// <param name="count">The number of bytes to read into the buffer</param>
        /// <returns>The number of bytes read</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Moves to a position in the stream.
        ///     Audio does not allow seeking, so this throws.
        /// </summary>
        /// <param name="offset">The position in the stream to move to</param>
        /// <param name="origin">The position to use as the origin for the stream</param>
        /// <returns>The position that was moved to in the stream</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Sets the length of the current stream.
        ///     Audio does not allow seeking, so this throws.
        /// </summary>
        /// <param name="value">The length to set the stream.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Writes bytes out to the stream.
        /// </summary>
        /// <param name="buffer">The bytes to write to the stream</param>
        /// <param name="offset">The offset into the buffer to start</param>
        /// <param name="count">The number of bytes to write</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
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
            var buffer = new float[count];
            count = Sampler.Read(buffer, 0, count);
            if (count <= 0)
                return toReturn;
            for (var k = 0; k < count; k++)
                toReturn[0].Add((double) buffer[k]);

            return toReturn;
        }
    }
}
