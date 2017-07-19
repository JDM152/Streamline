using NAudio.Wave;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Streams;
using System;
using System.IO;

namespace SeniorDesign.Plugins.Connections
{
    /// <summary>
    ///     A Data Connection that allows audio output.
    ///     Input is in a seperate stream.
    /// </summary>
    [MetadataDataStream(AllowAsInput = false, AllowAsOutput = true)]
    public class AudioDataStream : DataStream
    {
        #region User Config

        /// <summary>
        ///     The number of samples per second during playback
        /// </summary>
        [UserConfigurableInteger(
            Name = "Sampling Rate",
            Description = "The number of samples taken or played per second",
            Minimum = 1    
        )]
        public int SamplingRate {
            get { return _samplingRate; }
            set {
                // Set the value and reset the audio
                _samplingRate = value;
                ResetWaveFormat();
                }
            }
        private int _samplingRate = 4410;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Audio Stream"; } }

        /// <summary>
        ///     The sound player that will be piped to
        /// </summary>
        protected WaveOut Player;

        /// <summary>
        ///     The format information for how the sound is played
        /// </summary>
        protected WaveFormat Format;

        /// <summary>
        ///     The stream reader that feeds into the audio device
        /// </summary>
        protected RawSourceWaveStream SourceStream;

        /// <summary>
        ///     The stream that will store the written data
        /// </summary>
        protected MemoryStream Memory;

        /// <summary>
        ///     Initializes the defaults for the audio player
        /// </summary>
        public AudioDataStream()
        {
            // Create the memory stream to store the audio data in
            Memory = new MemoryStream(SamplingRate * 10);

            // Create the audio player using Floating point format
            ResetWaveFormat();
        }

        /// <summary>
        ///     Resets the wave format data
        ///     Note that this should only be used if the SamplingRate changed
        /// </summary>
        protected void ResetWaveFormat()
        {
            // Create a new player as needed
            if (Player == null)
                Player = new WaveOut();
            else
                Player.Stop();

            // Change the format and the stream, and start playing
            Format = WaveFormat.CreateIeeeFloatWaveFormat(SamplingRate, 1);
            SourceStream = new RawSourceWaveStream(Memory, Format);
            Player.Init(SourceStream);
            Player.Play();
        }

        /// <summary>
        ///     Checks if this stream can be read from.
        ///     This stream does not currently support reading
        /// </summary>
        public override bool CanRead { get { return false; } }

        /// <summary>
        ///     Checks if the position of the stream can be changed.
        ///     Audio output cannot be read
        /// </summary>
        public override bool CanSeek { get { return false; } }

        /// <summary>
        ///     Checks if this stream can be written to.
        ///     Anything written to the stream will be output as a sample.
        /// </summary>
        public override bool CanWrite { get { return true; } }

        /// <summary>
        ///     Gets the length of the available stream.
        ///     Audio Output will not allow seeking, so this throws.
        /// </summary>
        public override long Length { get { throw new NotSupportedException(); } }

        /// <summary>
        ///     Gets the current position in the stream.
        ///     Audio Output will not allow seeking, so this throws.
        /// </summary>
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        ///     Flushes all of the input from the buffer to the output.
        /// </summary>
        public override void Flush()
        {
            // Do nothing
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
            // Write to the memory buffer
            Memory.Write(buffer, offset, count);
        }
    }
}
