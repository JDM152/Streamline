using SeniorDesign.Core.Connections;

namespace SeniorDesign.Core
{
    /// <summary>
    ///     The main control class for the entire program.
    ///     Delegates work off to several smaller manager, each
    ///     responsible for their own work.
    /// </summary>
    public sealed class CoreManager
    {
        /// <summary>
        ///     The connection manager for input and output data
        /// </summary>
        private DataConnectionManager _dataConnectionManager;

    }
}
