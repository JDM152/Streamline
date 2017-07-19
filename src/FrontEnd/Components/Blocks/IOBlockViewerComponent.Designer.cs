namespace SeniorDesign.FrontEnd.Components.Blocks
{
    partial class IOBlockViewerComponent
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
            this.BlockName = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.StreamEditor = new SeniorDesign.FrontEnd.Components.Blocks.IOBlocks.StreamEditorComponent();
            this.PollerEditor = new SeniorDesign.FrontEnd.Components.Blocks.IOBlocks.PollerEditorComponent();
            this.ConverterEditor = new SeniorDesign.FrontEnd.Components.Blocks.IOBlocks.ConverterEditorComponent();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.BlockName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(410, 375);
            this.splitContainer1.SplitterDistance = 73;
            this.splitContainer1.TabIndex = 0;
            // 
            // BlockName
            // 
            this.BlockName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockName.Location = new System.Drawing.Point(133, 3);
            this.BlockName.Name = "BlockName";
            this.BlockName.Size = new System.Drawing.Size(274, 20);
            this.BlockName.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.StreamEditor);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(410, 298);
            this.splitContainer2.SplitterDistance = 129;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.PollerEditor);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ConverterEditor);
            this.splitContainer3.Size = new System.Drawing.Size(277, 298);
            this.splitContainer3.SplitterDistance = 130;
            this.splitContainer3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // StreamEditor
            // 
            this.StreamEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StreamEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StreamEditor.Location = new System.Drawing.Point(0, 0);
            this.StreamEditor.Margin = new System.Windows.Forms.Padding(0);
            this.StreamEditor.Name = "StreamEditor";
            this.StreamEditor.Size = new System.Drawing.Size(129, 298);
            this.StreamEditor.TabIndex = 0;
            // 
            // PollerEditor
            // 
            this.PollerEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PollerEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PollerEditor.Location = new System.Drawing.Point(0, 0);
            this.PollerEditor.Margin = new System.Windows.Forms.Padding(0);
            this.PollerEditor.Name = "PollerEditor";
            this.PollerEditor.Size = new System.Drawing.Size(130, 298);
            this.PollerEditor.TabIndex = 0;
            // 
            // ConverterEditor
            // 
            this.ConverterEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConverterEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ConverterEditor.Location = new System.Drawing.Point(0, 0);
            this.ConverterEditor.Margin = new System.Windows.Forms.Padding(0);
            this.ConverterEditor.Name = "ConverterEditor";
            this.ConverterEditor.Size = new System.Drawing.Size(143, 298);
            this.ConverterEditor.TabIndex = 2;
            // 
            // IOBlockViewerComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "IOBlockViewerComponent";
            this.Size = new System.Drawing.Size(410, 375);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TextBox BlockName;
        private IOBlocks.StreamEditorComponent StreamEditor;
        private IOBlocks.PollerEditorComponent PollerEditor;
        private IOBlocks.ConverterEditorComponent ConverterEditor;
        private System.Windows.Forms.Label label1;
    }
}
