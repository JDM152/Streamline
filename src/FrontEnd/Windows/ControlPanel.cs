using SeniorDesign.Core;
using System.Drawing;
using System.Windows.Forms;
using SeniorDesign.Core.Connections;
using SeniorDesign.Core.Filters;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     The main window for the Streamline application
    /// </summary>
    public partial class ControlPanel : Form
    {
        private BlockEditor blockEditor;
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
        private void glControl2_Load(object sender, System.EventArgs e)
        {
            blockEditor = new BlockEditor(glControl2, Core);
            timer1.Interval = 16;
            timer1.Start();
        }

        private void glControl2_Paint(object sender, PaintEventArgs e)
        {
            blockEditor.render();
        }

        private void glControl2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                blockEditor.updateUpMouse();
            }
            
            textBox1.Text = "UP";
        }

        private void glControl2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                blockEditor.updateDownMouse();
            }
            else
            {
                blockEditor.reset();
            }
            textBox1.Text = "DOWN";
        }

        private void glControl2_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            Point mouse = control.PointToClient(Control.MousePosition);
            blockEditor.updateMouse(mouse);
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            blockEditor.render();
        }

        private void glControl2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                blockEditor.handleDelete();
                textBox1.Text = "del";
            }
        }

        private void ControlPanel_Load(object sender, System.EventArgs e)
        {

        }

        private void grapherToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //example
            Form temp = new Graph();
            temp.Show();
        }
        int step = 0;
        private void button1_Click(object sender, System.EventArgs e)
        {
            var temp = new DataConnection();
            var wala = new DataConnection();
            if (step == 0)
            {
                temp.PositionX = 100;
                temp.PositionY = 100;
                temp.IsOutput = false;
                temp.Id = 0;
                blockEditor.CreatBlock(temp as IConnectable);
                step++;
            }
            else if (step == 1)
            {
                wala.PositionX = 300;
                wala.PositionY = 100;//why it will reset to 0?
                wala.IsOutput = true;
                wala.Id = 1;
                blockEditor.CreatBlock(wala as IConnectable);
                step++;
            }
            else if (step == 2)
            {
                temp.PositionX = 150;
                temp.PositionY = 100;
                blockEditor.UpdateBlockPosition(temp);
                step++;
            }
            else if (step == 3)
            {
                wala.PositionX = 350;
                wala.PositionY = 100;
                wala.IsOutput = true;
                blockEditor.UpdateBlockPosition(wala);
                step++;
            }
            else if (step == 4)
            {

                temp.PositionX = 150;
                temp.PositionY = 100;
                temp.IsOutput = false;
                temp.Id = 0;
                blockEditor.DeleteBlock(temp);
                step++;
            }
            else if(step == 5)
            {
                temp.PositionX = 150;
                temp.PositionY = 100;
                temp.IsOutput = false;
                temp.Id = 0;
                blockEditor.CreatBlock(temp);
            }
        }

        private void ControlPanel_Load_1(object sender, System.EventArgs e)
        {

        }
    }
}
