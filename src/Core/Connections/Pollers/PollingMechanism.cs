namespace SeniorDesign.Core.Connections.Pollers
{
    /// <summary>
    ///     An object that keeps track of how to poll data from an input/push data to an output
    /// </summary>
    public abstract class PollingMechanism : IRestorable
    {
        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public abstract string InternalName { get; }

        /// <summary>
        ///     Creates a new Polling Mechanism working for a specific core
        /// </summary>
        /// <param name="core">The core this reports back to</param>
        public PollingMechanism(StreamlineCore core)
        {
            Core = core;
        }

        /// <summary>
        ///     The core that this reports to
        /// </summary>
        protected StreamlineCore Core;

        /// <summary>
        ///     The data connection that is required to be polled.
        /// </summary>
        public DataConnection Connection;

        /// <summary>
        ///     Enables the polling mechanism to take additional input.
        ///     This will always be called after the Connection has been linked.
        /// </summary>
        public abstract void Enable();

        /// <summary>
        ///     Disables the polling mechanism 
        /// </summary>
        public abstract void Disable();

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
    }
}
