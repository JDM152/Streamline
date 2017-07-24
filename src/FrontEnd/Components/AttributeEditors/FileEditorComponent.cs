using SeniorDesign.Core.Attributes;
using System;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{

    /// <summary>
    ///     An editor for the User Input File Selector components
    /// </summary>
    public partial class FileEditorComponent : UserControl, IAttributeEditorComponent<UserConfigurableFileAttribute>
    {

        #region IAttributeEditorComponent

        /// <summary>
        ///     The field that is being edited
        /// </summary>
        public WrappedAttributeInfo Field { get; protected set; }

        /// <summary>
        ///     The object that this is editing the component of
        ///     (Not the field itself, but the object that owns the field)
        /// </summary>
        public object Owner { get; protected set; }

        /// <summary>
        ///     The attribute data for the field being edited
        /// </summary>
        public UserConfigurableFileAttribute Attribute { get; protected set; }

        #endregion

        /// <summary>
        ///     If the value should be allowed to update or not
        /// </summary>
        private bool _suppressUpdate = false;

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public FileEditorComponent(object owner, WrappedAttributeInfo field, UserConfigurableFileAttribute attribute)
        {
            _suppressUpdate = true;
            Owner = owner;
            Field = field;
            Attribute = attribute;

            InitializeComponent();
            UpdateComponent();
            _suppressUpdate = false;
        }

        /// <summary>
        ///     Updates this attribute editor's properties
        /// </summary>
        public void UpdateComponent()
        {
            // Set the help text
            AttributeName.Text = Attribute.Name;
            UseTip.SetToolTip(InputText, Attribute.Description);

            // Set the filters
            FileDialog.Filter = Attribute.Filter;

            InputText.Text = (string) Field.GetValue(Owner);
        }

        /// <summary>
        ///     Method triggered when the user hits the "Change File" button
        /// </summary>
        private void FileButton_Click(object sender, EventArgs e)
        {
            FileDialog.ShowDialog();
        }

        /// <summary>
        ///     Method triggered when a file has been selected
        /// </summary>
        private void FileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Set real value
            InputText.Text = FileDialog.FileName;
            Field.SetValue(Owner, FileDialog.FileName);
        }
    }
}
