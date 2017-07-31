using NAudio.Wave;
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
    public class MicrophoneStream : DataStream
    {
        
        /// <summary>
        ///     Creates a new AudioFileStream.
        ///     A file must be specified before it can be used
        /// </summary>
        public MicrophoneStream()
        {
            Reader = new WaveIn();
            Reader.DataAvailable += DataAvailableEvent;
            Reader.WaveFormat = new WaveFormat();

            BufferedPacketA = new DataPacket();
            BufferedPacketA.AddChannel();

            BufferedPacketB = new DataPacket();
            BufferedPacketB.AddChannel();

            CurrentDataPacket = BufferedPacketA;
        }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Microphone Input Stream"; } }

        /// <summary>
        ///     The wave reader that gets microphone input
        /// </summary>
        protected WaveIn Reader;

        /// <summary>
        ///     The leftover audio samples that need to be passed over
        /// </summary>
        protected DataPacket BufferedPacketA;

        /// <summary>
        ///     The second leftover audio samples that need to be passed over
        /// </summary>
        protected DataPacket BufferedPacketB;

        /// <summary>
        ///     Either BufferedPacket A or B
        /// </summary>
        protected DataPacket CurrentDataPacket;

        /// <summary>
        ///     Checks if this stream can be read from.
        /// </summary>
        public override bool CanRead { get { return Reader != null; } }

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
            lock (_bufferLock)
            {
                if (BufferedPacketA.ChannelCount <= 0)
                    BufferedPacketA.AddChannel();
                if (BufferedPacketB.ChannelCount <= 0)
                    BufferedPacketB.AddChannel();

                // Swap buffers whenever a read is requested
                if (CurrentDataPacket == BufferedPacketA)
                {
                    CurrentDataPacket = BufferedPacketB;
                    return BufferedPacketA;
                }
                else
                {
                    CurrentDataPacket = BufferedPacketA;
                    return BufferedPacketB;
                }
            }
        }

        /// <summary>
        ///     Method triggered whenever the stream is enabled
        /// </summary>
        public override void Enable()
        {
            Reader.StartRecording();
        }

        /// <summary>
        ///     Method triggered when the stream is disabled
        /// </summary>
        public override void Disable()
        {
            Reader.StopRecording();
        }

        /// <summary>
        ///     Method triggered whenever data is available from the microphone
        /// </summary>
        private void DataAvailableEvent(object sender, WaveInEventArgs e)
        {
            lock (_bufferLock)
            {
                if (CurrentDataPacket.ChannelCount <= 0)
                    CurrentDataPacket.AddChannel();
                // Convert from 16-bit samples
                for (var k = 0; k < e.BytesRecorded; k += 2)
                    CurrentDataPacket[0].Add(BitConverter.ToInt16(e.Buffer, k) / ((double) ushort.MaxValue));
            }
        }

        /// <summary>
        ///     An object to prevent collisions when polling
        /// </summary>
        private object _bufferLock = new object();
    }
}
