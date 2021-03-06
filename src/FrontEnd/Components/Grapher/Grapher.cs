﻿using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;
using System;
using SeniorDesign.Core;
using System.Drawing.Imaging;
using SeniorDesign.FrontEnd.Windows;

namespace SeniorDesign.FrontEnd
{
    class Grapher
    {
        //default
        private const int HISTORY = 200;
        private GLControl glControl;
        private float minY = -10.0f;
        private float maxY = 10.0f;
        private float average = 0.0f;
        private Color backgroundColor = Color.White;
        private Color renderColor = Color.Black;
        private Font font;
        private float textHeight = 5.0f;
        /// <summary>
        ///     This class take glControl to render
        /// </summary>
        public Grapher(GLControl glControl)
        {
            this.glControl = glControl;
            FontFamily fontFamily = new FontFamily("Times New Roman");
            font = new Font(fontFamily, 12, FontStyle.Regular, GraphicsUnit.Pixel);
        }
        /// <summary>
        ///     this function take a datapacket and render it 
        /// </summary>
        public void Draw(DataPacket packet)
        {
            lock (ControlPanel.RenderLock)
            {
                glControl.MakeCurrent();
                GL.PushMatrix();
                GL.ClearColor(backgroundColor);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                GL.Viewport(0, 0, glControl.Width, glControl.Height * 4 / 5);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                getSta(packet);
                GL.Ortho(1, HISTORY, minY, maxY, -1, 1);
                drawData(packet);
                GL.PopMatrix();
                //different viewport
                GL.PushMatrix();
                GL.Viewport(0, glControl.Height * 3 / 4, glControl.Width, glControl.Height * 1 / 5);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.Ortho(0, 1, 1, 0, -1, 1);
                GL.Enable(EnableCap.Texture2D);
                drawStatitics(packet);
                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                glControl.SwapBuffers();
                glControl.Context.MakeCurrent(null);
                deleteHistory(packet);
            }
        }
        /// <summary>
        ///     this function set the max height of the graph
        /// </summary>
        public void SetHeight(float minY, float maxY)
        {
            this.minY = minY;
            this.maxY = maxY;
        }
        /// <summary>
        ///     this function set the color of rendered line
        /// </summary>
        public void SetLineColor(int rgb)
        {
            //make it transparent, otherwise it bleeds the textures
            int alpha = 255 << 24;
            int argb = alpha | rgb;
            renderColor = Color.FromArgb(argb);
        }
        /// <summary>
        ///     this function set the color of background
        /// </summary>
        public void SetBackgroundColor(int rgb)
        {
            //make it transparent, otherwise it bleeds the textures
            int alpha = 255 << 24;
            int argb = alpha | rgb;
            backgroundColor = Color.FromArgb(argb);
        }
        /// <summary>
        ///     this function render datapacket 
        /// </summary>
        private void drawData(DataPacket packet)
        {

            GL.PushMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Color3(renderColor);
            GL.Begin(PrimitiveType.LineStrip);
            int currentX = HISTORY;
            for (int i = packet[0].Count - 1; i >= 0 && i >= packet[0].Count - HISTORY; i--)
            {
                GL.Vertex3(currentX, packet[0][i], 0.0);
                currentX--;
            }
            GL.End();
            GL.PopMatrix();
        }
        /// <summary>
        ///     this function delete unneeded data
        /// </summary>
        private void deleteHistory(DataPacket packet)
        {

            while (packet[0].Count > HISTORY)
            {
                packet.Pop(0);
            }
        }
        /// <summary>
        ///     this function convert string to bitmap image
        /// </summary>
        private Bitmap DrawText(String text, Font font, Color textColor, Color backColor)
        {
            Bitmap img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            SizeF textSize = drawing.MeasureString(text, font);
            img.Dispose();
            drawing.Dispose();
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);
            drawing = Graphics.FromImage(img);
            drawing.Clear(backColor);
            Brush textBrush = new SolidBrush(textColor);
            drawing.DrawString(text, font, textBrush, 0, 0);
            textBrush.Dispose();
            drawing.Dispose();
            return img;
        }
        /// <summary>
        ///     this function convert bitmap to a texture2D
        /// </summary>
        private int LoadTexture(Bitmap temp)
        {
            Bitmap bitmap = temp;

            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return tex;
        }
        /// <summary>
        ///     this function draw statitics
        /// </summary>
        private void drawStatitics(DataPacket packet)
        {
            
            String maxText = "Max: " + maxY.ToString() + "\n";
            String minText = "Min: " + minY.ToString() + "\n";
            String avgText = "AVG: " + average.ToString() + "\n";
            Bitmap tempImg = DrawText(maxText + minText + avgText, font, renderColor, backgroundColor);
            int textureID = LoadTexture(tempImg);
            renderText(textureID);
            //discard it after rendering
            GL.DeleteTexture(textureID);
        }
        /// <summary>
        ///     this function render statitics
        /// </summary>
        private void renderText(int textureID)
        {
            GL.PushMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Disable(EnableCap.Lighting);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(Color.Transparent);
            GL.TexCoord2(0, 0);
            //150 and 200 are magic x value, fell free to change
            GL.Vertex3(0.5, 0, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex3(1, 0, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex3(1, 1, 0);
            GL.TexCoord2(0, 1);
            GL.Vertex3(0.5, 1, 0);
            GL.End();
            GL.PopMatrix();
        }

        private void getSta(DataPacket packet)
        {
            double max = packet[0][packet[0].Count - 1];
            double min = packet[0][packet[0].Count - 1];
            double avg = packet[0][packet[0].Count - 1];
            for (int i = packet[0].Count - 2; i >= 0 && i >= packet[0].Count - HISTORY; i--)
            {
                double temp = packet[0][i];
                if (temp > max)
                {
                    max = temp;
                }
                if (temp < min)
                {
                    min = temp;
                }
                avg += temp;
            }
            avg /= HISTORY;

            minY = (float)min;
            maxY = (float)max;
            average = (float)avg;
        }
    }
}