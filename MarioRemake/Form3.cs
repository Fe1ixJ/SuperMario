using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using MarioRemake.Properties;

namespace MarioRemake
{
    public partial class WorldSelector : Form
    {
        public WorldSelector()
        {
            InitializeComponent();
        }
        //Här väljer man bana och det leder till att spelet startar samt också skickar det en vidare till nästa formulär
        SoundPlayer musik = new SoundPlayer(Resources.Mario4min);
        private void button1_Click(object sender, EventArgs e)
        {
            musik.Play();
            TestWorld frm1 = new TestWorld();
            //Form2 frm2 = new Form2();
            // Form3 frm3 = new Form3();
            frm1.Show();
            frm1.FormBorderStyle = FormBorderStyle.None;

            frm1.Bounds = Screen.PrimaryScreen.Bounds;
            this.Hide();
        }
        //Meny listan och allt som visas där.
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
            MessageBox.Show("Made By The One And Only \r\n Felix Fe1ixJ Johansson \r\n @github.com/Fe1ixJ");
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If Needed Update The Game At Github.com/Fe1ixJ");
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
            MessageBox.Show("Here is a basic tutorial of all the keybinds in the game \r\n Start game - Enter \r\n Forward - RightArrow \r\n Backward - LeftArrow \r\n Jump - Uparrow \r\n Exit Game - P");
        }

        private void bugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("If you find a bug please summit a report via mail or discord Felix.Johansson041@gmail.com or Fe1ixJ#3217. The Report should contain how to recreate the bug, what happened and what version.");
        }
    }
}
