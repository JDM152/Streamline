using SeniorDesign.Core;
using SeniorDesign.FrontEnd.Test;
using System;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A panel used to show specialized debugging tools
    /// </summary>
    public partial class TestPanel : Form
    {
        /// <summary>
        ///     The core that is being utilized
        /// </summary>
        private StreamlineCore Core;

        /// <summary>
        ///     Creates a new panel for debugging
        /// </summary>
        public TestPanel(StreamlineCore core)
        {
            Core = core;
            InitializeComponent();
        }

        /// <summary>
        ///     Method triggered when the user hits the "Test A" Button
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // Run the test setup
            WorkflowTests.CreateDummyWorkflowTestA(Core);
        }
    }
}
