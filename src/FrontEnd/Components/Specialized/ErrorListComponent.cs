using SeniorDesign.Core.Connections;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

namespace SeniorDesign.FrontEnd.Components.Specialized
{
    /// <summary>
    ///     A list of errors when dealing with components with attributes
    /// </summary>
    public partial class ErrorListComponent : UserControl
    {
        /// <summary>
        ///     The component whose errors are being viewed
        /// </summary>
        private IDataConnectionComponent _component;

        /// <summary>
        ///     Creates a new error list component
        /// </summary>
        public ErrorListComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets the component that is being viewed
        /// </summary>
        /// <param name="comp">The new component to view</param>
        public void SetComponent(IDataConnectionComponent comp)
        {
            // Unregister previous hooks as needed
            if (_component != null)
                _component.OnErrorStringsChanged -= ErrorStringChanged;

            _component = comp;
            if (_component != null)
                _component.OnErrorStringsChanged += ErrorStringChanged;
            ErrorStringChanged(null, null);
        }

        /// <summary>
        ///     Method triggered whenever the error messages on an object change
        /// </summary>
        private void ErrorStringChanged(object sender, EventArgs e)
        {
            // Reset the visible messages
            ErrorList.Items.Clear();

            if (_component == null) return;
            var safeMessages = new List<string>(_component.ErrorStrings);
            foreach (var msg in safeMessages)
                ErrorList.Items.Add(msg);
        }
    }
}
