using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Converter;
using SeniorDesign.Core.Connections.Pollers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SeniorDesign.Core.Connections.Streams
{
    /// <summary>
    ///     A stream specialized for use with Streamline
    /// </summary>
    public abstract class DataStream : Stream, IDataConnectionComponent
    {

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     Verifies that a poller is compatible with this DataStream
        /// </summary>
        /// <param name="poller">The poller to verify</param>
        /// <returns>True if the poller is allowed</returns>
        public virtual bool VerifyPoller(PollingMechanism poller)
        {
            // Don't allow if the poller is built-in
            if (!UsesGenericPollers)
                return false;

            return true;
        }

        /// <summary>
        ///     Verifies that a converter is compatible with this DataStream
        /// </summary>
        /// <param name="converter">The converter to verify</param>
        /// <returns>True if the converter is allowed</returns>
        public virtual bool VerifyConverter(DataConverter converter)
        {
            // Don't allow if the converter is built-in
            if (!UsesGenericConverters && converter != null)
                return false;

            return true;
        }
        /// <summary>
        ///     If this Data Stream can use a generic poller
        /// </summary>
        public bool UsesGenericPollers { get { return GetType().GetCustomAttribute<MetadataDataStreamAttribute>().GenericPoller; } }

        /// <summary>
        ///     If this Data Stream can use a generic converter
        /// </summary>
        public bool UsesGenericConverters { get { return GetType().GetCustomAttribute<MetadataDataStreamAttribute>().GenericConverter; } }

        /// <summary>
        ///     If this type of data stream supports ReadDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public virtual bool CanReadDirect { get { return false; } }

        /// <summary>
        ///     How many output channels are available when reading directly
        /// </summary>
        public virtual int DirectOutputCount { get { return CanReadDirect ? 1 : 0; } }

        /// <summary>
        ///     If the end of the stream has been reached
        /// </summary>
        public bool EndOfStream {
            get
            {
                return _endOfStream;
            }
            protected set
            {
                if (_endOfStream == value) return;
                _endOfStream = value;
                if (_endOfStream) Console.WriteLine("End of file reached on " + InternalName);
            }
        }
        private bool _endOfStream;

        #region Stream Default Implementation

        /// <summary>
        ///     Checks if this stream can be read from.
        /// </summary>
        public override bool CanRead { get { return false; } }

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
        public override long Length { get { throw new NotSupportedException(); } }

        /// <summary>
        ///     Gets the current position in the stream.
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

        #endregion

        /// <summary>
        ///     Reads directly from the stream, ignoring the Converter
        /// </summary>
        /// <param name="count">The number of points to poll</param>
        /// <returns>The data packet with at most count data points added</returns>
        public virtual DataPacket ReadDirect(int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     If this type of data stream supports WriteDirect.
        ///     Note that this will only apply if the Converter has not been specified
        /// </summary>
        public virtual bool CanWriteDirect { get { return false; } }

        /// <summary>
        ///     Writes directly from the stream, ignoring the Converter
        /// </summary>
        /// <param name="data">The data to write to the stream</param>
        public virtual void WriteDirect(DataPacket data)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Gets the metadata for this particular data stream
        /// </summary>
        /// <returns>The metadata attached to this data stream type</returns>
        public static MetadataDataStreamAttribute GetMetadata()
        {
            return MethodBase.GetCurrentMethod().DeclaringType.GetCustomAttribute<MetadataDataStreamAttribute>();
        }

        #region IDataConnectionComponent

        /// <summary>
        ///     Any errors that this component has
        /// </summary>
        public IList<string> ErrorStrings { get; } = new List<string>();

        /// <summary>
        ///     Event that is triggered when the error strings have changed
        /// </summary>
        public event EventHandler OnErrorStringsChanged;

        /// <summary>
        ///     Ensures that this object is valid before allowing it to be used
        /// </summary>
        /// <returns>True if the object is valid</returns>
        public virtual bool Validate()
        {
            return true;
        }

        /// <summary>
        ///     Checks if this object needs to be compiled before it is actually used
        /// </summary>
        /// <returns>True if Compile needs to be called before this object is valid</returns>
        public virtual bool CanCompile { get { return false; } }

        /// <summary>
        ///     Checks if this object needs to be compiled (If any changes were made)
        /// </summary>
        public bool NeedsCompile
        {
            get { return _needsCompile; }
            protected set
            {
                if (_needsCompile == value) return;
                _needsCompile = value;
                AddNeedsCompileErorr();
                OnNeedsCompileChangeEvent?.Invoke(this, value);
            }
        }
        private bool _needsCompile;

        /// <summary>
        ///     Event that is triggered when the NeedsCompile value changes
        /// </summary>
        public event EventHandler<bool> OnNeedsCompileChangeEvent;

        /// <summary>
        ///     Adds and removes the Needs Compiled Error as needed
        /// </summary>
        private void AddNeedsCompileErorr()
        {
            var eIndex = ErrorStrings.IndexOf(Constants.ERROR_COMPILE);
            if (_needsCompile && eIndex < 0)
            {
                ErrorStrings.Add(Constants.ERROR_COMPILE);
                OnErrorStringsChanged?.Invoke(this, null);
            }
            else if (!_needsCompile && eIndex >= 0)
            {
                ErrorStrings.RemoveAt(eIndex);
                OnErrorStringsChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        ///     Utility method for invoking the ErrorStringChanged event
        /// </summary>
        protected void InvokeOnErrorStringsChanged()
        {
            OnErrorStringsChanged?.Invoke(this, null);
        }

        /// <summary>
        ///     Compiles this object for actual use
        /// </summary>
        public virtual void Compile() { throw new NotImplementedException(); }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public virtual byte[] ToBytes()
        {
            return new byte[0];
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public virtual void Restore(byte[] data, ref int offset)
        {

        }

        #endregion
    }
}
