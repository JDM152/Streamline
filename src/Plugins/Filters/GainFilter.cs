using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Attributes.Specialized;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that multiplies the values of a field
    /// </summary>
    [RenderIcon(Filename = "Gain")]
    public class GainFilter : DataFilter
    {
        #region User Configuration

        /// <summary>
        ///     The gain to apply to the input connection
        /// </summary>
        [UserConfigurableDouble(
            Name = "Gain",
            Description = "The value to multiply the incoming signal by"
        )]
        public double Gain = 1.0;

        #endregion


        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Gain Filter"; } }

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
        public GainFilter(StreamlineCore core) : base(core) { }

        /// <summary>
        ///     Accepts incoming data from a previous connection.
        ///     This is allowed to queue and store as needed.
        /// </summary>
        /// <param name="data">The data being pushed from the previous node</param>
        /// <param name="core">The Streamline program this is a part of</param>
        public override void AcceptIncomingData(StreamlineCore core, DataPacket data)
        {
            // Push the multiplied value to the next node
            var toReturn = new DataPacket();
            toReturn.AddChannel();

            while ((BatchMode && data[0].Count > 0) || toReturn[0].Count == 0)
                toReturn[0].Add(data.Pop(0) * Gain);

            core.PassDataToNextConnectable(this, toReturn);
        }

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public override byte[] ToBytes()
        {
            var toReturn = new List<byte>(base.ToBytes());

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Gain));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public override void Restore(byte[] data, ref int offset)
        {
            base.Restore(data, ref offset);

            Gain = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
        }

    }
}
