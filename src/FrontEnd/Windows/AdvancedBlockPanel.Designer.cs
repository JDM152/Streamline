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
            this.BlockList = new System.Windows.Forms.ListBox();
            this.AddBlockButton = new System.Windows.Forms.Button();
            this.AddIOButton = new System.Windows.Forms.Button();
            this.DeleteBlockButton = new System.Windows.Forms.Button();
            this.ConnectionEditor = new SeniorDesign.FrontEnd.Components.Blocks.ConnectionViewerComponent();
            this.BlockViewComponent = new SeniorDesign.FrontEnd.Components.Blocks.BlockViewerComponent();
            this.SuspendLayout();
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
            // ConnectionEditor
            // 
            this.ConnectionEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectionEditor.Location = new System.Drawing.Point(489, 27);
            this.ConnectionEditor.MinimumSize = new System.Drawing.Size(383, 300);
            this.ConnectionEditor.Name = "ConnectionEditor";
            this.ConnectionEditor.Size = new System.Drawing.Size(383, 351);
            this.ConnectionEditor.TabIndex = 6;
            // 
            // BlockViewComponent
            // 
            this.BlockViewComponent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockViewComponent.Location = new System.Drawing.Point(141, 27);
            this.BlockViewComponent.Name = "BlockViewComponent";
            this.BlockViewComponent.Size = new System.Drawing.Size(342, 351);
            this.BlockViewComponent.TabIndex = 2;
            // 
            // AdvancedBlockPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 382);
            this.Controls.Add(this.ConnectionEditor);
            this.Controls.Add(this.DeleteBlockButton);
            this.Controls.Add(this.AddIOButton);
            this.Controls.Add(this.AddBlockButton);
            this.Controls.Add(this.BlockViewComponent);
            this.Controls.Add(this.BlockList);
            this.MinimumSize = new System.Drawing.Size(900, 420);
            this.Name = "AdvancedBlockPanel";
            this.Text = "Block Viewer";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox BlockList;
        private Components.Blocks.BlockViewerComponent BlockViewComponent;
        private System.Windows.Forms.Button AddBlockButton;
        private System.Windows.Forms.Button AddIOButton;
        private System.Windows.Forms.Button DeleteBlockButton;
        private Components.Blocks.ConnectionViewerComponent ConnectionEditor;
    }
}