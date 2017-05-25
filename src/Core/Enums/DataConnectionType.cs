using System;

namespace SeniorDesign.Core.Enums
{
    /// <summary>
    ///     An enum representing various flags for data connections
    /// </summary>
    [Flags]
    public enum DataConnectionType
    {
        /// <summary>
        ///     If this data connection type is permissible as an input for data
        /// </summary>
        AllowAsInput = 1,

        /// <summary>
        ///     If this data connection type is permissible as an output for data
        /// </summary>
        AllowAsOutput = 2
    }
}
