using SeniorDesign.Core;
using System.Collections.Generic;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     A drawable that encapsulates an IConnectable of some type
    /// </summary>
    internal abstract class DrawableIConnectable<T> : DrawableObject, IDrawableIConnectable where T : IConnectable
    {
        /// <summary>
        ///     The object that is being drawn
        /// </summary>
        public readonly T Object;

        /// <summary>
        ///     Creates a new Drawable Input from a specific DataConnection
        /// </summary>
        /// <param name="c">The connection to draw for</param>
        public DrawableIConnectable(StreamlineCore core, T c) : base(core)
        {
            Object = c;
        }

        /// <summary>
        ///     Gets the object associated with this IDrawableIConnectable
        /// </summary>
        /// <returns>The associated object</returns>
        public IConnectable GetObject()
        {
            return Object;
        }

        /// <summary>
        ///     The objects that are related, but not directly owned by this object
        /// </summary>
        public IList<DrawableObject> MappedObjects { get; protected set; } = new List<DrawableObject>();
    }

    /// <summary>
    ///     An interface encompassing the DrawableIConnectable class so
    ///     that we may retrieve the object generically
    /// </summary>
    internal interface IDrawableIConnectable
    {
        /// <summary>
        ///     Gets the object associated with this IDrawableIConnectable
        /// </summary>
        /// <returns>The associated object</returns>
        IConnectable GetObject();

        /// <summary>
        ///     The objects that are related, but not directly owned by this object
        /// </summary>
        IList<DrawableObject> MappedObjects { get; }


    }
}
