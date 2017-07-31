using SeniorDesign.Core;
using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections;
using SeniorDesign.FrontEnd.Components.AttributeEditors;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.Blocks
{
    /// <summary>
    ///     A component used for viewing the data of a given block
    /// </summary>
    public partial class BlockViewerComponent : UserControl
    {

        #region Display Configuration Parameters


        #endregion

        /// <summary>
        ///     The component currently being viewed
        /// </summary>
        private IConnectable _selected = null;

        /// <summary>
        ///     If the updates to the editor fields should be ignored for now
        /// </summary>
        private bool _ignoreUpdates = false;

        /// <summary>
        ///     Creates a new Block Viewer component
        /// </summary>
        public BlockViewerComponent()
        {
            InitializeComponent();

            _ignoreUpdates = true;
            ListBlockContent();
            _ignoreUpdates = false;
        }

        /// <summary>
        ///     Changes the block that is being edited
        /// </summary>
        /// <param name="component">The IConnectable block editing is switching to</param>
        public void SetViewingComponent(IConnectable component)
        {
            _selected = component;
            ListBlockContent();
        }

        /// <summary>
        ///     Updates the information about the currently selected block
        /// </summary>
        public void ListBlockContent()
        {
            // Only show things if the selected block is available
            if (_selected == null)
            {
                splitContainer1.Hide();
                AttributeList.SetComponent(null);
            }
            else
            {
                splitContainer1.Show();

                // Update the data on the top page
                BlockName.Text = _selected.Name;
                var realType = _selected.InternalName;
                var dc = _selected as DataConnection;
                if (dc != null)
                {
                    if (dc.MediaConnection != null)
                        realType += $"\nMedia : {dc.MediaConnection.InternalName}";
                    if (dc.Poller != null)
                        realType += $"\nPoller : {dc.Poller.InternalName}";
                    if (dc.Converter != null)
                        realType += $"\nConverter : {dc.Converter.InternalName}";
                }
                BlockDataText.Text = $"Type : {realType}\nAvailable Inputs : {(_selected.InputCount == -1 ? "Unlimited" : _selected.InputCount.ToString())}\nAvailable Outputs : {_selected.OutputCount}"; ;

                AttributeList.SetComponent(_selected);
            }
        }

        /// <summary>
        ///     Method triggered when the name of the block changes
        /// </summary>
        void BlockName_TextChanged(object sender, EventArgs e)
        {
            if (_selected == null || _ignoreUpdates)
                return;

            _selected.Name = BlockName.Text;
        }
    }
}
