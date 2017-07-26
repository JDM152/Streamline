﻿using SeniorDesign.Core;
using SeniorDesign.Core.Filters;
using OpenTK.Graphics.OpenGL;

namespace SeniorDesign.FrontEnd.Components.BlockEditor.Drawable
{
    /// <summary>
    ///     An object which holds a drawable filter object
    /// </summary>
    internal class DrawableFilter : DrawableIConnectable<DataFilter>
    {

        /// <summary>
        ///     Creates a new Drawable Filter from a specific DataFilter
        /// </summary>
        /// <param name="filter">The filter to draw for</param>
        public DrawableFilter(StreamlineCore core, DataFilter filter) : base(core, filter)
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

            // Main Box
            GL.Begin(PrimitiveType.LineStrip);
            {
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Vertex3(Object.PositionX, Object.PositionY, 0.0f);
                GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY, 0.0f);
                GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY + BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX, Object.PositionY + BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX, Object.PositionY, 0.0f);
            }
            GL.End();

            // Input Symbol
            GL.Begin(PrimitiveType.LineStrip);
            {
                GL.Vertex3(Object.PositionX - BlockEditorComponent.LINELENGTH, Object.PositionY - BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX, Object.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX - BlockEditorComponent.LINELENGTH, Object.PositionY + BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
            }
            GL.End();

            // Output Symbol
            GL.Begin(PrimitiveType.LineStrip);
            {
                GL.Vertex3(Object.PositionX - BlockEditorComponent.LINELENGTH + BlockEditorComponent.BOXWIDTH, Object.PositionY - BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX + BlockEditorComponent.BOXWIDTH, Object.PositionY + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
                GL.Vertex3(Object.PositionX - BlockEditorComponent.LINELENGTH + BlockEditorComponent.BOXWIDTH, Object.PositionY + BlockEditorComponent.LINELENGTH + 0.5 * BlockEditorComponent.BOXHEIGHT, 0.0f);
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
        public override bool IsPointInside(int x, int y)
        {
            bool inX = x >= Object.PositionX && x <= Object.PositionX + BlockEditorComponent.BOXWIDTH;
            bool inY = y >= Object.PositionY && y <= Object.PositionY + BlockEditorComponent.BOXHEIGHT;
            return inX && inY;
        }
    }
}