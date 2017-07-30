using SeniorDesign.Core;
using System;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{

    /// <summary>
    ///     A form used to edit general settings
    /// </summary>
    public partial class SettingsPanel : Form
    {

        /// <summary>
        ///     The core that this edits the settings of
        /// </summary>
        private StreamlineCore _core;

        /// <summary>
        ///     Creates a new Settings Panel
        /// </summary>
        public SettingsPanel(StreamlineCore core)
        {
            _core = core;

            InitializeComponent();

            AttributeListEditor.SetComponent(_core.Settings);
        }

        /// <summary>
        ///     Method triggered when the "Save and Close" button is pressed
        /// </summary>
        private void SaveButton_Click(object sender, System.EventArgs e)
        {
            // Save the data, and close the panel
            try
            {
                _core.SaveCoreSettings("settings.ini");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save settings file : {ex.Message}", "Save Error");
            }
            
        }
    }
}
