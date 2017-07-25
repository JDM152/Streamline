﻿using SeniorDesign.Core;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that combines two data streams using addition
    /// </summary>
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
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Return a new packet with the sum
            var toReturn = new DataPacket();
            toReturn.AddChannel();

            while (data.MinCountOnAllChannels(1))
            {
                toReturn[0].Add(0);
                for (var k = 0; k < data.ChannelCount; k++)
                    toReturn[0][toReturn[0].Count - 1] += data.Pop(k);
            } 

            // Push to the next node
            core.PassDataToNextConnectable(this, toReturn);
        }

    }
}
