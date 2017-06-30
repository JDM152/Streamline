namespace SeniorDesign.FrontEnd.Windows
{
    partial class PluginPanel
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
            this.PluginsList = new System.Windows.Forms.ListBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.RemovePlugin = new System.Windows.Forms.Button();
            this.PluginContentsList = new System.Windows.Forms.TreeView();
            this.PluginSelectDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // PluginsList
            // 
            this.PluginsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PluginsList.FormattingEnabled = true;
            this.PluginsList.Location = new System.Drawing.Point(12, 12);
            this.PluginsList.Name = "PluginsList";
            this.PluginsList.Size = new System.Drawing.Size(138, 290);
            this.PluginsList.TabIndex = 2;
            this.PluginsList.SelectedIndexChanged += new System.EventHandler(this.PluginsList_SelectedIndexChanged);
            // 
            // LoadButton
            // 
            this.LoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadButton.Location = new System.Drawing.Point(12, 308);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(138, 23);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load Plugin";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // RemovePlugin
            // 
            this.RemovePlugin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemovePlugin.Location = new System.Drawing.Point(12, 337);
            this.RemovePlugin.Name = "RemovePlugin";
            this.RemovePlugin.Size = new System.Drawing.Size(138, 23);
            this.RemovePlugin.TabIndex = 4;
            this.RemovePlugin.Text = "Remove Plugin";
            this.RemovePlugin.UseVisualStyleBackColor = true;
            this.RemovePlugin.Click += new System.EventHandler(this.RemovePlugin_Click);
            // 
            // PluginContentsList
            // 
            this.PluginContentsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PluginContentsList.Location = new System.Drawing.Point(192, 12);
            this.PluginContentsList.Name = "PluginContentsList";
            this.PluginContentsList.Size = new System.Drawing.Size(195, 348);
            this.PluginContentsList.TabIndex = 5;
            // 
            // PluginSelectDialog
            // 
            this.PluginSelectDialog.FileName = "plugin";
            this.PluginSelectDialog.Filter = "Plugin|*.strplg|DLL|*.dll|All Files|*.*";
            this.PluginSelectDialog.Title = "Select Plugin";
            // 
            // PluginPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 372);
            this.Controls.Add(this.PluginContentsList);
            this.Controls.Add(this.RemovePlugin);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.PluginsList);
            this.MinimumSize = new System.Drawing.Size(640, 410);
            this.Name = "PluginPanel";
            this.Text = "Plugins";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox PluginsList;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button RemovePlugin;
        private System.Windows.Forms.TreeView PluginContentsList;
        private System.Windows.Forms.OpenFileDialog PluginSelectDialog;
    }
}