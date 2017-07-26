using SeniorDesign.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     A line that can simply exist in space.
    ///     This cannot be selected
    /// </summary>
    internal class DrawableLine : DrawableObject
    {
        /// <summary>
        ///     The start point of the line
        /// </summary>
        public Vector3 Start;

        /// <summary>
        ///     The end point of the line
        /// </summary>
        public Vector3 End;

        /// <summary>
        ///     Creates a new DrawableObject attached to a given core
        /// </summary>
        /// <param name="core">The streamline core to use</param>
        /// <param name="start">The starting point of the line</param>
        /// <param name="end">The ending point of the line</param>
        public DrawableLine(StreamlineCore core, Vector3 start, Vector3 end) : base(core)
        {
            Start = start;
            End = end;
            Z = 0;
        }

        /// <summary>
        ///     Draws this object on a specific GLControl
        /// </summary>
        public override void Draw()
        {
            if (!Visible) return;
            GL.PushMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Begin(PrimitiveType.Lines);
            {
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Vertex3(Start);
                GL.Vertex3(End);
            }
            GL.End();

            GL.PopMatrix();
        }

        /// <summary>
        ///     Deletes this object, and performs the updates in the core
        /// </summary>
        public override void Delete()
        {
        }

        /// <summary>
        ///     Checks to see if a point falls inside this object
        /// </summary>
        /// <param name="x">The X position to check (Absolute)</param>
        /// <param name="y">The Y position to check (Absolute)</param>
        /// <returns>True if the point falls inside this object</returns>
        public override bool IsPointInside(int x, int y)
        {
            // Cannot select lines
            return false;
        }

    }
}
