namespace SeniorDesign.FrontEnd.Components.Blocks.IOBlocks
{
    partial class PollerEditorComponent
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
            this.AttributeList = new SeniorDesign.FrontEnd.Components.AttributeListComponent();
            this.BlockTypeName = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ErrorList = new SeniorDesign.FrontEnd.Components.Specialized.ErrorListComponent();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AttributeList
            // 
            this.AttributeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AttributeList.Location = new System.Drawing.Point(3, 3);
            this.AttributeList.Margin = new System.Windows.Forms.Padding(0);
            this.AttributeList.Name = "AttributeList";
            this.AttributeList.Size = new System.Drawing.Size(228, 236);
            this.AttributeList.TabIndex = 0;
            // 
            // BlockTypeName
            // 
            this.BlockTypeName.AutoSize = true;
            this.BlockTypeName.Location = new System.Drawing.Point(3, 0);
            this.BlockTypeName.Name = "BlockTypeName";
            this.BlockTypeName.Size = new System.Drawing.Size(101, 13);
            this.BlockTypeName.TabIndex = 1;
            this.BlockTypeName.Text = "- Nothing Selected -";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ErrorList);
            this.splitContainer1.Panel1.Controls.Add(this.BlockTypeName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AttributeList);
            this.splitContainer1.Size = new System.Drawing.Size(234, 334);
            this.splitContainer1.SplitterDistance = 88;
            this.splitContainer1.TabIndex = 6;
            // 
            // ErrorList
            // 
            this.ErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorList.Location = new System.Drawing.Point(0, 16);
            this.ErrorList.Name = "ErrorList";
            this.ErrorList.Size = new System.Drawing.Size(234, 69);
            this.ErrorList.TabIndex = 3;
            // 
            // PollerEditorComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PollerEditorComponent";
            this.Size = new System.Drawing.Size(240, 340);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AttributeListComponent AttributeList;
        private System.Windows.Forms.Label BlockTypeName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Specialized.ErrorListComponent ErrorList;
    }
}
