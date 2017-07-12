namespace SeniorDesign.FrontEnd.Windows
{
    partial class BlockCreatorPanel
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
            this.saveBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBlockAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BlockTypeBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.blockViewerComponent1 = new SeniorDesign.FrontEnd.Components.Blocks.BlockViewerComponent();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.CancelCreationButton = new System.Windows.Forms.Button();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(514, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveBlockToolStripMenuItem,
            this.saveBlockAsToolStripMenuItem,
            this.loadBlockToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveBlockToolStripMenuItem
            // 
            this.saveBlockToolStripMenuItem.Name = "saveBlockToolStripMenuItem";
            this.saveBlockToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveBlockToolStripMenuItem.Text = "Save Block...";
            this.saveBlockToolStripMenuItem.Click += new System.EventHandler(this.saveBlockToolStripMenuItem_Click);
            // 
            // saveBlockAsToolStripMenuItem
            // 
            this.saveBlockAsToolStripMenuItem.Name = "saveBlockAsToolStripMenuItem";
            this.saveBlockAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveBlockAsToolStripMenuItem.Text = "Save Block As...";
            this.saveBlockAsToolStripMenuItem.Click += new System.EventHandler(this.saveBlockAsToolStripMenuItem_Click);
            // 
            // loadBlockToolStripMenuItem
            // 
            this.loadBlockToolStripMenuItem.Name = "loadBlockToolStripMenuItem";
            this.loadBlockToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadBlockToolStripMenuItem.Text = "Load Block...";
            this.loadBlockToolStripMenuItem.Click += new System.EventHandler(this.loadBlockToolStripMenuItem_Click);
            // 
            // BlockTypeBox
            // 
            this.BlockTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlockTypeBox.FormattingEnabled = true;
            this.BlockTypeBox.Location = new System.Drawing.Point(134, 27);
            this.BlockTypeBox.Name = "BlockTypeBox";
            this.BlockTypeBox.Size = new System.Drawing.Size(368, 21);
            this.BlockTypeBox.TabIndex = 1;
            this.BlockTypeBox.SelectedIndexChanged += new System.EventHandler(this.BlockTypeBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Block Type";
            // 
            // blockViewerComponent1
            // 
            this.blockViewerComponent1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blockViewerComponent1.Location = new System.Drawing.Point(12, 54);
            this.blockViewerComponent1.Name = "blockViewerComponent1";
            this.blockViewerComponent1.Size = new System.Drawing.Size(490, 344);
            this.blockViewerComponent1.TabIndex = 3;
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBlockButton.Location = new System.Drawing.Point(318, 404);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(184, 23);
            this.AddBlockButton.TabIndex = 4;
            this.AddBlockButton.Text = "Close and Add Block";
            this.AddBlockButton.UseVisualStyleBackColor = true;
            // 
            // CancelCreationButton
            // 
            this.CancelCreationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelCreationButton.Location = new System.Drawing.Point(128, 404);
            this.CancelCreationButton.Name = "CancelCreationButton";
            this.CancelCreationButton.Size = new System.Drawing.Size(184, 23);
            this.CancelCreationButton.TabIndex = 5;
            this.CancelCreationButton.Text = "Cancel";
            this.CancelCreationButton.UseVisualStyleBackColor = true;
            // 
            // BlockCreatorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 432);
            this.Controls.Add(this.CancelCreationButton);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.blockViewerComponent1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BlockTypeBox);
            this.Controls.Add(this.MenuStrip);
            this.MainMenuStrip = this.MenuStrip;
            this.MinimumSize = new System.Drawing.Size(530, 470);
            this.Name = "BlockCreatorPanel";
            this.Text = "Create Block";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBlockAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadBlockToolStripMenuItem;
        private System.Windows.Forms.ComboBox BlockTypeBox;
        private System.Windows.Forms.Label label1;
        private Components.Blocks.BlockViewerComponent blockViewerComponent1;
        private System.Windows.Forms.Button AddBlockButton;
        private System.Windows.Forms.Button CancelCreationButton;
    }
}