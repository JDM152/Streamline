﻿using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Filters;
using SeniorDesign.Core.Util;
using System;
using System.Collections.Generic;

namespace SeniorDesign.Plugins.Filters
{
    /// <summary>
    ///     A data filter that quantizes a signal
    /// </summary>
    public class QuantizerFilter : DataFilter
    {
        #region User Parameters

        /// <summary>
        ///     The lowest value in the range
        /// </summary>
        [UserConfigurableDouble(
            Name = "Minimum",
            Description = "The minimum value in the range (values below will snap to this)."
        )]
        public double Minimum = 0;

        /// <summary>
        ///     The highest value in the range
        /// </summary>
        [UserConfigurableDouble(
            Name = "Maximum",
            Description = "The maximum value in the range (values above will snap to this)."
        )]
        public double Maximum = 100;

        /// <summary>
        ///     The size of the steps
        /// </summary>
        [UserConfigurableDouble(
            Name = "Step Size",
            Description = "The size of each step in the range.",
            Minimum = 0.0000001
        )]
        public double StepSize = 10;

        #endregion

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        public override string InternalName { get { return "Quantizer"; } }

        /// <summary>
        ///     The number of input connections this connectable accepts.
        ///     -1 means an arbitrary number.
        /// </summary>
        public override int InputCount { get { return -1; } }

        /// <summary>
        ///     The number of output connections this connectable provides.
        /// </summary>
        public override int OutputCount { get { return InputCount; } }

        /// <summary>
        ///     The number of samples per field required to use this filter.
        ///     You may be given and use more, but will never be given less
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
            // Quantize all points in each channel
            while (data.MinCountOnAllChannels(1))
            {
                for (var k = 0; k < data.ChannelCount; k++)
                {
                    for (var j = 0; j < data[k].Count; j++)
                    {
                        var currentData = data[k][j];
                        if (currentData < Minimum)
                            data[k][j] = Minimum;
                        else if (currentData > Maximum)
                            data[k][j] = Maximum;
                        else
                            data[k][j] = Math.Floor((currentData - Minimum) / StepSize) * StepSize + Minimum;
                    }
                }
            }
            

            // Push to the next node, and clear the saved data
            core.PassDataToNextConnectable(this, data);
            data.Clear();
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
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Minimum));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(Maximum));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(StepSize));

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
            Minimum = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
            Maximum = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
            StepSize = ByteUtil.GetDoubleFromSizedArray(data, ref offset);
        }
    }
}
