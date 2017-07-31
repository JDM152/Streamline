using SeniorDesign.Core;
using OpenTK.Graphics.OpenGL;
using SeniorDesign.Core.Connections;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     An object which holds a drawable input/output object
    /// </summary>
    internal class DrawableInputOutput : DrawableIConnectable<DataConnection>
    {

        /// <summary>
        ///     Checks if this is an input or an output
        /// </summary>
        public bool IsOutput { get { return Object.IsOutput; } }

        /// <summary>
        ///     Creates a new Drawable Input from a specific DataConnection
        /// </summary>
        /// <param name="c">The connection to draw for</param>
        public DrawableInputOutput(StreamlineCore core, DataConnection c) : base(core, c)
        {
            Z = 2;
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


            GL.LineWidth(Highlighted ? 2.0f : 1.0f);
            GL.Begin(PrimitiveType.LineLoop);
            {
                if (Object.Enabled)
                {
                    if (IsProcessing)
                    {
                        GL.Color3(0.0f, 1.0f, 0.1f);
                    }
                    else
                    {
                        GL.Color3(0.0f, 0.0f, 0.0f);
                    }
                }
                else
                {
                    GL.Color3(1.0f, 0.1f, 0.2f);
                }
                if (IsOutput)
                {
                    // Output Block
                    GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY, 0.0f);
                    GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY + BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Object.PositionX, Object.PositionY + BlockEditorComponent.BOXHEIGHT * 0.5, 0.0f);
                }
                else
                {
                    // Input Block
                    GL.Vertex3(Object.PositionX, Object.PositionY, 0.0f);
                    GL.Vertex3(Object.PositionX, Object.PositionY + BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY + BlockEditorComponent.BOXHEIGHT * 0.5, 0.0f);
                }
            }
            GL.End();

            GL.PopMatrix();
        }

        /// <summary>
        ///     Deletes this object, and performs the updates in the core
        /// </summary>
        public override void Delete()
        {
            Core.DeleteConnectable(Object);
        }

        /// <summary>
        ///     Checks to see if a point falls inside this object
        /// </summary>
        /// <param name="x">The X position to check (Absolute)</param>
        /// <param name="y">The Y position to check (Absolute)</param>
        /// <returns>True if the point falls inside this object</returns>
        public override bool IsPointInside(float x, float y)
        {
            bool inX = x >= Object.PositionX && x <= Object.PositionX + BlockEditorComponent.BOXWIDTH;
            bool inY = y >= Object.PositionY && y <= Object.PositionY + BlockEditorComponent.BOXHEIGHT;
            return inX && inY;
        }
    }
}
