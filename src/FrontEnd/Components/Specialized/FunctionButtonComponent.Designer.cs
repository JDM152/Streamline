namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class FunctionButtonComponent
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
            this.FunctionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UseTip
            // 
            this.UseTip.OwnerDraw = true;
            // 
            // FunctionButton
            // 
            this.FunctionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FunctionButton.Location = new System.Drawing.Point(3, 2);
            this.FunctionButton.Name = "FunctionButton";
            this.FunctionButton.Size = new System.Drawing.Size(214, 23);
            this.FunctionButton.TabIndex = 0;
            this.FunctionButton.Text = "Execute";
            this.FunctionButton.UseVisualStyleBackColor = true;
            this.FunctionButton.Click += new System.EventHandler(this.RecompileButton_Click);
            // 
            // FunctionButtonComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FunctionButton);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "FunctionButtonComponent";
            this.Size = new System.Drawing.Size(220, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip UseTip;
        private System.Windows.Forms.Button FunctionButton;
    }
}
