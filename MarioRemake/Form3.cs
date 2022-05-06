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
            frm1.FormBorderStyle = FormBorderStyle.None;

            frm1.Bounds = Screen.PrimaryScreen.Bounds;
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

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Fe1ixJ, github.com/Fe1ixJ");
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update The Game At Github.com/Fe1ixJ");
        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you have any problems with the game you can reach me at Felix.Johansson041@gmail.com");
        }

        private void goalOfTheGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Goal of the game is to survive though out the entire game and beat the final boss");
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OPen form with all keybinds 
        }
    }
}
