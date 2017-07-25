namespace SeniorDesign.FrontEnd.Components
{
    partial class AttributeListComponent
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
            this.AttributeEditorList = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // AttributeEditorList
            // 
            this.AttributeEditorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AttributeEditorList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.AttributeEditorList.Location = new System.Drawing.Point(3, 3);
            this.AttributeEditorList.Name = "AttributeEditorList";
            this.AttributeEditorList.Size = new System.Drawing.Size(200, 200);
            this.AttributeEditorList.TabIndex = 2;
            // 
            // AttributeListComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AttributeEditorList);
            this.Name = "AttributeListComponent";
            this.Size = new System.Drawing.Size(206, 206);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AttributeEditorList;
    }
}
