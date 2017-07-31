using SeniorDesign.Core.Attributes.Specialized;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     A button that can be placed that triggers arbitrary parameterless functions
    /// </summary>
    public partial class FunctionButtonComponent : UserControl
    {

        /// <summary>
        ///     The object that this triggers the function
        /// </summary>
        private object _owner;

        /// <summary>
        ///     The method that is triggered when the button is pressed
        /// </summary>
        private MethodInfo _method;

        /// <summary>
        ///     The attribute that contains information about the button
        /// </summary>
        private FunctionButtonAttribute _attribute;

        /// <summary>
        ///     Creates a new editor that can trigger Recompiles for an IDataConnectionComponent
        /// </summary>
        public FunctionButtonComponent(object owner, MethodInfo method, FunctionButtonAttribute attribute)
        {
            _owner = owner;
            _method = method;
            _attribute = attribute;
            InitializeComponent();

            FunctionButton.Enabled = true;
            FunctionButton.Text = _attribute.Name;
            UseTip.SetToolTip(FunctionButton, _attribute.Description);
            
        }


        /// <summary>
        ///     Method triggered when the user hits the "Compile" button
        /// </summary>
        private void RecompileButton_Click(object sender, EventArgs e)
        {
            // Trigger the method
            _method.Invoke(_owner, null);
        }
    }
}
