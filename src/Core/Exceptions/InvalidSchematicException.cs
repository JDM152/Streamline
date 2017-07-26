using System;

namespace SeniorDesign.Core.Exceptions
{
    /// <summary>
    ///     An exception thrown whenever a schematic file is corrupted
    /// </summary>
    public class InvalidSchematicException : Exception
    {
        /// <summary>
        ///     Creates a new exception about the schematic being invalid
        /// </summary>
        /// <param name="msg">The message for the exception</param>
        public InvalidSchematicException(string msg) : base(msg) { }

    }
}
