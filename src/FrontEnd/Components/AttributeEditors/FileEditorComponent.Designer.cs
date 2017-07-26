namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class FileEditorComponent
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AttributeName = new System.Windows.Forms.Label();
            this.UseTip = new System.Windows.Forms.ToolTip(this.components);
            this.InputText = new System.Windows.Forms.TextBox();
            this.FileButton = new System.Windows.Forms.Button();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // AttributeName
            // 
            this.AttributeName.AutoSize = true;
            this.AttributeName.Location = new System.Drawing.Point(3, 5);
            this.AttributeName.Name = "AttributeName";
            this.AttributeName.Size = new System.Drawing.Size(13, 13);
            this.AttributeName.TabIndex = 1;
            this.AttributeName.Text = "?";
            // 
            // UseTip
            // 
            this.UseTip.OwnerDraw = true;
            // 
            // InputText
            // 
            this.InputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputText.Location = new System.Drawing.Point(97, 3);
            this.InputText.Name = "InputText";
            this.InputText.ReadOnly = true;
            this.InputText.Size = new System.Drawing.Size(83, 20);
            this.InputText.TabIndex = 2;
            // 
            // FileButton
            // 
            this.FileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FileButton.Location = new System.Drawing.Point(183, 1);
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(34, 23);
            this.FileButton.TabIndex = 3;
            this.FileButton.Text = "...";
            this.FileButton.UseVisualStyleBackColor = true;
            this.FileButton.Click += new System.EventHandler(this.FileButton_Click);
            // 
            // FileDialog
            // 
            this.FileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.FileDialog_FileOk);
            // 
            // FileEditorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FileButton);
            this.Controls.Add(this.InputText);
            this.Controls.Add(this.AttributeName);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "FileEditorComponent";
            this.Size = new System.Drawing.Size(220, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label AttributeName;
        private System.Windows.Forms.ToolTip UseTip;
        private System.Windows.Forms.TextBox InputText;
        private System.Windows.Forms.Button FileButton;
        private System.Windows.Forms.OpenFileDialog FileDialog;
    }
}
