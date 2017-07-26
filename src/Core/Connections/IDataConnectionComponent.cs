using System;
using System.Collections.Generic;

namespace SeniorDesign.Core.Connections
{
    /// <summary>
    ///     Any type of object that can be included in a DataConnection,
    ///     such as a Poller, DataConverter, or DataStream.
    /// </summary>
    public interface IDataConnectionComponent : IRestorable
    {
        /// <summary>
        ///     Any errors that this component has
        /// </summary>
        IList<string> ErrorStrings { get; }

        /// <summary>
        ///     Event that is triggered when the error strings have changed
        /// </summary>
        event EventHandler OnErrorStringsChanged;

        /// <summary>
        ///     Ensures that this object is valid before allowing it to be used
        /// </summary>
        /// <returns>True if the object is valid</returns>
        bool Validate();

        /// <summary>
        ///     Checks if this object needs to be compiled before it is actually used
        /// </summary>
        bool CanCompile { get; }

        /// <summary>
        ///     Checks if this object needs to be compiled (If any changes were made)
        /// </summary>
        bool NeedsCompile { get;}

        /// <summary>
        ///     Event that is triggered when the NeedsCompile value changes
        /// </summary>
        event EventHandler<bool> OnNeedsCompileChangeEvent;

        /// <summary>
        ///     Compiles this object for actual use
        /// </summary>
        void Compile();

    }
}
