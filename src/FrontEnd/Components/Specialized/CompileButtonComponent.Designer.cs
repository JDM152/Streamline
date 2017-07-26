namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class CompileButtonComponent
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
            this.RecompileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UseTip
            // 
            this.UseTip.OwnerDraw = true;
            // 
            // RecompileButton
            // 
            this.RecompileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RecompileButton.Location = new System.Drawing.Point(3, 2);
            this.RecompileButton.Name = "RecompileButton";
            this.RecompileButton.Size = new System.Drawing.Size(214, 23);
            this.RecompileButton.TabIndex = 0;
            this.RecompileButton.Text = "Compile";
            this.UseTip.SetToolTip(this.RecompileButton, "Compiles this component to be used");
            this.RecompileButton.UseVisualStyleBackColor = true;
            this.RecompileButton.Click += new System.EventHandler(this.RecompileButton_Click);
            // 
            // CompileButtonComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RecompileButton);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "CompileButtonComponent";
            this.Size = new System.Drawing.Size(220, 25);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip UseTip;
        private System.Windows.Forms.Button RecompileButton;
    }
}
