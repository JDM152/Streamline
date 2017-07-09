using SeniorDesign.Core.Attributes;
using System.Reflection;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    /// <summary>
    ///     A base class for all of the Editor components
    /// </summary>
    public class AttributeEditorComponent<T> : UserControl where T : UserConfigurableAttribute
    {
        /// <summary>
        ///     The field that is being edited
        /// </summary>
        protected FieldInfo Field;

        /// <summary>
        ///     The object that this is editing the component of
        ///     (Not the field itself, but the object that owns the field)
        /// </summary>
        protected object Owner;

        /// <summary>
        ///     The attribute data for the field being edited
        /// </summary>
        protected T Attribute;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // EditorComponent
            // 
            this.Name = "EditorComponent";
            this.ResumeLayout(false);

        }
    }
}
