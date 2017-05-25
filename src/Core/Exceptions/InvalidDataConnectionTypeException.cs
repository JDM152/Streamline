using System;

namespace SeniorDesign.Core.Exceptions
{
    /// <summary>
    ///     An exception thrown whenever a data connection is attempted to be used
    ///     in a situation where it is forbidden.
    /// </summary>
    public class InvalidDataConnectionTypeException : Exception
    {
        /// <summary>
        ///     Creates a new exception about the invalid use of a Data Connection
        /// </summary>
        /// <param name="msg">The message for the exception</param>
        public InvalidDataConnectionTypeException(string msg) : base(msg) { }

    }
}
