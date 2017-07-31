using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that delays input by a specified number of ticks
    /// </summary>
    public class DelayFilter : DataFilter
    {
        #region User Configuration

        /// <summary>
        ///     The gain to apply to the input connection
        /// </summary>
        [UserConfigurableInteger(
            Name = "Delay",
            Description = "The number of ticks to delay the values by"
        )]
        public int Delay = 1;

        #endregion


        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Delay Filter"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return 1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     -1 means the inputs match the outputs
        /// </summary>
        public override int OutputCount { get { return 1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return 1; } }

        /// <summary>
        ///     Creates a new Addition Filter
        /// </summary>
        public DelayFilter(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Only push if the number of points waiting is greater than the delay
            if (data[0].Count > Delay)
                core.PassDataToNextConnectable(this, data.Pop(0));
        }

    }
}
