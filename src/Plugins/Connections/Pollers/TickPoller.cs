using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Pollers;

namespace SeniorDesign.Plugins.Connections.Pollers
{
    /// <summary>
    ///     A polling mechanism where data is polled periodically using the Streamline tick
    /// </summary>
    [MetadataPoller(TickBased = true)]
    public class TickPoller : PollingMechanism
    {

        #region User Configuration

        /// <summary>
        ///     The time between polling in ticks
        /// </summary>
        [UserConfigurableInteger(
            Name = "Polling Time",
            Description = "The time between polling, in ticks",
            Minimum = 1
        )]
        public int PollingTime = 1;

        /// <summary>
        ///     How many points are pulled per polling instance (max)
        /// </summary>
        [UserConfigurableInteger(
            Name = "Point Count",
            Description = "The number of points that are polled maximum",
            Minimum = 1
        )]
        public int PollingCount = 1;

        #endregion

        /// <summary>
        ///     The number of ticks since the last poll
        /// </summary>
        private int _ticks = 0;

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Tick Poller"; } }

        /// <summary>
        ///     Creates a new Tick Poller
        /// </summary>
        /// <param name="core">The core that this reports back to</param>
        public TickPoller(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Enables the polling mechanism to take additional input.
        ///     This will always be called after the Connection has been linked.
        /// </summary>
        public override void Enable()
        {
        }

        /// <summary>
        ///     Disables the polling mechanism 
        /// </summary>
        public override void Disable()
        {
        }


    }
}
