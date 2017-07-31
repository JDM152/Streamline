namespace SeniorDesign.FrontEnd.Windows
{
    partial class GrapherPanel
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Grapher = new SeniorDesign.FrontEnd.Components.Grapher.GrapherComponent();
            this.SuspendLayout();
            // 
            // Grapher
            // 
            this.Grapher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Grapher.Location = new System.Drawing.Point(12, 12);
            this.Grapher.Name = "Grapher";
            this.Grapher.Size = new System.Drawing.Size(260, 238);
            this.Grapher.TabIndex = 0;
            // 
            // GrapherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Grapher);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "GrapherPanel";
            this.Text = "Grapher";
            this.ResumeLayout(false);

        }

        #endregion

        internal Components.Grapher.GrapherComponent Grapher;
    }
}