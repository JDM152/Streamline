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
        ///     If this is a save or load file dialog
        /// </summary>
        private bool _isSave = false;

        /// <summary>
        ///     Creates a new editor for the given component of an object
        /// </summary>
        public FileEditorComponent(object owner, WrappedAttributeInfo field, UserConfigurableFileAttribute attribute)
        {
            Owner = owner;
            Field = field;
            Attribute = attribute;

            InitializeComponent();
            UpdateComponent();
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
            FileDialogOpen.Filter = Attribute.Filter;
            FileDialogSave.Filter = Attribute.Filter;

            _isSave = Attribute.IsSave;

            InputText.Text = (string) Field.GetValue(Owner);
        }

        /// <summary>
        ///     Method triggered when the user hits the "Change File" button
        /// </summary>
        private void FileButton_Click(object sender, EventArgs e)
        {
            if (_isSave)
                FileDialogSave.ShowDialog();
            else
                FileDialogOpen.ShowDialog();
        }

        /// <summary>
        ///     Method triggered when an open file has been selected
        /// </summary>
        private void FileDialogOpen_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Set real value
            InputText.Text = FileDialogOpen.FileName;
            Field.SetValue(Owner, FileDialogOpen.FileName);
        }

        /// <summary>
        ///     Method triggered when a save file has been selected
        /// </summary>
        private void FileDialogSave_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Set real value
            InputText.Text = FileDialogSave.FileName;
            Field.SetValue(Owner, FileDialogSave.FileName);
        }
    }
}
