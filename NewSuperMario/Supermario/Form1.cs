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
{// när man går av blocket så får man inte flying state

    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();

        }

        int gravity = 16;
        int Rörelse = 0;

        bool MarioFlying = true;
        bool turbo = false;
        double accelration = 1;

        int temporär;
        bool EndGame;
        public static int Life = 5;




        public void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();


        }
        private void timer2_Tick(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (turbo== true) //&& !MarioFlying) När jag har fixat röra block osv
            {
                accelration = 1.75;
            }
            else
            {
                accelration = 1;
            }
            temporär = (int)Math.Round(Rörelse * accelration);


            Mario.Top += gravity;
            Mario.Left += temporär;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Marken")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {

                        MarioFlying = false;
                        Mario.Top=x.Top - Mario.Height;

                        if (Mario.Top == x.Bottom)
                        {
                            //  Mario.Top
                            gravity = 4;
                            MarioFlying = true;

                        }
                        if (Mario.Bottom == x.Top)
                        {
                            MarioFlying = false;
                            Mario.Top=x.Top - Mario.Height;
                        }
                        if (Mario.Right == x.Left)
                        {
                            Rörelse = 0;
                        }
                        if (Mario.Left == x.Right)
                        {
                            Rörelse = 0;
                        }
                        //Mario undersidan
                        //mario vänster
                        //mario höger
                        //mario top vad som händer(Det under är för top)
                    }
                    else
                    {

                        // gravity = 4;

                    }
                }
                else
                    gravity = 8;

            }
            foreach (Control z in this.Controls)
            {
                if (z is PictureBox && (string)z.Tag == "Flagstolpsknop")
                {
                    if (Mario.Bounds.IntersectsWith(z.Bounds))
                    {
                        if (Life<98)
                            Life++;
                        Endgame(true);
                        MessageBox.Show("You WIN + 1UP");


                    }

                }


            }
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && (string)y.Tag == "Flagstolpe")
                {
                    if (Mario.Bounds.IntersectsWith(y.Bounds))
                    {
                        Endgame(true);
                        MessageBox.Show("You WIN");
                        //endgame;
                    }


                }
            }

            Console.Write(Life);
            lbl_Lifes.Text = Life.ToString();

        }
        private void Endgame(bool x)
        {
            if (x == true)
            {
                Mario.Left = 128;
                Mario.Top = 512;
                timer1.Stop();
                Form1 frm1 = new Form1();
                Form2 frm2 = new Form2();
                Form3 frm3 = new Form3();
                frm3.Show();
                this.Hide();
                if (Mario.Bounds.IntersectsWith(pictureBox36.Bounds))
                {

                }

            }

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
                EndGame = false;
                //MessageBox.Show("Gl");
            }


            if (e.KeyCode == Keys.Up)
            {

                // gravity = -128;
                if (!MarioFlying)
                {
                    gravity = -160;
                    gravity = (int)Math.Round(gravity* accelration);
                    MarioFlying = true;
                }

            }
            if (e.KeyCode == Keys.Left)
                Rörelse = -32;

            if (e.KeyCode == Keys.Right)
                Rörelse =32;

            if (e.KeyCode == Keys.ShiftKey)
            {
                turbo = true;
                timer2.Start();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
                Rörelse = 0;
            if (e.KeyCode == Keys.Right)
                Rörelse = 0;

            if (e.KeyCode == Keys.ShiftKey)
            {
                turbo = false;
                timer2.Stop();
            }

        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {

        }
    }
}
