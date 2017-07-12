namespace SeniorDesign.FrontEnd.Components.Blocks
{
    partial class BlockViewerComponent
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BlockTypeName = new System.Windows.Forms.Label();
            this.BlockName = new System.Windows.Forms.TextBox();
            this.AttributeEditorList = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.BlockTypeName);
            this.splitContainer1.Panel1.Controls.Add(this.BlockName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.AttributeEditorList);
            this.splitContainer1.Size = new System.Drawing.Size(480, 340);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.TabIndex = 4;
            // 
            // BlockTypeName
            // 
            this.BlockTypeName.AutoSize = true;
            this.BlockTypeName.Location = new System.Drawing.Point(3, 26);
            this.BlockTypeName.Name = "BlockTypeName";
            this.BlockTypeName.Size = new System.Drawing.Size(101, 13);
            this.BlockTypeName.TabIndex = 1;
            this.BlockTypeName.Text = "- Nothing Selected -";
            // 
            // BlockName
            // 
            this.BlockName.Location = new System.Drawing.Point(3, 3);
            this.BlockName.Name = "BlockName";
            this.BlockName.Size = new System.Drawing.Size(197, 20);
            this.BlockName.TabIndex = 0;
            this.BlockName.TextChanged += new System.EventHandler(this.BlockName_TextChanged);
            // 
            // AttributeEditorList
            // 
            this.AttributeEditorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AttributeEditorList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.AttributeEditorList.Location = new System.Drawing.Point(3, 3);
            this.AttributeEditorList.Name = "AttributeEditorList";
            this.AttributeEditorList.Size = new System.Drawing.Size(474, 161);
            this.AttributeEditorList.TabIndex = 0;
            // 
            // BlockViewerComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "BlockViewerComponent";
            this.Size = new System.Drawing.Size(486, 346);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label BlockTypeName;
        private System.Windows.Forms.TextBox BlockName;
        private System.Windows.Forms.FlowLayoutPanel AttributeEditorList;
    }
}
