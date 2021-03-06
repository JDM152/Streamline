﻿using SeniorDesign.Core;
using SeniorDesign.Core.Attributes.Specialized;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that combines two data streams using addition
    /// </summary>
    [RenderIcon(Filename = "Addition")]
    public class AdditionFilter : DataFilter
    {

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Addition Combination Filter"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return -1; } }

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
        public AdditionFilter(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            var toReturn = new DataPacket();
            toReturn.AddChannel();

            // Sum every available channel
            while ((BatchMode && data.EnsureMinCountOnAllChannels(1, 0)) || (toReturn[0].Count == 0 && data.EnsureMinCountOnAllChannels(1, 0)))
            {
                var val = 0.0;
                for (var k = 0; k < data.ChannelCount; k++)
                    val += data.Pop(k);
                toReturn[0].Add(val);
            }

            // Push to the next node
            core.PassDataToNextConnectable(this, toReturn);
        }

    }
}
