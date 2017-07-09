namespace SeniorDesign.FrontEnd.Components.AttributeEditors
{
    partial class DoubleEditorComponent
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
            this.InputControl = new System.Windows.Forms.NumericUpDown();
            this.AttributeName = new System.Windows.Forms.Label();
            this.UseTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InputControl)).BeginInit();
            this.SuspendLayout();
            // 
            // InputControl
            // 
            this.InputControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputControl.Location = new System.Drawing.Point(97, 3);
            this.InputControl.Name = "InputControl";
            this.InputControl.Size = new System.Drawing.Size(120, 20);
            this.InputControl.TabIndex = 0;
            this.InputControl.ThousandsSeparator = true;
            this.InputControl.ValueChanged += new System.EventHandler(this.InputControl_ValueChanged);
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
            // IntegerEditorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AttributeName);
            this.Controls.Add(this.InputControl);
            this.MaximumSize = new System.Drawing.Size(0, 25);
            this.MinimumSize = new System.Drawing.Size(220, 25);
            this.Name = "IntegerEditorComponent";
            this.Size = new System.Drawing.Size(220, 25);
            ((System.ComponentModel.ISupportInitialize)(this.InputControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown InputControl;
        private System.Windows.Forms.Label AttributeName;
        private System.Windows.Forms.ToolTip UseTip;
    }
}
