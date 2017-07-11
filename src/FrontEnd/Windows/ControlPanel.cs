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
    }
}
