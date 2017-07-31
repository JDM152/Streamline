using SeniorDesign.Core;
using SeniorDesign.Core.Connections;
using System;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the Enable/Disable Button
    /// </summary>
    public partial class EnableButtonComponent : UserControl
    {

        /// <summary>
        ///     The object that this can enable/disable
        /// </summary>
        private DataConnection _owner;

        /// <summary>
        ///     Creates a new editor that can trigger Recompiles for an IDataConnectionComponent
        /// </summary>
        public EnableButtonComponent(DataConnection owner)
        {
            _owner = owner;
            InitializeComponent();

            EnableButton.Text = _owner.Enabled ? "Disable" : "Enable";
            _owner.OnEnabledChanged += OnEnabledChanged;
            
        }

        /// <summary>
        ///     Method triggered whenever the requirement of recompiling changes
        /// </summary>
        private void OnEnabledChanged(object sender, bool e)
        {
            if (EnableButton.InvokeRequired)
                EnableButton.Invoke(new MethodInvoker(() => { OnEnabledChanged(sender, e); }));
            EnableButton.Text = e ? "Disable" : "Enable";
        }


        /// <summary>
        ///     Method triggered when the user hits the "Compile" button
        /// </summary>
        private void RecompileButton_Click(object sender, EventArgs e)
        {
            if (_owner.Enabled)
                _owner.Disable();
            else
                _owner.Enable();
        }
    }
}
