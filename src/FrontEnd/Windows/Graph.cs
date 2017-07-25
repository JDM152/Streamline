using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeniorDesign.FrontEnd.Windows
{
    public partial class Graph : Form
    {
        private Grapher grapher;
        public Graph()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            grapher = new Grapher(glControl1);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            grapher.render();
        }
        private void updateData_Click(object sender, EventArgs e)
        {
            //fake data
            float[][] testerV = new float[3][];
            float[][] testerT = new float[3][];
            testerV[0] = new float[4];
            testerT[0] = new float[4];
            testerV[1] = new float[4];
            testerT[1] = new float[4];
            testerV[2] = new float[4];
            testerT[2] = new float[4];
            for (int i = 0; i < 4; i++)
            {
                testerV[0][i] = -3.0f + 5.0f * i;
                testerT[0][i] = 5.0f * i;
                testerV[1][i] = 2.0f - 1.0f * i;
                testerT[1][i] = 3.0f * i;
                testerV[2][i] = 1.0f + 1.0f * i * i;
                testerT[2][i] = 5.0f * i;
            }
            Color[] testerC = new Color[3];
            testerC[0] = Color.DarkGoldenrod;
            testerC[1] = Color.GreenYellow;
            testerC[2] = Color.Pink;
            bool[] d = { true, false, false };
            grapher.updateData(testerV, testerT, testerC, d);
        }

        private void updateViewport_Click(object sender, EventArgs e)
        {
            //default is 10
            grapher.updateXspan(100);
            grapher.updateYspan(100);
        }

        private void start_Click(object sender, EventArgs e)
        {
            //if not start, no data will be display
            grapher.start();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            //if stop, no data will be display
            grapher.stop();
        }

        private void render_Click(object sender, EventArgs e)
        {
            //do not render if no data!!!!
            grapher.render();
        }

        private void Graph_Load(object sender, EventArgs e)
        {

        }
    }
}
