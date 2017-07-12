namespace SeniorDesign.FrontEnd.Windows
{
    partial class AdvancedBlockPanel
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
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlockList = new System.Windows.Forms.ListBox();
            this.BlockViewComponent = new SeniorDesign.FrontEnd.Components.Blocks.BlockViewerComponent();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.AddIOButton = new System.Windows.Forms.Button();
            this.DeleteBlockButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(639, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // BlockList
            // 
            this.BlockList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BlockList.FormattingEnabled = true;
            this.BlockList.Location = new System.Drawing.Point(12, 27);
            this.BlockList.Name = "BlockList";
            this.BlockList.Size = new System.Drawing.Size(120, 264);
            this.BlockList.TabIndex = 1;
            this.BlockList.SelectedIndexChanged += new System.EventHandler(this.BlockList_SelectedIndexChanged);
            // 
            // BlockViewComponent
            // 
            this.BlockViewComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockViewComponent.Location = new System.Drawing.Point(141, 27);
            this.BlockViewComponent.Name = "BlockViewComponent";
            this.BlockViewComponent.Size = new System.Drawing.Size(486, 346);
            this.BlockViewComponent.TabIndex = 2;
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddBlockButton.Location = new System.Drawing.Point(12, 297);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(120, 23);
            this.AddBlockButton.TabIndex = 3;
            this.AddBlockButton.Text = "Add Block";
            this.AddBlockButton.UseVisualStyleBackColor = true;
            this.AddBlockButton.Click += new System.EventHandler(this.AddBlockButton_Click);
            // 
            // AddIOButton
            // 
            this.AddIOButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddIOButton.Location = new System.Drawing.Point(12, 326);
            this.AddIOButton.Name = "AddIOButton";
            this.AddIOButton.Size = new System.Drawing.Size(120, 23);
            this.AddIOButton.TabIndex = 4;
            this.AddIOButton.Text = "Add Input/Output";
            this.AddIOButton.UseVisualStyleBackColor = true;
            this.AddIOButton.Click += new System.EventHandler(this.AddIOButton_Click);
            // 
            // DeleteBlockButton
            // 
            this.DeleteBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteBlockButton.Location = new System.Drawing.Point(12, 355);
            this.DeleteBlockButton.Name = "DeleteBlockButton";
            this.DeleteBlockButton.Size = new System.Drawing.Size(120, 23);
            this.DeleteBlockButton.TabIndex = 5;
            this.DeleteBlockButton.Text = "Delete";
            this.DeleteBlockButton.UseVisualStyleBackColor = true;
            this.DeleteBlockButton.Click += new System.EventHandler(this.DeleteBlockButton_Click);
            // 
            // AdvancedBlockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 382);
            this.Controls.Add(this.DeleteBlockButton);
            this.Controls.Add(this.AddIOButton);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.BlockViewComponent);
            this.Controls.Add(this.BlockList);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(640, 410);
            this.Name = "AdvancedBlockPanel";
            this.Text = "Block Viewer";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ListBox BlockList;
        private Components.Blocks.BlockViewerComponent BlockViewComponent;
        private System.Windows.Forms.Button AddBlockButton;
        private System.Windows.Forms.Button AddIOButton;
        private System.Windows.Forms.Button DeleteBlockButton;
    }
}