using SeniorDesign.Core;
using SeniorDesign.FrontEnd.Windows;
using SeniorDesign.Plugins.Filters;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd
{
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create the Streamline Core object, and load the default plugins
            var core = new StreamlineCore();
            var pluginErrors = new List<string>();
            core.LoadPluginsFromAssembly(typeof(RollingAverageFilter).Assembly, pluginErrors);

            // Start the program GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ControlPanel(core));
        }
    }
}
