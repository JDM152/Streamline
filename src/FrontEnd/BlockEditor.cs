using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using OpenTK;
using System.Drawing.Imaging;
using System.Collections.Generic;
using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.FrontEnd
{

    class BlockEditor
    {
        private GLControl glControl;
        private StreamlineCore core;
        private DataConnection input;
        private DataConnection output;
        private List<DataFilter> filterList;
        //const
        private const int width = 40;
        private const int height = 40;
        private const int lineLength = 5;
        //input 
        //drag = down to up
        private Point downPoint;
        private Point upPoint;
        private IDstruct lastSelect;
        private IDstruct currentSelect;
        private Point mouse;


        public BlockEditor(GLControl glControl, StreamlineCore core)
        {
            this.glControl = glControl;
            this.core = core;
            filterList = new List<DataFilter>();
            foreach (IConnectable temp in core.Nodes)
            {
                DataFilter tempFitler = temp as DataFilter;
                if ( tempFitler != null)
                {
                    filterList.Add(tempFitler);
                }
                else
                {
                    DataConnection tempConnection = temp as DataConnection;
                    if(tempConnection.IsOutput == false)
                    {
                        input = tempConnection;
                    }
                    else
                    {
                        output = tempConnection;
                    }
                }
            }
        }
        public void reset()
        {
            currentSelect = new IDstruct(ObjectType.Ouput, -1, -1);
        }
        public void updateMouse(Point mouse)
        {
            this.mouse = mouse;
        }
        public void updateUpMouse()
        {
            upPoint = mouse;
            handleMouseClick();
         }
        public void updateDownMouse()
        {
            downPoint = mouse;
            lastSelect = currentSelect;
            currentSelect = getObject();
        }
        public void handleMouseClick()
        {
            if (currentSelect.objectType == ObjectType.Block)
            {
                if (upPoint != downPoint)
                {
                    drag(currentSelect.ID, downPoint);
                }
            }
            else if (currentSelect.objectType !=  ObjectType.Null && currentSelect.objectType != ObjectType.Line 
                && lastSelect.objectType != ObjectType.Null && lastSelect.objectType != ObjectType.Line)
            {
                if (currentSelect.ID != lastSelect.ID)
                {
                    ConnectBlocks(lastSelect.ID, currentSelect.ID);
                }
            }
        }
        public void handleDelete()
        {
            if(currentSelect.objectType == ObjectType.Line)
            {
                DisconnectBlocks(currentSelect.ID, currentSelect.ID2);
            }
            else if(currentSelect.objectType == ObjectType.Block)
            {
                DeleteBlock(currentSelect.ID);
            }
            else if(currentSelect.objectType == ObjectType.Input)
            {
                input = null;
            }
            else if(currentSelect.objectType == ObjectType.Ouput)
            {
                output = null;
            }
        }
        public void drag(int Id, Point newPosition)
        {
            foreach (DataFilter tempFilter in filterList)
            {
                if (tempFilter.Id == Id)
                {
                    tempFilter.PositionX = newPosition.X;
                    tempFilter.PositionY = newPosition.Y;
                }
            }
            //TO DO CORE
        }

        public void CreatBlock(IConnectable temp)
        {
            DataFilter tempFitler = temp as DataFilter;
            if (tempFitler != null)
            {
                filterList.Add(tempFitler);
            }
            else
            {
                DataConnection tempConnection = temp as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    input = tempConnection;
                }
                else
                {
                    output = tempConnection;
                }
            }
        }
        public void UpdateBlockPosition(IConnectable temp)
        {
            DataFilter tempFitler = temp as DataFilter;
            if (tempFitler != null)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if(tempFitler.Id == temp.Id)
                    {
                        tempFilter.PositionX = temp.PositionX;
                        tempFilter.PositionY = temp.PositionY;
                    }
                }
            }
            else
            {
                DataConnection tempConnection = temp as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    input.PositionX = tempConnection.PositionX;
                }
                else
                {
                    output.PositionY = tempConnection.PositionY;
                }
            }
        }
        public void ConnectBlocks(IConnectable A, IConnectable B)
        {
            var tempFitler = A as DataFilter;
            if (tempFitler != null)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if (tempFitler.Id == A.Id)
                    {
                        tempFilter.NextConnections.Add(B);
                        return;
                    }
                }
            }
            else
            {
                DataConnection tempConnection = A as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    A.NextConnections.Add(B);
                }
            }
            
        }
        public void ConnectBlocks(int aid, int bid)
        {
            if (aid == input.Id)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if (tempFilter.Id == bid)
                    {
                        input.NextConnections.Add(tempFilter);
                        return;
                    }
                }
                input.NextConnections.Add(output);
            }
            else if(bid == output.Id)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if (tempFilter.Id == aid)
                    {
                        tempFilter.NextConnections.Add(output);
                        return;
                    }
                }
            }
            else
            {
                int temp1 = -1;
                int temp2 = -1;
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == aid)
                    {
                        temp1 = i;
                    }
                    else if(filterList[i].Id == bid)
                    {
                        temp2 = i;
                    }
                 }
                filterList[temp1].NextConnections.Add(filterList[temp2]);
            }
            //core to do
        }
        public void DisconnectBlocks(IConnectable A, IConnectable B)
        {
            DataFilter tempFitler = A as DataFilter;
            if (tempFitler != null)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if (tempFitler.Id == A.Id)
                    {
                        tempFilter.NextConnections.Remove(B);
                    }
                }
            }
            else
            {
                DataConnection tempConnection = A as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    A.NextConnections.Remove(B);
                }
            }
        }
        public void DisconnectBlocks(int aid, int bid)
        {

            int temp1 = -1;
            int temp2 = -1;
            for (int i = 0; i < filterList.Count; i++)
            {
                if (filterList[i].Id == aid)
                {
                    temp1 = i;
                }
                else if (filterList[i].Id == bid)
                {
                    temp2 = i;
                }
            }
            filterList[temp1].NextConnections.Remove(filterList[temp2]);

            //to do core
        }
        public void DeleteBlock(int id)
        {
            int temp1 = -1;
            for (int i = 0; i < filterList.Count; i++)
            {
                if (filterList[i].Id == id)
                {
                    temp1 = i;
                    return;
                }
            }
            filterList.RemoveAt(temp1);
            //to do core
        }
        public void DeleteBlock(IConnectable temp)
        {
            DataFilter tempFitler = temp as DataFilter;
            if (tempFitler != null)
            {
                for(int i = 0; i< filterList.Count; i++)
                {
                    if(filterList[i].Id == temp.Id)
                    {
                        filterList.RemoveAt(i);
                    }
                }
            }
            else
            {
                DataConnection tempConnection = temp as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    input = null;
                }
                else
                {
                    output = null;
                }
            }
        }
        public void SetName(IConnectable temp, String name)
        {
            DataFilter tempFitler = temp as DataFilter;
            if (tempFitler != null)
            {
                foreach (DataFilter tempFilter in filterList)
                {
                    if (tempFitler.Id == temp.Id)
                    {
                        tempFilter.Name = name;
                        return;
                    }
                }
            }
            else
            {
                DataConnection tempConnection = temp as DataConnection;
                if (tempConnection.IsOutput == false)
                {
                    input.Name = name;
                }
                else
                {
                    output.Name = name;
                }
            }
        }
        public IDstruct getObject()
        {
            foreach (DataFilter tempFilter in filterList)
            {
                foreach (IConnectable next in tempFilter.NextConnections)
                {
                    bool contact = false;
                    if(tempFilter.PositionX < next.PositionX)
                    {
                       contact = isOnLine(new Point(tempFilter.PositionX, tempFilter.PositionY), new Point(next.PositionX, next.PositionY), downPoint);
                    }
                    else
                    {
                        contact = isOnLine(new Point(next.PositionX, next.PositionY), new Point(tempFilter.PositionX, tempFilter.PositionY), downPoint);
                    }
                    if(contact)
                    {
                        return new IDstruct(ObjectType.Line, tempFilter.Id, next.Id);
                    }
                }
            }
            foreach (DataFilter tempFilter in filterList)
            {
                bool contact = isInsideBlock(new Point(tempFilter.PositionX, tempFilter.PositionY), downPoint);
                if(contact)
                {
                    return new IDstruct(ObjectType.Block, tempFilter.Id, -1);
                }
            }
            if(isInsideBlock(new Point(input.PositionX, input.PositionY), downPoint))
            {
                return new IDstruct(ObjectType.Input, input.Id, -1);
            }
            if (isInsideBlock(new Point(output.PositionX, output.PositionY), downPoint))
            {
                return new IDstruct(ObjectType.Ouput, output.Id, -1);
            }
            return new IDstruct(ObjectType.Null, output.Id, -1);

        }
        // left must be smaller
        public bool isInsideBlock(Point block, Point mouse)
        {
            bool inX = mouse.X >= block.X && mouse.X <= block.X + width;
            bool inY = mouse.Y >= block.Y && mouse.Y <= block.Y + height;
            return inX && inY;
        }
        public bool isOnLine(Point start, Point end, Point mouse)
        {
            if (mouse.X > end.X || mouse.X < start.X)
            {
                return false;
            }
            float AllowDif = 3.0f;
            float slope = (end.Y - start.Y)/ (end.X - start.X);
            float expectedY = (mouse.X - start.X) * slope + start.Y;
            if ( Math.Abs(expectedY - mouse.Y) <= AllowDif)
            {
                return true;
            }
            else
            {
                return false;
            }
         }
        public void renderBlock()
        {
            foreach(DataFilter tempFilter in filterList)
            {
                GL.PushMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Begin(PrimitiveType.LineStrip);
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Vertex3(tempFilter.PositionX, tempFilter.PositionY, 0.0f);
                GL.Vertex3(tempFilter.PositionX + width, tempFilter.PositionY, 0.0f);
                GL.Vertex3(tempFilter.PositionX + width, tempFilter.PositionY + height, 0.0f);
                GL.Vertex3(tempFilter.PositionX, tempFilter.PositionY + height, 0.0f);
                GL.Vertex3(tempFilter.PositionX, tempFilter.PositionY, 0.0f);
                GL.End();
                GL.Begin(PrimitiveType.LineStrip);
                GL.Vertex3(tempFilter.PositionX - lineLength, tempFilter.PositionY - lineLength + 0.5*height, 0.0f);
                GL.Vertex3(tempFilter.PositionX, tempFilter.PositionY + 0.5 * height, 0.0f);
                GL.Vertex3(tempFilter.PositionX - lineLength, tempFilter.PositionY + lineLength + 0.5 * height, 0.0f);
                GL.End();
                GL.Begin(PrimitiveType.LineStrip);
                GL.Vertex3(tempFilter.PositionX - lineLength + width, tempFilter.PositionY - lineLength + 0.5 * height, 0.0f);
                GL.Vertex3(tempFilter.PositionX + width, tempFilter.PositionY + 0.5 * height, 0.0f);
                GL.Vertex3(tempFilter.PositionX - lineLength + width, tempFilter.PositionY + lineLength + 0.5 * height, 0.0f);
                GL.End();
                GL.PopMatrix();
            }
        }
        public void renderLine()
        {
            foreach (DataFilter tempFilter in filterList)
            {
                if (tempFilter.NextConnections != null)
                {
                    foreach (IConnectable next in tempFilter.NextConnections)
                    {
                        GL.PushMatrix();
                        GL.MatrixMode(MatrixMode.Modelview);
                        GL.LoadIdentity();
                        GL.Begin(PrimitiveType.Lines);
                        GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                        GL.Vertex3(tempFilter.PositionX + width, tempFilter.PositionY + 0.5 * height, 0.0f);
                        GL.Vertex3(next.PositionX, tempFilter.PositionY + 0.5 * height, 0.0f);
                        GL.End();
                        GL.PopMatrix();
                    }
                }
            }
        }
        public void renderOutput()
        {
            if (output != null)
            {
                GL.PushMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Begin(PrimitiveType.LineStrip);
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Vertex3(output.PositionX + width, output.PositionY, 0.0f);
                GL.Vertex3(output.PositionX + width, output.PositionY + height, 0.0f);
                GL.Vertex3(output.PositionX, output.PositionY + height * 0.5, 0.0f);
                GL.Vertex3(output.PositionX + width, output.PositionY, 0.0f);
                GL.End();
                GL.PopMatrix();
            }
        }
        public void renderInput()
        {
            if (input != null)
            {
                GL.PushMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Begin(PrimitiveType.LineStrip);
                GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                GL.Vertex3(input.PositionX, input.PositionY, 0.0f);
                GL.Vertex3(input.PositionX, input.PositionY + height, 0.0f);
                GL.Vertex3(input.PositionX + width, input.PositionY + height * 0.5, 0.0f);
                GL.Vertex3(input.PositionX, input.PositionY, 0.0f);
                GL.End();
                GL.PopMatrix();
            }
        }
        public void render()
        {
            glControl.MakeCurrent();
            GL.PushMatrix();
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //subject to change if support zoom in and zoom out
            GL.Ortho(0, glControl.Width, glControl.Height, 0, -1, 1);
            //enable transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            renderInput();
            renderOutput();
            renderBlock();
            renderLine();

            GL.PopMatrix();
            glControl.SwapBuffers();
        }
    }
}