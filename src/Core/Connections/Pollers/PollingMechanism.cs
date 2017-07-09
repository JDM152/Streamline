namespace SeniorDesign.Core.Connections.Pollers
{
    /// <summary>
    ///     An object that keeps track of how to poll data from an input/push data to an output
    /// </summary>
    public abstract class PollingMechanism
    {
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
    }
}
