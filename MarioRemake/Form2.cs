using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioRemake
{
    public partial class LoadingScreen : Form
    {
        public LoadingScreen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Skickar bara en vidare till nästa formulär
            TestWorld frm1 = new TestWorld();
            WorldSelector frm2 = new WorldSelector();
            WorldSelector frm3 = new WorldSelector();
            frm3.Show();
            this.Hide();
        }

        private void LoadingScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
