using SeniorDesign.Core;
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
        ///     The core of the program (the actual Streamline)
        /// </summary>
        protected readonly StreamlineCore Core;

        /// <summary>
        ///     Creates a new central control panel using a given core
        /// </summary>
        public ControlPanel(StreamlineCore core)
        {
            Core = core;
            InitializeComponent();

            // Don't show debug if not a dev
            if (!CoreSettings.DebugMode)
                debugToolStripMenuItem.Dispose();

            // Set up the block editor
            BlockSchematic.SetCore(Core);
            BlockSchematic.OnBlockSelected += (e, o) => BlockViewer.SetViewingComponent(o);
            
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
            var panel = new BlockCreatorPanel(Core);
            panel.ShowDialog();
        }

        /// <summary>
        ///     Method triggered when the user presses the "Add Input/Output" button
        /// </summary>
        private void AddIOButton_Click(object sender, System.EventArgs e)
        {
            // Show the Add IO Panel
            var panel = new IOBlockCreatorPanel(Core);
            panel.ShowDialog();
        }
    }
}
