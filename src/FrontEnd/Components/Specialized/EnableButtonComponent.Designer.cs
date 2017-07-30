namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class EnableButtonComponent
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
            this.UseTip = new System.Windows.Forms.ToolTip(this.components);
            this.EnableButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UseTip
            // 
            this.UseTip.OwnerDraw = true;
            // 
            // EnableButton
            // 
            this.EnableButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EnableButton.Location = new System.Drawing.Point(3, 2);
            this.EnableButton.Name = "EnableButton";
            this.EnableButton.Size = new System.Drawing.Size(214, 23);
            this.EnableButton.TabIndex = 0;
            this.EnableButton.Text = "Disable";
            this.UseTip.SetToolTip(this.EnableButton, "Enables or Disables this block");
            this.EnableButton.UseVisualStyleBackColor = true;
            this.EnableButton.Click += new System.EventHandler(this.RecompileButton_Click);
            // 
            // EnableButtonComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EnableButton);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "EnableButtonComponent";
            this.Size = new System.Drawing.Size(220, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip UseTip;
        private System.Windows.Forms.Button EnableButton;
    }
}
