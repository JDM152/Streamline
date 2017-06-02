
namespace SeniorDesign.Core
{
    /// <summary>
    ///     An interface for objects that can be chained together,
    ///     that take some data input and give some data output.
    /// </summary>
    public interface IConnectable
    {

        /// <summary>
        ///     If this connectable is currently active or not
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     An indentifier for this particular object.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     A name for this particular object type
        /// </summary>
        string InternalName { get; }

        /// <summary>
        ///     The name given to this object by the user to differentiate it
        ///     from the others.
        /// </summary>
        string Name { get; set; }

    }
}
