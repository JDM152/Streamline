using SeniorDesign.Core.Attributes;
using SeniorDesign.Core.Connections;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input Boolean components
    /// </summary>
    public partial class CompileButtonComponent : UserControl
    {

        /// <summary>
        ///     The object that this triggers the recompile on
        /// </summary>
        private IDataConnectionComponent _owner;

        /// <summary>
        ///     Creates a new editor that can trigger Recompiles for an IDataConnectionComponent
        /// </summary>
        public CompileButtonComponent(IDataConnectionComponent owner)
        {
            _owner = owner;
            InitializeComponent();

            RecompileButton.Enabled = _owner.NeedsCompile;
            RecompileButton.Text = _owner.NeedsCompile ? "Compile" : "Component Compiled";
            _owner.OnNeedsCompileChangeEvent += OnNeedsCompileChange;
            
        }

        /// <summary>
        ///     Method triggered whenever the requirement of recompiling changes
        /// </summary>
        private void OnNeedsCompileChange(object sender, bool e)
        {
            RecompileButton.Enabled = e;
            RecompileButton.Text = e ? "Compile" : "Component Compiled";
        }


        /// <summary>
        ///     Method triggered when the user hits the "Compile" button
        /// </summary>
        private void RecompileButton_Click(object sender, EventArgs e)
        {
            _owner.Compile();
        }
    }
}
