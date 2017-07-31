using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     The main window for the Streamline application
    /// </summary>
    public partial class ControlPanel : Form
    {
        /// <summary>
        ///     The filename that is currently being edited
        /// </summary>
        protected string Filename = null;

        /// <summary>
        ///     An object used to reserve rendering time for OpenGL
        /// </summary>
        public static object RenderLock = new object();

        /// <summary>
        ///     The core of the program (the actual Streamline)
        /// </summary>
        protected readonly StreamlineCore Core;

        /// <summary>
        ///     The BlockCreatorPanel currently being used
        /// </summary>
        private BlockCreatorPanel _blockCreatorPanel = null;

        /// <summary>
        ///     The IOBlockCreatorPanel currently being used
        /// </summary>
        private IOBlockCreatorPanel _ioBlockCreatorPanel = null;

        /// <summary>
        ///     Creates a new central control panel using a given core
        /// </summary>
        public ControlPanel(StreamlineCore core)
        {
            Core = core;
            InitializeComponent();

            // Don't show debug if not a dev
            if (!Core.Settings.DebugMode)
                debugToolStripMenuItem.Dispose();

            // Hide the components until something is selected
            BlockViewer.Hide();
            IOBlockViewer.Hide();

            // Set up the block editor
            BlockSchematic.SetCore(Core);
            BlockSchematic.OnBlockSelected += DetermineViewingComponent;
            
        }

        /// <summary>
        ///     Determines which block viewer should be shown for the user
        /// </summary>
        private void DetermineViewingComponent(object owner, IConnectable c)
        {
            var dc = c as DataConnection;
            if (dc != null)
            {
                // Treat as Data Connection
                BlockViewer.SetViewingComponent(null);
                IOBlockViewer.SetViewingComponent(dc);
                IOBlockViewer.UpdateAllComponents();
                BlockViewer.Hide();
                IOBlockViewer.Show();
            }
            else
            {
                // Treat as generic block
                BlockViewer.SetViewingComponent(c);
                IOBlockViewer.SetViewingComponent(null);
                BlockViewer.Show();
                IOBlockViewer.Hide();
            }
            
        }

        /// <summary>
        ///     Method triggered when the "Debug" option is clicked in Debug
        /// </summary>
        private void debugToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Open the debug control panel
            new TestPanel(Core).Show();
        }

        /// <summary>
        ///     Method triggered when the "Plugins" option is clicked in View->Plugins
        /// </summary>
        private void pluginsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            new PluginPanel(Core).ShowDialog();
        }

        /// <summary>
        ///     Method triggered when the "Advanced Block Editor" option is clicked in View->Advanced Block Editor
        /// </summary>
        private void advancedBlockEditorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            new AdvancedBlockPanel(Core).ShowDialog();
        }

        /// <summary>
        ///     Method triggered when the user clicks Help->About Streamline
        /// </summary>
        private void aboutToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            new AboutPanel().ShowDialog();
        }

        #region File Menu

        /// <summary>
        ///     Method triggered when the "New" option is clicked in File->New
        /// </summary>
        private void newToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Stop and clear everything
            this.Text = "Streamline - New Schematic";
            Core.ClearProjectSchematic();
        }

        /// <summary>
        ///     Method triggered when the "Save" option is clicked in File->Save
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            // Show "Save As" if no current project
            if (Filename == null)
                SaveSchematicDialog.ShowDialog();
            else
                Core.SaveProjectSchematic(Filename);
        }

        /// <summary>
        ///     Method triggered when the "Save As" option is clicked in File->Save As
        /// </summary>
        private void saveAsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SaveSchematicDialog.ShowDialog();
        }

        /// <summary>
        ///     Method triggered when the "Open" option is clicked iin the File->Open
        /// </summary>
        private void openToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            OpenSchematicDialog.ShowDialog();
        }

        /// <summary>
        ///     Method triggered when the save dialog file name is selected
        /// </summary>
        private void SaveSchematicDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Filename = SaveSchematicDialog.FileName;
            this.Text = "Streamline - " + Filename;
            Core.SaveProjectSchematic(Filename);
        }

        /// <summary>
        ///     Method triggere when the open dialog file is selected
        /// </summary>
        private void OpenSchematicDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Filename = OpenSchematicDialog.FileName;
            this.Text = "Streamline - " + Filename;
            Core.LoadProjectSchematic(Filename);
        }

        #endregion

        /// <summary>
        ///     Method triggered when the user presses the "Add Filter" button
        /// </summary>
        private void AddFilterButton_Click(object sender, System.EventArgs e)
        {
            // Show the Add Block Panel
            if (_blockCreatorPanel == null || _blockCreatorPanel.IsDisposed)
                _blockCreatorPanel = new BlockCreatorPanel(Core);
            _blockCreatorPanel.Show();
            _blockCreatorPanel.BringToFront();
        }

        /// <summary>
        ///     Method triggered when the user presses the "Add Input/Output" button
        /// </summary>
        private void AddIOButton_Click(object sender, System.EventArgs e)
        {
            // Show the Add IO Panel
            if (_ioBlockCreatorPanel == null || _ioBlockCreatorPanel.IsDisposed)
                _ioBlockCreatorPanel = new IOBlockCreatorPanel(Core);
            _ioBlockCreatorPanel.Show();
            _ioBlockCreatorPanel.BringToFront();
        }

        private void IOBlockViewer_Load(object sender, System.EventArgs e)
        {

        }

        /// <summary>
        ///     Method triggered whenever View->Settings is selected
        /// </summary>
        private void settingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            new SettingsPanel(Core).ShowDialog();
        }
    }
}
