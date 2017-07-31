using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Util;
using System.Collections.Generic;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     A container for settings for StreamlineCore
    /// </summary>
    public sealed class CoreSettings : IRestorable
    {
        /// <summary>
        ///     The number of bytes that an input can take at a time
        /// </summary>
        [UserConfigurableInteger(
            Name = "Input Buffer",
            Description = "The maximum number of bytes to read from streams at a time",
            Minimum = 8
        )]
        public int InputBuffer = 4096;

        /// <summary>
        ///     The number of milliseconds per tick
        /// </summary>
        [UserConfigurableInteger(
            Name = "Tick Time",
            Description = "The number of milliseconds between each tick of simulation",
            Minimum = 1
        )]
        public int TickTime = 10;

        /// <summary>
        ///     If the tick time should be ignored, and run as fast as possible instead
        /// </summary>
        [UserConfigurableBoolean(
            Name = "Unlimited Tickrate",
            Description = "If enabled, the tickrate will not be limited"
        )]
        public bool UnlimitedTickrate = false;

        /// <summary>
        ///     If debug mode is currently enabled
        /// </summary>
        public bool DebugMode = true;

        /// <summary>
        ///     Converts this object into a byte array representation
        /// </summary>
        /// <returns>This object as a restoreable byte array</returns>
        public byte[] ToBytes()
        {
            var toReturn = new List<byte>();

            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(InputBuffer));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(TickTime));
            toReturn.AddRange(ByteUtil.GetSizedArrayRepresentation(UnlimitedTickrate));

            return toReturn.ToArray();
        }

        /// <summary>
        ///     Restores the state of this object from the data of ToBytes()
        /// </summary>
        /// <param name="data">The data to restore from</param>
        /// <param name="offset">The offset into the data to start</param>
        public void Restore(byte[] data, ref int offset)
        {
            InputBuffer = ByteUtil.GetIntFromSizedArray(data, ref offset);
            TickTime = ByteUtil.GetIntFromSizedArray(data, ref offset);
            UnlimitedTickrate = ByteUtil.GetBoolFromSizedArray(data, ref offset);
        }
    }
}
