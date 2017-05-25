using System;

namespace SeniorDesign.Core.Exceptions
{
    /// <summary>
    ///     An exception that is thrown whenever an invalid operation is attempted to be performed.
    ///     This can occur for both developent-related, and user interaction.
    /// </summary>
    public class InvalidOperationException : Exception
    {

        /// <summary>
        ///     Creates a new exception about generic invalid operations
        /// </summary>
        /// <param name="msg">The message for the exception</param>
        public InvalidOperationException(string msg) : base(msg) { }

    }
}
