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
            try
            {
                core.LoadCoreSettings("settings.ini");
            }
            catch { }
            var pluginErrors = new List<string>();

            core.LoadPluginsFromAssembly(typeof(RollingAverageFilter).Assembly, pluginErrors);
            core.LoadPluginsFromAssembly(typeof(Program).Assembly, pluginErrors);

            foreach (var line in pluginErrors)
                Console.WriteLine("Plugin Error : " + line);

            // Start the program GUI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ControlPanel(core));
        }
    }
}
