using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that computes the rolling average of a given number
    /// </summary>
    public class RollingAverageFilter : DataFilter
    {
        #region User Configuration

        /// <summary>
        ///     The number of points used in calculating the rolling average
        /// </summary>
        [UserConfigurableInteger(
            Name = "History Count",
            Description = "The number of previous steps to use in the average",
            Minimum = 1
        )]
        public int SmoothingFactor = 3;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Rolling Average Filter"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return -1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        ///     -1 means the number of outputs matches the number of inputs
        /// </summary>
        public override int OutputCount { get { return -1; } }

        /// <summary>
        ///     The number of samples per field required to use this filter
        /// </summary>
        public override int InputLength { get { return SmoothingFactor; } }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Create the packet to return
            var toReturn = new DataPacket();
            toReturn.EnsureChannelCount(data.ChannelCount);

            // Loop through until no data is available
            while (data.MinCountOnAllChannels(SmoothingFactor))
            {
                for (var k = 0; k < data.ChannelCount; k++)
                {
                    // Grab the required amount, but only pop off the earliest
                    var fulldata = data.PeekRange(k, SmoothingFactor);
                    data.Pop(k);

                    // Calculate the average
                    var currentData = 0.0;
                    for (var j = 0; j < fulldata.Count; j++)
                        currentData += fulldata[j];
                    toReturn[k].Add(currentData / fulldata.Count);
                }
            }

            // Push the data
            core.PassDataToNextConnectable(this, toReturn);

        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            // Start constructing the data array
            var toReturn = new List<byte>(base.ToBytes());

            // Add all of the user configurable options
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(SmoothingFactor));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            // Restore the base first
            base.Restore(data, ref offset);

            // Restore all of the user configurable options
            SmoothingFactor = ByteUtil.GetIntFromSizedArray(data, ref offset);
        }

    }
}
