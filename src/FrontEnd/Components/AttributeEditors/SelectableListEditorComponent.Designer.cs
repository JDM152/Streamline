namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class SelectableListEditorComponent
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
            this.InputControl = new System.Windows.Forms.ComboBox();
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
            // InputControl
            // 
            this.InputControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputControl.FormattingEnabled = true;
            this.InputControl.Location = new System.Drawing.Point(97, 3);
            this.InputControl.Name = "InputControl";
            this.InputControl.Size = new System.Drawing.Size(120, 21);
            this.InputControl.TabIndex = 2;
            this.InputControl.SelectedIndexChanged += new System.EventHandler(this.InputControl_SelectedIndexChanged);
            // 
            // SelectableListEditorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InputControl);
            this.Controls.Add(this.AttributeName);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "SelectableListEditorComponent";
            this.Size = new System.Drawing.Size(220, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label AttributeName;
        private System.Windows.Forms.ToolTip UseTip;
        private System.Windows.Forms.ComboBox InputControl;
    }
}
