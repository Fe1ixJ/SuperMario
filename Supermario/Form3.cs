using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermario
{
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        public void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = Form1.Life.ToString();

        }


        private void btn_testWorld_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            //Form2 frm2 = new Form2();
            // Form3 frm3 = new Form3();
            frm1.Show();
            this.Hide();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
