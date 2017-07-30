using System;
using System.Collections.Generic;

namespace SeniorDesign.Core.Connections.Converter
{
    /// <summary>
    ///     A configurable converter used in Data Connections to decode strings
    ///     into usable data. This class is allowed to have state for data input
    ///     that is multi-packet.
    /// </summary>
    public abstract class DataConverter : IDataConnectionComponent
    {
        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     Decodes an input byte stream into seperate values.
        ///     If input is not emptied, it will be appended to the next round.
        /// </summary>
        /// <param name="input">The input byte array to convert</param>
        /// <returns>A series of doubles representing the decoded data</returns>
        public virtual DataPacket DecodeData(ref byte[] input)
        {
            return null;
        }

        /// <summary>
        ///     Encodes input values into a single byte stream.
        ///     If output is not emptied, it will be appended to the next round.
        /// </summary>
        /// <param name="output">The output byte array to convert</param>
        /// <returns>A series of bytes representing the encoded data</returns>
        public virtual byte[] EncodeData(DataPacket output)
        {
            return null;
        }

        /// <summary>
        ///     The number of output streams this converts from a single byte stream
        /// </summary>
        public virtual int DecodeDataCount { get; } = -1;

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
        public bool NeedsCompile {
            get { return _needsCompile; }
            protected set {
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
        ///     Compiles this object for actual use
        /// </summary>
        public virtual void Compile() { throw new NotImplementedException(); }

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
