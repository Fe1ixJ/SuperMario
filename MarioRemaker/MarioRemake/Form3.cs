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
    public partial class WorldSelector : Form
    {
        public WorldSelector()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestWorld frm1 = new TestWorld();
            //Form2 frm2 = new Form2();
            // Form3 frm3 = new Form3();
            frm1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WorldSelector_Load(object sender, EventArgs e)
        {
            label1.Text = TestWorld.Life.ToString();
        }
    }
}
