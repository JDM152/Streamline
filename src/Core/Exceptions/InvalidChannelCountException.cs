using System;

namespace SeniorDesign.Core.Exceptions
{
    /// <summary>
    ///     An exception thrown whenever a data connection with the wrong number of
    ///     data channels is attempted to be made
    /// </summary>
    public class InvalidChannelCountException : Exception
    {
        /// <summary>
        ///     Creates a new exception about the invalid number of channels
        /// </summary>
        /// <param name="msg">The message for the exception</param>
        public InvalidChannelCountException(string msg) : base(msg) { }

    }
}
