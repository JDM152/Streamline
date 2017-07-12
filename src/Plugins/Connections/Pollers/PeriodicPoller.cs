using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections.Pollers;
using System.Threading;

namespace SeniorDesign.Plugins.Connections.Pollers
{
    /// <summary>
    ///     A polling mechanism where data is polled periodically using a timer
    /// </summary>
    public class PeriodicPoller : PollingMechanism
    {

        #region User Configuration

        /// <summary>
        ///     The time between polling in ms
        /// </summary>
        [UserConfigurableInteger(
            Name = "Polling Time",
            Description = "The time between polling, in milliseconds",
            Minimum = 1
        )]
        public int PollingTime = 100;

        #endregion

        /// <summary>
        ///     Creates a new Periodic Poller
        /// </summary>
        /// <param name="core">The core that this reports back to</param>
        public PeriodicPoller(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     The timer used to trigger the polls
        /// </summary>
        protected Timer PollingTimer;

        /// <summary>
        ///     Enables the polling mechanism to take additional input.
        ///     This will always be called after the Connection has been linked.
        /// </summary>
        public override void Enable()
        {
            // Create the timer and start it
            if (PollingTimer == null)
                PollingTimer = new Timer(OnTimerCallback, PollingTime, PollingTime, PollingTime);
        }

        /// <summary>
        ///     Disables the polling mechanism 
        /// </summary>
        public override void Disable()
        {
            // Remove the timer
            if (PollingTimer != null)
                PollingTimer = null;
        }

        /// <summary>
        ///     Method triggered whenever the timer runs out
        /// </summary>
        /// <param name="state">The last time of the timer</param>
        private void OnTimerCallback(object state)
        {
            // Failsafe in case the timer was destroyed (should never trigger)
            if (PollingTimer == null)
                return;

            // Tell the connection to perform a poll
            Connection.Poll(Core);

            // Reset the timer as needed
            if ((int) state != PollingTime)
                PollingTimer.Change(PollingTime, PollingTime);
        }

    }
}
