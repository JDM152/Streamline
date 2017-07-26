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
        private Point downPoint = new Point(0,0);
        private Point upPoint = new Point(0, 0);
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
            currentSelect = new IDstruct(ObjectType.Output, null, null);
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
            currentSelect = getObject(downPoint);
        }
        public void handleMouseClick()
        {
            if (
                (currentSelect.objectType == ObjectType.Input ||
                currentSelect.objectType == ObjectType.Output ||
                currentSelect.objectType == ObjectType.Block) &&
                getObject(upPoint).objectType == ObjectType.Null)
            {
                if (upPoint != downPoint)
                {
                    drag(currentSelect.A, upPoint);
                }
            }
            else if (currentSelect.objectType != ObjectType.Null && lastSelect.objectType != ObjectType.Null && currentSelect.A.Id != lastSelect.A.Id)
            {
                if (lastSelect.objectType == ObjectType.InputPort && currentSelect.objectType == ObjectType.OutputPort)
                {
                    ConnectBlocks(currentSelect.A, lastSelect.A, true);
                }
                else if (lastSelect.objectType == ObjectType.OutputPort && currentSelect.objectType == ObjectType.InputPort)
                {
                    ConnectBlocks(lastSelect.A, currentSelect.A, true);
                }
            }
        }
        public void handleDelete()
        {
            ObjectType temp = (currentSelect.objectType);
            IConnectable temp2 = currentSelect.A;
            if(currentSelect.objectType == ObjectType.Line)
            {
                DisconnectBlocks(currentSelect.A, currentSelect.B, true);
                currentSelect = new IDstruct(ObjectType.Null, null, null);
            }
            else if(currentSelect.objectType == ObjectType.Block)
            {
                DeleteBlock(currentSelect.A, true);
                currentSelect = new IDstruct(ObjectType.Null, null, null);
            }
            else if(currentSelect.objectType == ObjectType.Input)
            {
                input = null;
                //call back
                currentSelect = new IDstruct(ObjectType.Null, null, null);
            }
            else if(currentSelect.objectType == ObjectType.Output)
            {
                output = null;
                //call back
                currentSelect = new IDstruct(ObjectType.Null, null, null);
            }
            
        }
        public void drag(IConnectable A, Point newPosition)
        {
            if(input != null && input.Id == A.Id)
            {
                input.PositionX = newPosition.X;
                input.PositionY = newPosition.Y;
                //to do core    
            }
            else if (output != null && output.Id == A.Id)
            {
                output.PositionX = newPosition.X;
                output.PositionY = newPosition.Y;
                //to do core  
            }
            else
            {
                if(filterList != null)
                {
                    for (int i = 0; i < filterList.Count; i++)
                    {
                        if (filterList[i].Id == A.Id)
                        {
                            filterList[i].PositionX = newPosition.X;
                            filterList[i].PositionY = newPosition.Y;
                            //to do core
                            return;
                        }
                    }
                }
            }
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
        public void UpdateBlockPosition(IConnectable A, bool callback = false)
        {
            if(input != null && input.Id == A.Id)
            {
                input.PositionX = A.PositionX;
                input.PositionY = A.PositionY;
                if(callback)
                {
                    //to do core    
                }
            }
            else if (output != null && output.Id == A.Id)
            {
                output.PositionX = A.PositionX;
                output.PositionY = A.PositionY;
                if (callback)
                {
                    //to do core    
                }
            }
            else
            {
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        filterList[i].PositionX = A.PositionX;
                        filterList[i].PositionY = A.PositionY;
                        if (callback)
                        {
                            //to do core    
                        }
                    }
                }
            }
        }
        public void ConnectBlocks(IConnectable A, IConnectable B, bool callback = false)
        {
            //input output
            if (input != null && output != null && A.Id == input.Id && B.Id == output.Id )
            {
                input.NextConnections.Add(output);
                if (callback)
                {
                    //to do core
                }
            }
            //input block
            else if (input != null && A.Id == input.Id && (output == null || output.Id != B.Id) )
            {
                for(int i = 0; i < filterList.Count; i++)
                {
                    if(filterList[i].Id  == B.Id)
                    {
                        input.NextConnections.Add(filterList[i]);
                        if(callback)
                        {
                            //to do core
                        }
                        return;
                    }
                }
            }
            //block ouput
            else if ( ( input == null || input.Id != A.Id) && ( output != null && output.Id == B.Id) )
            {
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        filterList[i].NextConnections.Add(output);
                        if (callback)
                        {
                            //to do core
                        }
                        return;
                    }
                }
            }
            //block block
            else
            {
                int left = -1;
                int right = -1;
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        left = i;
                    }
                    else if(filterList[i].Id == B.Id)
                    {
                        right = i;
                    }
                }
                filterList[left].NextConnections.Add(filterList[right]);
                if (callback)
                {
                    //to do core
                }
            }
        }
        public void DisconnectBlocks(IConnectable A, IConnectable B, bool callback = false)
        {
            //input remove
            if(input != null && input.Id == A.Id)
            {
                input.NextConnections.Remove(B);
                if (callback)
                {
                    //core call back
                }
            }
            //block ouput
            else if(output != null && output.Id == B.Id)
            {
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        filterList[i].NextConnections.Remove(output);
                        if (callback)
                        {
                            //core call back
                        }
                        return;
                    }
                }
            }
            //block
            else
            {
                int left = -1;
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        left = i;
                        return;
                    }
                }
                filterList[left].NextConnections.Remove(B);
                if (callback)
                {
                    //core call back
                }
            }
        }
        public void DeleteBlock(IConnectable temp, bool callback = false)
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
            if(callback)
            {
                //CORE CALL BACK
            }
        }
        public void SetName(IConnectable A, String name)
        {
            if(input != null && input.Id == A.Id)
            {
                input.Name = name;
            }
            else if (output != null && output.Id == A.Id)
            {
                output.Name = name;
            }
            else
            {
                for (int i = 0; i < filterList.Count; i++)
                {
                    if (filterList[i].Id == A.Id)
                    {
                        filterList[i].Name = name;
                        return;
                    }
                }
            }
        }
        public IDstruct getObject(Point mouse)
        {
            //line
            foreach (DataFilter tempFilter in filterList)
            {
                foreach (IConnectable next in tempFilter.NextConnections)
                {
                    
                    bool contact = false;
                    //actual port position
                    Point tempFilterPoint = new Point(tempFilter.PositionX + width, tempFilter.PositionY + height / 2);
                    Point nextPortPoint = new Point(next.PositionX, next.PositionY + height / 2);
                    if(tempFilterPoint.X < nextPortPoint.X)
                    {
                       contact = isOnLine(tempFilterPoint, nextPortPoint, mouse);
                    }
                    else
                    {
                        contact = isOnLine(nextPortPoint, tempFilterPoint, mouse);
                    }
                    if(contact)
                    {
                        return new IDstruct(ObjectType.Line, tempFilter, next);
                    }
                }
            }
           if(input != null && input.NextConnections != null)
            {
                foreach (IConnectable next in input.NextConnections)
                {
                    bool contact = false;
                    Point inputPortPoint = new Point(input.PositionX + width, input.PositionY + height / 2);
                    Point nextPortPoint = new Point(next.PositionX, next.PositionY + height / 2);
                    if (inputPortPoint.X < nextPortPoint.X)
                    {
                        contact = isOnLine(inputPortPoint, nextPortPoint, mouse);
                    }
                    else
                    {
                        contact = isOnLine(nextPortPoint, inputPortPoint, mouse);
                    }
                    if (contact)
                    {
                        return new IDstruct(ObjectType.Line, input, next);
                    }
                }
            }
            //port
            for(int i = 0; i < filterList.Count; i++)
            {
                ObjectType returnType = isInsidePort(filterList[i], mouse);
                if(returnType == ObjectType.InputPort)
                {
                    return new IDstruct(ObjectType.InputPort, filterList[i], null);
                }
                else if(returnType == ObjectType.Output)
                {
                    return new IDstruct(ObjectType.OutputPort, filterList[i], null);
                }
            }
            if (input != null)
            {
                ObjectType returnType = isInsidePort(input, mouse);
                //input must outputing
                if (returnType == ObjectType.OutputPort)
                {
                    return new IDstruct(ObjectType.OutputPort, input, null);
                }
            }
            if (output != null)
            {
                ObjectType returnType = isInsidePort(output, mouse);
                if (returnType == ObjectType.InputPort)
                {
                    return new IDstruct(ObjectType.InputPort, output, null);
                }
            }
            //block
            foreach (DataFilter tempFilter in filterList)
            {
                bool contact = isInsideBlock(new Point(tempFilter.PositionX, tempFilter.PositionY), mouse);
                if(contact)
                {
                    return new IDstruct(ObjectType.Block, tempFilter, null);
                }
            }
            if(input != null)
            {
                if (isInsideBlock(new Point(input.PositionX, input.PositionY), mouse))
                {
                    return new IDstruct(ObjectType.Input, input, null);
                }
            }
            if(output != null)
            {
                if (isInsideBlock(new Point(output.PositionX, output.PositionY), mouse))
                {
                    return new IDstruct(ObjectType.Output, output, null);
                }
            }
            return new IDstruct(ObjectType.Null, null, null);

        }
        public ObjectType isInsidePort(IConnectable A, Point mouse)
        {
            float allowedDif = 10.0f;
            Point inputPort = new Point(A.PositionX, A.PositionY + height / 2);
            Point outputPort = new Point(A.PositionX + width, A.PositionY + height / 2);

            float difX = Math.Abs(inputPort.X - mouse.X);
            float difY = Math.Abs(inputPort.Y - mouse.Y);
            if(difX <= allowedDif && difY <= allowedDif)
            {
                return ObjectType.InputPort;
            }
            difX = Math.Abs(outputPort.X - mouse.X);
            difY = Math.Abs(outputPort.Y - mouse.Y);
            if (difX <= allowedDif && difY <= allowedDif)
            {
                return ObjectType.OutputPort;
            }
            return ObjectType.Null;
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
            if (mouse.X < start.X || mouse.X > end.X)
            {
                return false;
            }
            float AllowDif = 4.0f;
            float slope = (float)(end.Y - start.Y)/ (end.X - start.X);
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
                        GL.Vertex3(next.PositionX, next.PositionY + 0.5 * height, 0.0f);
                        GL.End();
                        GL.PopMatrix();
                    }
                }
               
            }
            if (input != null)
            {
                foreach (IConnectable next in input.NextConnections)
                {
                    GL.PushMatrix();
                    GL.MatrixMode(MatrixMode.Modelview);
                    GL.LoadIdentity();
                    GL.Begin(PrimitiveType.Lines);
                    GL.Color4(0.0f, 0.0f, 0.0f, 1.0f);
                    GL.Vertex3(input.PositionX + width, input.PositionY + 0.5 * height, 0.0f);
                    GL.Vertex3(next.PositionX, next.PositionY + 0.5 * height, 0.0f);
                    GL.End();
                    GL.PopMatrix();
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