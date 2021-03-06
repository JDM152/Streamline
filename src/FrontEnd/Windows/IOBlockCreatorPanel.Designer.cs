﻿namespace SeniorDesign.FrontEnd.Windows
{
    partial class IOBlockCreatorPanel
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
            this.CancelCreationButton = new System.Windows.Forms.Button();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.MediaTypeLabel = new System.Windows.Forms.Label();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBlockAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadBlockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MediaTypeBox = new System.Windows.Forms.ComboBox();
            this.PollerTypeBox = new System.Windows.Forms.ComboBox();
            this.PollerTypeLabel = new System.Windows.Forms.Label();
            this.ConverterTypeBox = new System.Windows.Forms.ComboBox();
            this.ConverterTypeLabel = new System.Windows.Forms.Label();
            this.InputOutputBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.IOBlockViewer = new SeniorDesign.FrontEnd.Components.Blocks.IOBlockViewerComponent();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelCreationButton
            // 
            this.CancelCreationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelCreationButton.Location = new System.Drawing.Point(458, 414);
            this.CancelCreationButton.Name = "CancelCreationButton";
            this.CancelCreationButton.Size = new System.Drawing.Size(184, 23);
            this.CancelCreationButton.TabIndex = 11;
            this.CancelCreationButton.Text = "Cancel";
            this.CancelCreationButton.UseVisualStyleBackColor = true;
            this.CancelCreationButton.Click += new System.EventHandler(this.CancelCreationButton_Click);
            // 
            // AddBlockButton
            // 
            this.AddBlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBlockButton.Location = new System.Drawing.Point(648, 414);
            this.AddBlockButton.Name = "AddBlockButton";
            this.AddBlockButton.Size = new System.Drawing.Size(184, 23);
            this.AddBlockButton.TabIndex = 10;
            this.AddBlockButton.Text = "Close and Add Block";
            this.AddBlockButton.UseVisualStyleBackColor = true;
            this.AddBlockButton.Click += new System.EventHandler(this.AddBlockButton_Click);
            // 
            // MediaTypeLabel
            // 
            this.MediaTypeLabel.AutoSize = true;
            this.MediaTypeLabel.Location = new System.Drawing.Point(12, 57);
            this.MediaTypeLabel.Name = "MediaTypeLabel";
            this.MediaTypeLabel.Size = new System.Drawing.Size(63, 13);
            this.MediaTypeLabel.TabIndex = 9;
            this.MediaTypeLabel.Text = "Media Type";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(844, 24);
            this.MenuStrip.TabIndex = 8;
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
            // 
            // saveBlockAsToolStripMenuItem
            // 
            this.saveBlockAsToolStripMenuItem.Name = "saveBlockAsToolStripMenuItem";
            this.saveBlockAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.saveBlockAsToolStripMenuItem.Text = "Save Block As...";
            // 
            // loadBlockToolStripMenuItem
            // 
            this.loadBlockToolStripMenuItem.Name = "loadBlockToolStripMenuItem";
            this.loadBlockToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.loadBlockToolStripMenuItem.Text = "Load Block...";
            // 
            // MediaTypeBox
            // 
            this.MediaTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MediaTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MediaTypeBox.FormattingEnabled = true;
            this.MediaTypeBox.Location = new System.Drawing.Point(134, 54);
            this.MediaTypeBox.Name = "MediaTypeBox";
            this.MediaTypeBox.Size = new System.Drawing.Size(698, 21);
            this.MediaTypeBox.TabIndex = 12;
            this.MediaTypeBox.SelectedIndexChanged += new System.EventHandler(this.MediaTypeBox_SelectedIndexChanged);
            // 
            // PollerTypeBox
            // 
            this.PollerTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PollerTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PollerTypeBox.FormattingEnabled = true;
            this.PollerTypeBox.Location = new System.Drawing.Point(134, 81);
            this.PollerTypeBox.Name = "PollerTypeBox";
            this.PollerTypeBox.Size = new System.Drawing.Size(698, 21);
            this.PollerTypeBox.TabIndex = 13;
            this.PollerTypeBox.SelectedIndexChanged += new System.EventHandler(this.PollerTypeBox_SelectedIndexChanged);
            // 
            // PollerTypeLabel
            // 
            this.PollerTypeLabel.AutoSize = true;
            this.PollerTypeLabel.Location = new System.Drawing.Point(12, 84);
            this.PollerTypeLabel.Name = "PollerTypeLabel";
            this.PollerTypeLabel.Size = new System.Drawing.Size(60, 13);
            this.PollerTypeLabel.TabIndex = 14;
            this.PollerTypeLabel.Text = "Poller Type";
            // 
            // ConverterTypeBox
            // 
            this.ConverterTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConverterTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConverterTypeBox.FormattingEnabled = true;
            this.ConverterTypeBox.Location = new System.Drawing.Point(134, 108);
            this.ConverterTypeBox.Name = "ConverterTypeBox";
            this.ConverterTypeBox.Size = new System.Drawing.Size(698, 21);
            this.ConverterTypeBox.TabIndex = 15;
            this.ConverterTypeBox.SelectedIndexChanged += new System.EventHandler(this.ConverterTypeBox_SelectedIndexChanged);
            // 
            // ConverterTypeLabel
            // 
            this.ConverterTypeLabel.AutoSize = true;
            this.ConverterTypeLabel.Location = new System.Drawing.Point(12, 111);
            this.ConverterTypeLabel.Name = "ConverterTypeLabel";
            this.ConverterTypeLabel.Size = new System.Drawing.Size(80, 13);
            this.ConverterTypeLabel.TabIndex = 16;
            this.ConverterTypeLabel.Text = "Converter Type";
            // 
            // InputOutputBox
            // 
            this.InputOutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputOutputBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputOutputBox.FormattingEnabled = true;
            this.InputOutputBox.Items.AddRange(new object[] {
            "Input",
            "Output"});
            this.InputOutputBox.Location = new System.Drawing.Point(134, 27);
            this.InputOutputBox.Name = "InputOutputBox";
            this.InputOutputBox.Size = new System.Drawing.Size(698, 21);
            this.InputOutputBox.TabIndex = 17;
            this.InputOutputBox.SelectedIndexChanged += new System.EventHandler(this.InputOutputBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Input / Output";
            // 
            // IOBlockViewer
            // 
            this.IOBlockViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IOBlockViewer.Location = new System.Drawing.Point(15, 138);
            this.IOBlockViewer.Margin = new System.Windows.Forms.Padding(0);
            this.IOBlockViewer.Name = "IOBlockViewer";
            this.IOBlockViewer.Size = new System.Drawing.Size(817, 273);
            this.IOBlockViewer.TabIndex = 7;
            // 
            // IOBlockCreatorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 442);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputOutputBox);
            this.Controls.Add(this.ConverterTypeLabel);
            this.Controls.Add(this.ConverterTypeBox);
            this.Controls.Add(this.PollerTypeLabel);
            this.Controls.Add(this.PollerTypeBox);
            this.Controls.Add(this.MediaTypeBox);
            this.Controls.Add(this.CancelCreationButton);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.MediaTypeLabel);
            this.Controls.Add(this.MenuStrip);
            this.Controls.Add(this.IOBlockViewer);
            this.MinimumSize = new System.Drawing.Size(860, 480);
            this.Name = "IOBlockCreatorPanel";
            this.Text = "Create IO Block";
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.Blocks.IOBlockViewerComponent IOBlockViewer;
        private System.Windows.Forms.Button CancelCreationButton;
        private System.Windows.Forms.Button AddBlockButton;
        private System.Windows.Forms.Label MediaTypeLabel;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBlockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBlockAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadBlockToolStripMenuItem;
        private System.Windows.Forms.ComboBox MediaTypeBox;
        private System.Windows.Forms.ComboBox PollerTypeBox;
        private System.Windows.Forms.Label PollerTypeLabel;
        private System.Windows.Forms.ComboBox ConverterTypeBox;
        private System.Windows.Forms.Label ConverterTypeLabel;
        private System.Windows.Forms.ComboBox InputOutputBox;
        private System.Windows.Forms.Label label4;
    }
}