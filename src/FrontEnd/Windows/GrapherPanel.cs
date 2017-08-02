using System.Windows.Forms;
using SeniorDesign.Core;

namespace SeniorDesign.FrontEnd.Windows
{
    /// <summary>
    ///     A window specifically made to show the Grapher
    /// </summary>
    public partial class GrapherPanel : Form
    {

        /// <summary>
        ///     Creates a new Grapher Panel
        /// </summary>
        public GrapherPanel()
        {
            InitializeComponent();
        }
        private Grapher grapher;

        private void GrapherPanel_Load(object sender, System.EventArgs e)
        {
            grapher = new Grapher(glControl1); 
        }
        

        private void glControl1_Load(object sender, System.EventArgs e)
        {
        }
        public void draw(DataPacket p)
        {
            if(grapher == null)
            {
                return;
            }
            grapher.Draw(p);
        }
    }
}
