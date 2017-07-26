using SeniorDesign.Core;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     An object that can be drawn
    /// </summary>
    internal abstract class DrawableObject
    {
        /// <summary>
        ///     If this object can be seen or not
        /// </summary>
        public bool Visible = true;

        /// <summary>
        ///     The Z position on-screen (For sorting)
        /// </summary>
        public int Z = 10;

        /// <summary>
        ///     The core that this object belongs to
        /// </summary>
        protected StreamlineCore Core;

        /// <summary>
        ///     Creates a new DrawableObject attached to a given core
        /// </summary>
        /// <param name="core">The streamline core to use</param>
        public DrawableObject(StreamlineCore core)
        {
            Core = core;
        }

        /// <summary>
        ///     Draws this object on a specific GLControl
        /// </summary>
        public abstract void Draw();

        /// <summary>
        ///     If this object is allowed to be deleted
        /// </summary>
        public virtual bool CanDelete { get { return true; } }

        /// <summary>
        ///     Deletes this object, and performs the updates in the core
        /// </summary>
        public abstract void Delete();

        /// <summary>
        ///     Checks to see if a point falls inside this object
        /// </summary>
        /// <param name="x">The X position to check (Absolute)</param>
        /// <param name="y">The Y position to check (Absolute)</param>
        /// <returns>True if the point falls inside this object</returns>
        public abstract bool IsPointInside(int x, int y);
    }
}
