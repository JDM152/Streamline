using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using SeniorDesign.Plugins.Connections;
using SeniorDesign.Plugins.Connections.Converters;
using SeniorDesign.Plugins.Connections.Pollers;
using SeniorDesign.Plugins.Filters;
using System;

namespace SeniorDesign.FrontEnd.Test
{

    /// <summary>
    ///     A test class used to debug workflows
    /// </summary>
    public static class WorkflowTests
    {

        /// <summary>
        ///     A test that creates a complete dummy workflow from start to finish
        ///     using random data inputs.
        /// </summary>
        /// <param name="core">The Streamline Core used in the test</param>
        public static void CreateDummyWorkflowTestA(StreamlineCore core)
        {
            // Create the random input object that is polled every 100ms
            var input = new DataConnection();
            input.MediaConnection = new RandomDataStream();
            input.Converter = new SimpleStreamConverter();
            input.Poller = new PeriodicPoller(core);
            core.AddConnectable(input);

            // Create the rolling average filter object and connect it
            /*
            var rollingAverageFilter = new RollingAverageFilter();
            input.NextConnections.Add(rollingAverageFilter);
            core.AddConnectable(rollingAverageFilter);
            */

            // Create the quantizer filter object and connect it
            var quantizerFilter = new QuantizerFilter();
            input.NextConnections.Add(quantizerFilter);
            quantizerFilter.Maximum = 64000;
            quantizerFilter.StepSize = 100;
            core.AddConnectable(quantizerFilter);

            // Create the console output object and connect it to the filter
            var output = new DataConnection();
            output.IsOutput = true;
            output.MediaConnection = new ConsoleDataStream();
            output.Converter = new SimpleStringConverter();
            output.Poller = null;
            //rollingAverageFilter.NextConnections.Add(output);
            quantizerFilter.NextConnections.Add(output);
            core.AddConnectable(output);

            // At this point, the workflow should work entirely on its own
        }

    }
}
