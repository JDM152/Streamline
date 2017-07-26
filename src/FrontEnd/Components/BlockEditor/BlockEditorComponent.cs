using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using SeniorDesign.FrontEnd.Components.BlockEditor.Drawable;
using OpenTK;

namespace SeniorDesign.FrontEnd.Components.BlockEditor
{
    /// <summary>
    ///     An editor for the Block Diagram of the project schematic
    /// </summary>
    public partial class BlockEditorComponent : UserControl
    {
        #region Constants

        /// <summary>
        ///     The width of boxes in pixels
        /// </summary>
        public const int BOXWIDTH = 40;

        /// <summary>
        ///     The height of boxes in pixels
        /// </summary>
        public const int BOXHEIGHT = 40;

        /// <summary>
        ///     The length of the input/output lines
        /// </summary>
        public const int LINELENGTH = 5;

        #endregion

        #region Public Variables

        /// <summary>
        ///     The offset of the viewport
        /// </summary>
        public Vector3 Offset = Vector3.Zero;

        #endregion

        #region Private Variables

        /// <summary>
        ///     The core that is being used for this editor
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     The point where the mouse was clicked down
        /// </summary>
        private Point _downPoint = new Point(0, 0);

        /// <summary>
        ///     The point wherre the mouse was released
        /// </summary>
        private Point _upPoint = new Point(0, 0);

        /// <summary>
        ///     The last object selected
        /// </summary>
        private DrawableObject _lastSelect;

        /// <summary>
        ///     The object currently selected
        /// </summary>
        private DrawableObject _currentSelect;

        /// <summary>
        ///     The current position of the mouse
        /// </summary>
        private Point _mouse;

        /// <summary>
        ///     If the GLControl is loaded, and it is safe to paint
        /// </summary>
        private bool _glLoaded = false;

        /// <summary>
        ///     If the user is currently dragging something, and what that mode is
        /// </summary>
        private DragMode _dragMode = DragMode.None;

        /// <summary>
        ///     The IConnectable that is being moved
        /// </summary>
        private IConnectable _movingBlock;

        /// <summary>
        ///     The line used to show where a connection will be made
        /// </summary>
        private DrawableLine _movingLine;

        /// <summary>
        ///     The list of objects that can be rendered in this scene
        /// </summary>
        private SortedList<int, DrawableObject> _renderables = new SortedList<int, DrawableObject>();

        /// <summary>
        ///     Mappings between IConnectables and the corresponding Drawable Objects.
        ///     The Drawable objects always contain the IConnectable for referse mapping
        /// </summary>
        private Dictionary<IConnectable, DrawableObject> _objectMapping = new Dictionary<IConnectable, DrawableObject>();

        #endregion

        #region Enums

        /// <summary>
        ///     The mode that the dragging function is doing
        /// </summary>
        private enum DragMode
        {
            None,
            MoveBlock,
            ConnectPorts
        }

        #endregion

        /// <summary>
        ///     Creates a new Block Editor Component.
        ///     The Core must be set before this can be fully used
        /// </summary>
        public BlockEditorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets the core to use with this editor
        /// </summary>
        /// <param name="core">The Streamline Core to use</param>
        public void SetCore(StreamlineCore core)
        {
            _core = core;
            _movingLine = new DrawableLine(core, Vector3.Zero, Vector3.Zero);
            _movingLine.Visible = false;
        }

        #region GLControl

        /// <summary>
        ///     Method triggered when the entire editor loads
        /// </summary>
        private void BlockEditorComponent_Load(object sender, EventArgs e)
        {
            // There's a glitch in OpenTK that the GLControl Load never
            // gets called, so this one works instead
            _glLoaded = true;
        }

        /// <summary>
        ///     Method triggered when the GLComponent fully loads
        /// </summary>
        private void BlockControl_Load(object sender, System.EventArgs e)
        {
            // There's a glitch in OpenTK that this never gets called
            //_glLoaded = true;
        }

        /// <summary>
        ///     Method triggered on the Repaint method for the GLComponent
        /// </summary>
        private void BlockControl_Paint(object sender, PaintEventArgs e)
        {
            // Don't paint if not loaded yet
            if (!_glLoaded) return;
        }

        /// <summary>
        ///     Method triggered when a key is pressed inside the GLComponent
        /// </summary>
        private void BlockControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Branch off by key
            switch (e.KeyCode)
            {
                case Keys.Delete: // Delete selected object
                    HandleDelete();
                    break;
            }
        }

        /// <summary>
        ///     Method triggered when the mouse is pressed down inside the GLComponent
        /// </summary>
        private void BlockControl_MouseDown(object sender, MouseEventArgs e)
        {
            // Update the down position of the mouse, and the selected object
            _downPoint = _mouse;
            _lastSelect = _currentSelect;
            _currentSelect = GetObject(_downPoint);

            // Determine which mode to enter by the type of object selected
            if (_currentSelect is DrawablePort)
            {
                // User is attempting to connect points
                _movingBlock = ((DrawablePort) _currentSelect).Owner;
                _movingLine.Start = new Vector3(_mouse.X, _mouse.Y, 0);
                _movingLine.Visible = true;
                _dragMode = DragMode.ConnectPorts;
            }
            else if (_currentSelect is DrawableConnector)
            {
                // User clicked on a line. This cannot be dragged

            }
            else if (_currentSelect is IDrawableIConnectable)
            {
                // User is attempting to drag a block
                _movingBlock = ((IDrawableIConnectable) _currentSelect).GetObject();
                _dragMode = DragMode.MoveBlock;
            }
        }

        /// <summary>
        ///     Method triggered when the mouse is released inside the GLComponent
        /// </summary>
        private void BlockControl_MouseUp(object sender, MouseEventArgs e)
        {
            // Update the up position of the mouse, and handle clicking on anything
            _upPoint = _mouse;

            // Branch off by current drag mode if applicable
            switch (_dragMode)
            {
                case DragMode.MoveBlock: // Update the block position

                    break;

                case DragMode.ConnectPorts: // Update the connected blocks
                    // Connect blocks if applicable
                    _movingLine.Visible = false;
                    var toConnect = GetObject(_mouse);
                    if (toConnect != null && toConnect is DrawablePort)
                        _core.ConnectConnectables(_movingBlock, ((DrawablePort) toConnect).Owner);
                    break;
            }
            _dragMode = DragMode.None;
            
        }

        /// <summary>
        ///     Method triggered when the mouse moves inside the GLControl
        /// </summary>
        private void BlockControl_MouseMove(object sender, MouseEventArgs e)
        {
            // Keep track of the mouse position
            _mouse = ((Control) sender).PointToClient(Control.MousePosition);

            // Branch off by current drag mode if applicable
            switch (_dragMode)
            {
                case DragMode.MoveBlock: // Update the block position
                    _movingBlock.PositionX = _mouse.X;
                    _movingBlock.PositionY = _mouse.Y;
                    Render();
                    break;

                case DragMode.ConnectPorts: // Update the connected blocks
                    // Snap to the port if nearby
                    var toConnect = GetObject(_mouse);
                    if (toConnect != null && toConnect is DrawablePort)
                        _movingLine.End = ((DrawablePort) toConnect).Center;
                    else
                        _movingLine.End = new Vector3(_mouse.X, _mouse.Y, 0);
                    Render();
                    break;
            }
        }

        #endregion

        #region External Triggers

        /*
         *  The methods in this section are used exclusively externally.
         *  When this editor is active, this will always be triggered whenever
         *  a change occurs to the data model, including when this makes changes
         *  through the core.
         *  
         *  DO NOT CALL CORE FUNCTIONS IN ANY OF THESE METHODS
         *  DO NOT CALL DELETE OR ANYTHING THAT CAUSES A CORE FUNCTION TO BE CALLED
         */

        /// <summary>
        ///     Creates a new block for a specific IConnectable Item
        /// </summary>
        /// <param name="temp"></param>
        public void CreateBlock(IConnectable temp)
        {
            DrawableObject newDrawable;

            // Create a new Block depending on the type
            if (temp is DataFilter)
            {
                var dfBlock = new DrawableFilter(_core, temp as DataFilter);
                _renderables.Add(dfBlock.Z, dfBlock);
                _objectMapping.Add(temp, dfBlock);
                newDrawable = dfBlock;
            }
            else if (temp is DataConnection)
            {
                var dcBlock = new DrawableInputOutput(_core, temp as DataConnection);
                _renderables.Add(dcBlock.Z, dcBlock);
                _objectMapping.Add(temp, dcBlock);
                newDrawable = dcBlock;
            }
            else
            {
                throw new NotSupportedException($"The type {temp.GetType()} does not have an associated block symbol");
            }

            // Trigger a re-draw
            Render();

        }

        /// <summary>
        ///     Deletes an existing block from the diagram
        /// </summary>
        /// <param name="temp">The block to delete from the diagram</param>
        public void DeleteBlock(IConnectable temp)
        {
            // Remove from the renderables
            _renderables.RemoveAt(_renderables.IndexOfValue(_objectMapping[temp]));

            // Note that we do not remove the connections, as the core will call
            // remove on each of those seperately before this is called.

            // Trigger a re-draw
            Render();
        }

        /// <summary>
        ///     Creates a line that connects two blocks
        /// </summary>
        /// <param name="A">The first block to connect (from output)</param>
        /// <param name="B">The second block to connect (from input)</param>
        public void ConnectBlocks(IConnectable A, IConnectable B)
        {
            var newConnection = new DrawableConnector(_core, A, B);
            _renderables.Add(newConnection.Z, newConnection);
            (_objectMapping[A] as IDrawableIConnectable)?.MappedObjects.Add(newConnection);

            // Trigger a re-draw
            Render();
        }

        /// <summary>
        ///     Deletes a line that connects two blocks
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="callback"></param>
        public void DisconnectBlocks(IConnectable A, IConnectable B, bool callback = false)
        {
            // Get the connectable object through A's mapped objects
            DrawableConnector selected = null;
            var oMap = (_objectMapping[A] as IDrawableIConnectable).MappedObjects;
            foreach (var obj in oMap)
            {
                selected = obj as DrawableConnector;
                if (selected != null && selected.Child == B)
                    break;
            }

            // Remove the object from all mappings
            _renderables.RemoveAt(_renderables.IndexOfValue(selected));
            oMap.Remove(selected);

            // Trigger a re-draw
            Render();
        }

        /// <summary>
        ///     Updates the position and name of an object
        /// </summary>
        /// <param name="A">The object to update</param>
        public void UpdateBlock(IConnectable A)
        {
            // Trigger a re-draw
            Render();
        }

        #endregion

        #region Internal Triggers

        /// <summary>
        ///     Method triggered when the user attempts to delete the currently selected component
        /// </summary>
        private void HandleDelete()
        {
            if (_currentSelect == null) return;

            // Remove the mapping if an IConnectable
            if (_currentSelect is IDrawableIConnectable)
                _objectMapping.Remove(((IDrawableIConnectable)_currentSelect).GetObject());

            // Delete, and remove from the renderables
            _currentSelect.Delete();
            _renderables.RemoveAt(_renderables.IndexOfValue(_currentSelect));
            _currentSelect = null;

        }

        #endregion
        
        /// <summary>
        ///     Gets the object that exists underneath a specific point
        /// </summary>
        /// <param name="point">The point that is being checked</param>
        /// <returns>The object at that point, or null if none</returns>
        private DrawableObject GetObject(Point point)
        {
            foreach (var item in _renderables)
                if (item.Value.IsPointInside(point.X, point.Y))
                    return item.Value;
            return null;

        }

        #region Rendering

        /// <summary>
        ///     Performs the actual rendering/redrawing on the canvas
        /// </summary>
        public void Render()
        {
            BlockControl.MakeCurrent();
            GL.PushMatrix();
            GL.ClearColor(Color.White);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Viewport(0, 0, BlockControl.Width, BlockControl.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            //subject to change if support zoom in and zoom out
            GL.Ortho(0, BlockControl.Width, BlockControl.Height, 0, -1, 1);
            //enable transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            // Render every control available
            foreach (var obj in _renderables)
            {
                GL.Translate(Offset);
                obj.Value.Draw();
                GL.LoadIdentity();
            }

            GL.PopMatrix();
            BlockControl.SwapBuffers();
        }


        #endregion

    }
}
