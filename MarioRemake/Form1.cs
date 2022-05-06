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
using System.Threading;
using System.Media;
using MarioRemake.Properties;


namespace MarioRemake
{
    //Problem att lösa vid errort, Vilket är pipe vilket löser bannans längd problem
    //Size 1459; 896
    //Kollition i sidled
    //Flera Ljud effekter samtidigt
    //kollition med block underifrån
    //Fixa så mario inte rör sig för mycket åt höger Löses delvis av punkt 1
    //bygga ut kartor och bygga fler, totalt 8 stycken är målet.

    public partial class TestWorld : Form
    {
        public TestWorld()
        {
            InitializeComponent();
        }

        int gravity = 0;
        int Rörelse = 0;
        int Gumbarörelse = -1;
        int GumbaGravity = 2;
        int Mushroomrörelse = -1;
        int Mushroomgravity = 2;
        int backgrundRörelse = 0;
        int totalBackgroundRörelse = 0;
        int totalMarioRörelse = 0;
        int temp = 0;
        int temp2 = 0;


        bool RörelseFram = false;
        bool RörelseBak = false;
        bool RörelseUp = false;
        bool RörelseUp2 = false;
        bool MarioFlying = true;
        bool Gumba1Flying = true;
        bool turbo = false;
        double accelration = 1;
        bool marioGn = false;
        bool marioGn2 = false;
        bool marioGn3 = false;
        bool gumbaGn = false;
        bool mushroomGn = false;
        bool mushroom = false;
        bool neripipe = false;

        int temporär;
        bool EndGame;
        public static int Life = 5;

        SoundPlayer musik = new SoundPlayer(Resources.Mario4min);

        SoundPlayer jump = new SoundPlayer(Resources.MarioJump);

        SoundPlayer deathA = new SoundPlayer(Resources.MarioDeath);

        SoundPlayer permDeathA = new SoundPlayer(Resources.MarioPermDeath);

        //Ljud för vinst

        //Ljud för dödad monster


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            totalMarioRörelse += temporär;
            totalBackgroundRörelse += backgrundRörelse;
            label2.Text = mushroom.ToString();
            label1.Text = temp2.ToString();
            label3.Text = totalBackgroundRörelse.ToString();
            label5.Text = totalMarioRörelse.ToString();
            if (turbo== true) //&& !MarioFlying) När jag har fixat röra block osv
            {
                accelration = 1.8;
            }
            else
            {
                accelration = 1;
            }

            if (RörelseFram == true)
            {

                Rörelse = 7;
            }
            else if (RörelseBak == true)
            {

                Rörelse = -7;
            }
            else
            {

                Rörelse = 0;
            }

            if (RörelseUp == true)
            {
                if (temp==7 || temp== 8)
                {
                    gravity = -25;
                }
                else if (temp == 1||temp==2)
                {
                    gravity = -15;
                }
                else if (temp==3||temp==4||temp==5||temp==6)
                {
                    gravity= -20;
                }
            }
            if (gravity == 0)
            {
                temp2 = 0;
            }


            temporär = (int)Math.Round(Rörelse * accelration);


            Mario.Top += gravity;
            Mario.Left += temporär;

            Gumba1.Top += GumbaGravity;
            Gumba1.Left += Gumbarörelse;

            Mushroom1.Top += Mushroomgravity;
            Mushroom1.Left += Mushroomrörelse;







            marioGn = false;
            gumbaGn = false;
            mushroomGn = false;
            marioGn2 = false;
            marioGn3 = false;
            MarioFlying = false;
            if (gravity != 0)
            {
                MarioFlying = true;
            }
            else
                MarioFlying=false;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "Marken") //Ändra om så att den faktiskt känner av bottom och top med lite plus minus top/ botten. Sdian är fortfarande oklar 
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        marioGn2 = true;
                        marioGn3 = true;


                        if (Mario.Bottom == x.Top)
                        {
                            marioGn = true;
                            temp2 = 0;



                        }




                        if (Mario.Top == x.Bottom)
                        {
                            marioGn=false;
                            gravity += 15;
                        }/*
                        if (MarioFlying == true)
                        {
                            if (Mario.Left >= x.Right ||Mario.Left <= x.Right - 5)
                            {
                                marioGn3 = false;
                                RörelseFram = false;
                            }
                            else
                                marioGn3 = true;
                            if (Mario.Right >= x.Left || Mario.Right <= x.Left + 5)
                            {
                                marioGn2 = false;
                                RörelseBak = false;
                            }
                            else
                                marioGn2 = true;
                            {
                                marioGn2 = true;
                                marioGn3 = true;
                            }
                        }
                        */





                    }
                    else
                    {



                    }
                    #region

                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn = true;

                    }
                    if (Mushroom1.Bounds.IntersectsWith(x.Bounds))
                    {

                        mushroomGn = true;

                    }



                }
                else


                if (Mario.Top>900)
                {
                    death(true);
                    if (mushroom == true)
                    {
                        reset(true);

                        Life--;

                        musik.Stop();
                        deathA.Play();
                        MessageBox.Show("Eehe ehehe ehee");
                    }

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
                if (x is PictureBox && (string)x.Tag == "mushroom")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        mushroom = true;
                        //bytter storlek
                        x.Dispose();
                        Mario.Size = new Size(64, 128);
                        Mario.Top += -64;

                    }

                }

                //Måste ha ett bättre system

                if (x is PictureBox && (string)x.Tag != "Mario")
                {
                    if (Mario.Left>450 && Mario.Left<15500)
                    {
                        backgrundRörelse = (temporär * -1);
                        x.Left += backgrundRörelse;


                    }


                }
                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    if (mushroom == false)
                    {
                        if (Mario.Bounds.IntersectsWith(x.Bounds))
                        {
                            //gumbaKollition
                            #region 

                            if (Mario.Bottom == x.Top)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+1)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+2)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+3)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+4)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+5)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+6)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+7)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+8)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+9)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+10)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            #endregion


                            else
                            {
                                death(true);
                                //deathA.Play();
                            }




                        }
                    }
                    else
                    {
                        if (Mario.Bounds.IntersectsWith(x.Bounds))
                        {
                            //gumbaKollition
                            #region 

                            if (Mario.Bottom == x.Top)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+1)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+2)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+3)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+4)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+5)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+6)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+7)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+8)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+9)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+10)
                            {
                                temp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            #endregion


                            else
                            {
                                death(true);
                                mushroom = false;
                                Mario.Size = new Size(64, 64);



                                x.Dispose();
                            }


                        }

                    }
                }
                *
                if (x is PictureBox && (string)x.Tag == "pipe") //Den mario under måste sättas i en utanför pipe denna jag skriver på nu då den endast flyttar pipen annars,
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {

                        #region 

                        if (Mario.Bottom == x.Top)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }


                        }
                        else if (Mario.Bottom == x.Top+1)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+2)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+3)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+4)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+5)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+6)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+7)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+8)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+9)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }
                        }
                        else if (Mario.Bottom == x.Top+10)
                        {
                            marioGn = true;
                            temp2 = 0;
                            if (neripipe == true)
                            {
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {

                                        x.Left += 1000;


                                    }


                                }
                                Mario.Left += 300;
                            }

                        }
                        #endregion


                        else
                        {

                        }





                    }



                }
            }



            #endregion

            if (!marioGn)
            {
                temp2++;
                if (temp2 == 1)
                {
                    gravity = 2;
                }
                else if (temp2 == 2)
                {
                    gravity = 2;
                }
                else if (temp2 == 3)
                {
                    gravity = 4;
                }
                else if (temp2 == 4)
                {
                    gravity = 4;
                }
                else if (temp2 == 5)
                {
                    gravity = 4;
                }
                else if (temp2 == 6)
                {
                    gravity = 6;
                }
                else if (temp2 == 7)
                {
                    gravity = 6;
                }
                else if (temp2 == 8)
                {
                    gravity = 6;
                }
                else if (temp2 == 9)
                {
                    gravity = 6;
                }
                else if (temp2 == 10)
                {
                    gravity = 8;
                }
                else if (temp2 == 11)
                {
                    gravity = 8;
                }
                else if (temp2 == 12)
                {
                    gravity = 8;
                }
                else if (temp2 == 13)
                {
                    gravity = 8;
                }
                else if (temp2 == 14)
                {
                    gravity = 8;
                }
                else if (temp2 == 15)
                {
                    gravity = 10;
                }
                else if (temp2 == 16)
                {
                    gravity = 10;
                }
                else if (temp2 == 17)
                {
                    gravity = 10;
                }
                else if (temp2 == 18)
                {
                    gravity = 10;
                }
                else if (temp2 == 19)
                {
                    gravity = 10;
                }
                else if (temp2 == 20)
                {
                    gravity = 10;
                }
                else if (temp2 == 21)
                {
                    gravity = 12;
                }
                else if (temp2 == 22)
                {
                    gravity = 12;
                }
                else if (temp2 == 23)
                {
                    gravity = 12;
                }
                else if (temp2 == 24)
                {
                    gravity = 12;
                }
                else if (temp2 == 25)
                {
                    gravity = 12;
                }
                else
                {
                    gravity = 14;
                }
            }
            else
            {
                gravity = 0;
            }


            if (!marioGn2)
            {
                Rörelse = 0;
            }
            else
            {
                Rörelse = 0;
            }


            if (!gumbaGn)
            {
                GumbaGravity = 4;
                Gumbarörelse = 0;
            }
            else
            {
                GumbaGravity = 0;
                Gumbarörelse = -3;
            }
            if (!mushroomGn)
            {
                Mushroomgravity = 4;
                Mushroomrörelse = 0;
            }
            else
            {
                Mushroomgravity = 0;
                Mushroomrörelse = -3;
            }






            lbl_Lifes.Text = Life.ToString();
            temp--;

        }


        private void Endgame(bool x)
        {
            if (x == true)
            {
                Mario.Left = 128;
                Mario.Top = 512;
                timer1.Stop();
                musik.Stop();


                TestWorld frm1 = new TestWorld();
                LoadingScreen frm2 = new LoadingScreen();
                WorldSelector frm3 = new WorldSelector();
                frm3.Show();
                this.Hide();



            }

        }

        private void death(bool y)
        {
            if (mushroom!= true)
            {
                if (y == true)
                {
                    if (Life>1)
                    {
                        reset(true);

                        Life--;

                        musik.Stop();
                        deathA.Play();
                        MessageBox.Show("Eehe ehehe ehee");

                    }
                    else
                    {

                        Endgame(true);
                        musik.Stop();
                        permDeathA.Play();
                        MessageBox.Show("GAME OVER");
                        Life = 5;



                        // Form1 frm1 = new Form1();
                        //Form2 frm2 = new Form2();
                        //Form3 frm3 = new Form3();

                        //                    frm3.Show();

                    }
                }
            }
            else
            {



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
            frm1.FormBorderStyle = FormBorderStyle.None;
            frm1.Bounds = Screen.PrimaryScreen.Bounds;

            this.Hide();
            frm1.Show();
        }


        /*
        private void Inivicbility(bool x)
        {
            if (x == true)
            {

                Task.Delay(2500);
                Inivicbility(false);
            }
        } */


        private void TestWorld_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
                //musik.PlayLooping();
                EndGame = false;
                //MessageBox.Show("Gl");
            }


            if (e.KeyCode == Keys.Up)
            {
                if (marioGn)
                {

                    //jump.Play();
                    //  jump.Play();

                    RörelseUp = true;
                    marioGn = false;
                    temp = 8;




                }

            }
            if (e.KeyCode == Keys.Left)
            //  if (marioGn2)
            {
                RörelseBak = true;
            }


            if (e.KeyCode == Keys.Right)
            //if (marioGn3)
            {
                RörelseFram = true;
            }

            //Rörelse =32;

            if (e.KeyCode == Keys.ShiftKey)
            {
                if (marioGn)
                {
                    turbo = true;
                    timer2.Start();

                }

            }
            if (e.KeyCode == Keys.Down)
            {
                neripipe = true;
            }
        }


        private void TestWorld_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                RörelseBak = false;
            if (e.KeyCode == Keys.Right)
                RörelseFram = false;

            if (e.KeyCode == Keys.ShiftKey)
            {
                turbo = false;
                timer2.Stop();
            }
            if (e.KeyCode == Keys.Down)
            {
                neripipe = false;
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
