using SeniorDesign.Core;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     A port where lines can be connected on some sort of Block
    /// </summary>
    internal class DrawablePort : DrawableObject
    {
        /// <summary>
        ///     The object that this belongs to
        /// </summary>
        public readonly IConnectable Owner;

        /// <summary>
        ///     If this port signifies an input or an output
        /// </summary>
        public readonly bool IsOutput;

        /// <summary>
        ///     Creates a new Drawable Filter from a specific DataFilter
        /// </summary>
        /// <param name="filter">The filter to draw for</param>
        public DrawablePort(StreamlineCore core, IConnectable c, bool output) : base(core)
        {
            Z = 0;
            IsOutput = output;
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

            
            GL.Begin(PrimitiveType.LineStrip);
            {
                if (IsOutput)
                {
                    GL.Vertex3(Owner.PositionX - BlockEditorComponent.LINELENGTH + BlockEditorComponent.BOXWIDTH, Owner.PositionY - BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Owner.PositionX + BlockEditorComponent.BOXWIDTH, Owner.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Owner.PositionX - BlockEditorComponent.LINELENGTH + BlockEditorComponent.BOXWIDTH, Owner.PositionY + BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                }
                else
                {
                    GL.Vertex3(Owner.PositionX - BlockEditorComponent.LINELENGTH, Owner.PositionY - BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Owner.PositionX, Owner.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                    GL.Vertex3(Owner.PositionX - BlockEditorComponent.LINELENGTH, Owner.PositionY + BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                }
            }
            GL.End();

            GL.PopMatrix();
        }

        /// <summary>
        ///     If this object is allowed to be deleted
        /// </summary>
        public override bool CanDelete { get { return false; } }

        /// <summary>
        ///     Deletes this object, and performs the updates in the core
        /// </summary>
        public override void Delete()
        {
            throw new System.Exception("Ports cannot be deleted.");
        }

        /// <summary>
        ///     Checks to see if a point falls inside this object
        /// </summary>
        /// <param name="x">The X position to check (Absolute)</param>
        /// <param name="y">The Y position to check (Absolute)</param>
        /// <returns>True if the point falls inside this object</returns>
        public override bool IsPointInside(int x, int y)
        {
            /*// This code was used previously, but I have not investigated it
            float allowedDif = 10.0f;
            Point inputPort = new Point(A.PositionX, A.PositionY + _height / 2);
            Point outputPort = new Point(A.PositionX + _width, A.PositionY + _height / 2);

            float difX = Math.Abs(inputPort.X - mouse.X);
            float difY = Math.Abs(inputPort.Y - mouse.Y);
            if (difX <= allowedDif && difY <= allowedDif)
            {
                return DrawableObjectType.InputPort;
            }
            difX = Math.Abs(outputPort.X - mouse.X);
            difY = Math.Abs(outputPort.Y - mouse.Y);
            if (difX <= allowedDif && difY <= allowedDif)
            {
                return DrawableObjectType.OutputPort;
            }
            return DrawableObjectType.Null;
            */

            var xoff = Owner.PositionX + (IsOutput ? 0 : BlockEditorComponent.BOXWIDTH) - BlockEditorComponent.LINELENGTH;
            var ystart = Owner.PositionY - BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT;
            var yend = Owner.PositionY + BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT;

            bool inX = x >= xoff && x <= xoff + BlockEditorComponent.LINELENGTH;
            bool inY = y >= ystart && y <= yend;
            return inX && inY;
        }

        /// <summary>
        ///     The position of the center of this particular port
        /// </summary>
        public Vector3 Center
        {
            get
            {
                return IsOutput 
                    ? new Vector3(Owner.PositionX + BlockEditorComponent.BOXWIDTH, Owner.PositionY + 0.5f * BlockEditorComponent.BOXHEIGHT, 0.0f)
                    : new Vector3(Owner.PositionX, Owner.PositionY + 0.5f * BlockEditorComponent.BOXHEIGHT, 0.0f);
            }
        }
    }
}
