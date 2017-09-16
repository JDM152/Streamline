using SeniorDesign.Core;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SeniorDesign.FrontEnd.BlockEditor;
using System.Drawing.Imaging;
using SeniorDesign.Core.Attributes.Specialized;
using System.Reflection;

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
        ///     The bitmap that is used as the 'texture' for this sprite
        /// </summary>
        protected Bitmap TextureBitmap;

        /// <summary>
        ///     The rectangle that shows what the source of the sprite is
        /// </summary>
        public Rectangle SourceRect;

        /// <summary>
        ///     The ID given to the texture this sprite renders
        /// </summary>
        protected uint TextureID;

        /// <summary>
        ///     Creates a new Drawable Input from a specific DataConnection
        /// </summary>
        /// <param name="c">The connection to draw for</param>
        public DrawableIConnectable(StreamlineCore core, T c) : base(core)
        {
            Object = c;

            // Try and get a bitmap to render
            var attrib = c.GetType().GetCustomAttribute<RenderIconAttribute>();
            if (attrib == null)
                SetBitmap("Filter");
            else
                SetBitmap(attrib.Filename);
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

        /// <summary>
        ///     If this block is currently being processed in the core
        /// </summary>
        public bool IsProcessing { get; set; }

        /// <summary>
        ///     Sets the bitmap that this sprite displays from a given filename
        /// </summary>
        /// <param name="filename">THe name of the bitmap to show</param>
        public void SetBitmap(string filename)
        {
            // Generate the GL texture slot
            GL.GenTextures(1, out TextureID);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            // Load the bitmap, and lock the bits for editing
            TextureBitmap = ImageCache.LoadBitmap(this, "Resources/Icons/" + filename + ".png");
            SourceRect = new Rectangle(0, 0, TextureBitmap.Width, TextureBitmap.Height);
            var data = TextureBitmap.LockBits(SourceRect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Link the texture to the slot
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            // Allow the bitmap to be edited again
            TextureBitmap.UnlockBits(data);

            // Set the texture's operation modes
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) TextureWrapMode.Clamp);

        }

        /// <summary>
        ///     Renders this sprite to the world
        /// </summary>
        public override void Draw()
        {
            // Don't render if nothing to show
            if (!Visible) return;
            if (TextureBitmap == null) return;

            GL.PushMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Enable texturess with alpha blending
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.ColorMaterial);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            // Move the sprite to the correct position
            GL.Translate(Object.PositionX, Object.PositionY, 0);

            // Draw a quad with the sprite inside
            GL.Begin(PrimitiveType.Quads);
            {
                if (Object.Enabled)
                {
                    if (IsProcessing)
                        GL.Color3(0.0f, 1.0f, 0.0f);
                    else if (Highlighted)
                        GL.Color3(1.0f, 1.0f, 1.0f);
                    else
                        GL.Color3(0.9f, 0.9f, 0.9f);
                }
                else
                {
                    if (Highlighted)
                        GL.Color3(0.5f, 0.5f, 0.5f);
                    else
                        GL.Color3(0.25f, 0.25f, 0.25f);
                }

                GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
                GL.TexCoord2(1, 0); GL.Vertex2(TextureBitmap.Width, 0);
                GL.TexCoord2(1, 1); GL.Vertex2(TextureBitmap.Width, TextureBitmap.Height);
                GL.TexCoord2(0, 1); GL.Vertex2(0, TextureBitmap.Height);
            }
            GL.End();

            // Unbind the texture
            GL.BindTexture(TextureTarget.Texture2D, 0);

            GL.PopMatrix();
        }
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

        /// <summary>
        ///     If this block is currently being processed in the core
        /// </summary>
        bool IsProcessing { get; set; }
    }
}
