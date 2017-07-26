namespace SeniorDesign.FrontEnd.Components.Specialized
{
    partial class ErrorListComponent
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
            this.ErrorList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ErrorList
            // 
            this.ErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorList.BackColor = System.Drawing.SystemColors.Control;
            this.ErrorList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorList.ForeColor = System.Drawing.Color.Red;
            this.ErrorList.FormattingEnabled = true;
            this.ErrorList.Location = new System.Drawing.Point(0, 0);
            this.ErrorList.Name = "ErrorList";
            this.ErrorList.Size = new System.Drawing.Size(150, 143);
            this.ErrorList.TabIndex = 0;
            // 
            // ErrorListComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ErrorList);
            this.Name = "ErrorListComponent";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ErrorList;
    }
}
