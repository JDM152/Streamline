using SeniorDesign.Core;
using OpenTK.Graphics.OpenGL;
using System;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     A line connector between the input and output of two IConnectables
    /// </summary>
    internal class DrawableConnector : DrawableObject
    {

        /// <summary>
        ///     The IConnectable whose output this attaches to
        /// </summary>
        public readonly IConnectable Root;

        /// <summary>
        ///     The IConnectable whose input this attaches to
        /// </summary>
        public readonly IConnectable Child;

        /// <summary>
        ///     Creates a new DrawableObject attached to a given core
        /// </summary>
        /// <param name="core">The streamline core to use</param>
        /// <param name="root">The IConnectable this connects to the output of</param>
        /// <param name="child">The IConnectable this connects to the input of</param>
        public DrawableConnector(StreamlineCore core, IConnectable root, IConnectable child) : base(core)
        {
            Root = root;
            Child = child;
            Z = 1;
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
                GL.Vertex3(Root.PositionX + BlockEditorComponent.BOXWIDTH, Root.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Child.PositionX, Child.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
            }
            GL.End();

            GL.PopMatrix();
        }

        /// <summary>
        ///     Deletes this object, and performs the updates in the core
        /// </summary>
        public override void Delete()
        {
            Core.DisconnectConnectables(Root, Child);
        }

        /// <summary>
        ///     Checks to see if a point falls inside this object
        /// </summary>
        /// <param name="x">The X position to check (Absolute)</param>
        /// <param name="y">The Y position to check (Absolute)</param>
        /// <returns>True if the point falls inside this object</returns>
        public override bool IsPointInside(int x, int y)
        {
            var startX = (float) (Root.PositionX + BlockEditorComponent.BOXWIDTH);
            var startY = (float) (Root.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT);
            var endX = (float) (Child.PositionX);
            var endY = (float) (Child.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT);
            if (startX < endX)
            {
                var temp = startX;
                startX = endX;
                endX = temp;

                temp = startY;
                startY = endY;
                endY = temp;

            }

            if (startX > x || endX < x)
                return false;

            float AllowDif = 4.0f;
            float slope = (endY - startY) / (endX - startX);
            float expectedY = (x - startX) * slope + startY;
            return (Math.Abs(expectedY - y) <= AllowDif);
        }

    }
}
