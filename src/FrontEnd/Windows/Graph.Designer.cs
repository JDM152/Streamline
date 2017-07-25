namespace SeniorDesign.FrontEnd.Windows
{
    partial class Graph
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
            this.glControl1 = new OpenTK.GLControl();
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.updateViewport = new System.Windows.Forms.Button();
            this.updateData = new System.Windows.Forms.Button();
            this.render = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(486, 395);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(558, 36);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(85, 30);
            this.start.TabIndex = 1;
            this.start.Text = "start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(558, 72);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(85, 30);
            this.stop.TabIndex = 2;
            this.stop.Text = "stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // updateViewport
            // 
            this.updateViewport.Location = new System.Drawing.Point(535, 108);
            this.updateViewport.Name = "updateViewport";
            this.updateViewport.Size = new System.Drawing.Size(140, 30);
            this.updateViewport.TabIndex = 3;
            this.updateViewport.Text = "updateViewPort";
            this.updateViewport.UseVisualStyleBackColor = true;
            this.updateViewport.Click += new System.EventHandler(this.updateViewport_Click);
            // 
            // updateData
            // 
            this.updateData.Location = new System.Drawing.Point(535, 144);
            this.updateData.Name = "updateData";
            this.updateData.Size = new System.Drawing.Size(140, 30);
            this.updateData.TabIndex = 4;
            this.updateData.Text = "updateData";
            this.updateData.UseVisualStyleBackColor = true;
            this.updateData.Click += new System.EventHandler(this.updateData_Click);
            // 
            // render
            // 
            this.render.Location = new System.Drawing.Point(568, 180);
            this.render.Name = "render";
            this.render.Size = new System.Drawing.Size(75, 28);
            this.render.TabIndex = 5;
            this.render.Text = "render";
            this.render.UseVisualStyleBackColor = true;
            this.render.Click += new System.EventHandler(this.render_Click);
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 395);
            this.Controls.Add(this.render);
            this.Controls.Add(this.updateData);
            this.Controls.Add(this.updateViewport);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.start);
            this.Controls.Add(this.glControl1);
            this.Name = "Graph";
            this.Text = "Graph";
            this.Load += new System.EventHandler(this.Graph_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.Button updateViewport;
        private System.Windows.Forms.Button updateData;
        private System.Windows.Forms.Button render;
    }
}