using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.Timers;


namespace MarioRemake
{
    public partial class TestWorld : Form
    {
        public TestWorld()
        {
            InitializeComponent();
        }

        int gravity = 0;
        int Rörelse = 0;
        int Gumbarörelse = -4;
        int GumbaGravity = 16;
        int backgrundRörelse = 0;
        int totalBackgroundRörelse = 0;

        bool MarioFlying = true;
        bool Gumba1Flying = true;
        bool turbo = false;
        double accelration = 1;
        bool marioGn = false;
        bool gumbaGn = false;

        int temporär;
        bool EndGame;
        public static int Life = 5;

        //SoundPlayer musik = new SoundPlayer();
        //SoundPlayer effekt = new SoundPlayer();


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (turbo== true) //&& !MarioFlying) När jag har fixat röra block osv
            {
                accelration = 1.8;
            }
            else
            {
                accelration = 1;
            }
            temporär = (int)Math.Round(Rörelse * accelration);


            Mario.Top += gravity;
            Mario.Left += temporär;

            Gumba1.Top += GumbaGravity;
            Gumba1.Left += Gumbarörelse;


            //Kamera


            marioGn = false;
            gumbaGn = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Marken")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {



                        marioGn = true;
                        //Mario.Top=x.Top - Mario.Height;

                        #region
                        /*
                                                if (Mario.Top == x.Bottom)
                                                {
                                                    //  Mario.Top
                                                    gravity = 0;
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
                                                //mario top vad som händer(Det under är för top) */
                        #endregion
                    }
                    else
                    {



                    }
                    #region

                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn = true;
                        /*
                        if (Gumba1.Top == x.Bottom)
                        {
                            //  Mario.Top
                            GumbaGravity = 4;
                            Gumba1Flying = true;

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
                        //mario top vad som händer(Det under är för top) */
                    }
                    else
                    {

                        // gravity = 4;

                    }


                }
                else


                if (Mario.Top>900)
                {
                    //death(true); 
                }
                if (Mario.Left<=0)
                {
                    Mario.Left = 0;

                }

                //Flagstongsknop test
                if (x is PictureBox && (string)x.Tag == "Flagstolpsknop")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Life<98)
                            Life++;
                        Endgame(true);
                        MessageBox.Show("You WIN + 1UP");


                    }

                }

                if (x is PictureBox && (string)x.Tag == "Flagstolpe")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        Endgame(true);
                        MessageBox.Show("You WIN");
                        //endgame;
                    }



                }

                //Måste ha ett bättre system

                if (x is PictureBox && (string)x.Tag != "Mario")
                {
                    if (Mario.Left>450 && Mario.Left<1500)
                    {
                        backgrundRörelse = (temporär * -1);
                        if (backgrundRörelse < 0)
                            x.Left += backgrundRörelse;






                    }




                }
            }


            #endregion

            if (!marioGn)
            {
                gravity = 16;
            }
            else
            {
                gravity = 0;
            }
            if (!gumbaGn)
            {
                GumbaGravity = 16;
                Gumbarörelse = 0;
            }
            else
            {
                GumbaGravity = 0;
                Gumbarörelse = -4;
            }





            lbl_Lifes.Text = Life.ToString();

        }
        private void Endgame(bool x)
        {
            if (x == true)
            {
                Mario.Left = 128;
                Mario.Top = 512;
                timer1.Stop();

                TestWorld frm1 = new TestWorld();
                LoadingScreen frm2 = new LoadingScreen();
                WorldSelector frm3 = new WorldSelector();
                frm3.Show();
                this.Hide();

                if (Mario.Bounds.IntersectsWith(pictureBox63.Bounds))
                {

                }

            }

        }

        private void death(bool y)
        {
            if (y == true)
            {
                if (Life>1)
                {

                    reset(true);


                    Life--;
                    MessageBox.Show("Eehe ehehe ehee");

                }
                else
                {




                    Endgame(true);
                    MessageBox.Show("GAME OVER");
                    Life = 5;

                    // Form1 frm1 = new Form1();
                    //Form2 frm2 = new Form2();
                    //Form3 frm3 = new Form3();

                    //                    frm3.Show();

                }
            }
        }
        private void reset(bool x)
        {
            Mario.Left = 128;
            Mario.Top = 512;
            timer1.Stop();

            TestWorld frm1 = new TestWorld();
            LoadingScreen frm2 = new LoadingScreen();
            WorldSelector frm3 = new WorldSelector();

            this.Hide();
            frm1.Show();
        }


        private void TestWorld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
                EndGame = false;
                //MessageBox.Show("Gl");
            }


            if (e.KeyCode == Keys.Up)
            {
                if (marioGn)
                {
                    marioGn = false;
                    gravity = -160;
                    gravity = (int)Math.Round(gravity* accelration);

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


        private void TestWorld_KeyUp(object sender, KeyEventArgs e)
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

        private void timer2_Tick(object sender, EventArgs e)
        {


        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }

        private void TestWorld_Load(object sender, EventArgs e)
        {
            // timer1.Start();
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {

        }
    }
}
