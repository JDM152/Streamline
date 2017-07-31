namespace SeniorDesign.FrontEnd.Components.Grapher
{
    partial class GrapherComponent
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
            this.GlComponent = new OpenTK.GLControl();
            this.SuspendLayout();
            // 
            // GlComponent
            // 
            this.GlComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GlComponent.BackColor = System.Drawing.Color.Black;
            this.GlComponent.Location = new System.Drawing.Point(0, 0);
            this.GlComponent.Name = "GlComponent";
            this.GlComponent.Size = new System.Drawing.Size(150, 150);
            this.GlComponent.TabIndex = 0;
            this.GlComponent.VSync = false;
            this.GlComponent.Resize += new System.EventHandler(this.GlComponent_Resize);
            // 
            // GrapherComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GlComponent);
            this.Name = "GrapherComponent";
            this.Load += new System.EventHandler(this.GrapherComponent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl GlComponent;
    }
}
