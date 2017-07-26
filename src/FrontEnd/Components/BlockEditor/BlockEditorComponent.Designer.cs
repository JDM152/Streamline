namespace SeniorDesign.FrontEnd.Components.BlockEditor
{
    partial class BlockEditorComponent
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
            this.BlockControl = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // BlockControl
            // 
            this.BlockControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockControl.BackColor = System.Drawing.Color.Black;
            this.BlockControl.Location = new System.Drawing.Point(0, 0);
            this.BlockControl.Margin = new System.Windows.Forms.Padding(0);
            this.BlockControl.Name = "BlockControl";
            this.BlockControl.Size = new System.Drawing.Size(150, 150);
            this.BlockControl.TabIndex = 0;
            this.BlockControl.VSync = false;
            this.BlockControl.Load += new System.EventHandler(this.BlockControl_Load);
            this.BlockControl.Paint += new System.Windows.Forms.PaintEventHandler(this.BlockControl_Paint);
            this.BlockControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BlockControl_KeyDown);
            this.BlockControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BlockControl_MouseDown);
            this.BlockControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BlockControl_MouseMove);
            this.BlockControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BlockControl_MouseUp);
            // 
            // BlockEditorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BlockControl);
            this.Name = "BlockEditorComponent";
            this.Load += new System.EventHandler(this.BlockEditorComponent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl BlockControl;
    }
}
