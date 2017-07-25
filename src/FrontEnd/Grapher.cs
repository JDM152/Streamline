using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK;
using System;

namespace SeniorDesign.FrontEnd
{
    class Grapher
    {
        private float[][] value = null;
        private float[][] time = null;
        private Color[] color = null;
        private bool[] isDotLine = null;
        private bool canDraw = false;
        private float xSpan = 10.0f;
        private float ySpan = 10.0f;
        private GLControl glControl;

        public Grapher(GLControl glControl)
        {
            this.glControl = glControl;
        }

        public void start()
        {
            canDraw = true;
        }
        public void stop()
        {
            canDraw = false;
        }
        public void updateData(float[][] value, float[][] time, Color[] color, bool[] isDotLine)
        {
            //pass by reference
            this.value = value;
            this.time = time;
            this.color = color;
            this.isDotLine = isDotLine;
        }
        public void updateXspan(float xSpan)
        {
            this.xSpan = xSpan;
        }
        public void updateYspan(float ySpan)
        {
            this.ySpan = ySpan;
        }
        public void render()
        {
            glControl.MakeCurrent();
            GL.PushMatrix();
            //clean the color
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //region can be viewed
            if (canDraw)
            {
                int length = value.GetLength(0);
                GL.Ortho(-xSpan / 2, xSpan / 2, -ySpan / 2, ySpan / 2, -length, 1);
                float lastTime = 0.0f;
                for (int i = 0; i < length; i++)
                {
                    float temp = time[i][time[i].Length - 1];
                    if (temp > lastTime)
                    {
                        lastTime = temp;
                    }
                }
                for (int i = 0; i < length; i++)
                {
                    drawData(value[i], time[i], color[i], isDotLine[i], lastTime - xSpan / 2, i);
                }
            }
            GL.PopMatrix();
            glControl.SwapBuffers();
        }
        private void drawData(float[] var, float[] tim, Color col, bool dot, float dif, float z)
        {
            GL.PushMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Color3(col);
            if (dot)
            {
                GL.Enable(EnableCap.LineStipple);
                short pattern = Convert.ToInt16("AAAA", 16);
                GL.LineStipple(2, pattern);
            }
            else
            {
                GL.Disable(EnableCap.LineStipple);
            }
            GL.Begin(PrimitiveType.LineStrip);
            for (int i = 0; i < var.Length; i++)
            {
                GL.Vertex3(tim[i] - dif, var[i], z);
            }
            GL.End();
            GL.PopMatrix();
        }
    }

}