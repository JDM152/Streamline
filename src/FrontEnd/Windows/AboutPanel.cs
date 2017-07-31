using System;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{

    /// <summary>
    ///     A small "About" panel
    /// </summary>
    public partial class AboutPanel : Form
    {
        /// <summary>
        ///     Creates the About Panel
        /// </summary>
        public AboutPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Method triggered when the user clicks the "Close" button
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
