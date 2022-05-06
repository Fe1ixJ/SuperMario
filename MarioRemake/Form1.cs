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
    //Kollition i sidled
    //Sätta gravity så den accelerar lite granna när den faller
    //Kan fixa två olika hopp höjder låter den kolla om man håller ner mer en en gång test. temp om den redan är 8(4) ändra till 4 och om den är ner tryckt när det är 4 och direkt efter så gör hoppet så högt som nu annars lägre
    //Ljud effekter
    //powerup och kollition med block underifrån
    //bygga ut kartor och bygga fler, totalt 8 stycken är målet.
    //Om gumba rör dig så dör du och den försvinner
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

        int temporär;
        bool EndGame;
        public static int Life = 5;

        SoundPlayer musik = new SoundPlayer(Resources.Mario4min);

        SoundPlayer jump = new SoundPlayer(Resources.MarioJump);

        SoundPlayer deathA = new SoundPlayer(Resources.MarioDeath);

        SoundPlayer permDeathA = new SoundPlayer(Resources.MarioPermDeath);


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = mushroom.ToString();
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
                                deathA.Play();
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



                                x.Dispose();
                            }


                        }

                    }
                }
            }



            #endregion

            if (!marioGn)
            {
                gravity = 10;
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
            if (mushroom!= true)
            {
                if (y == true)
                {
                    if (Life>1)
                    {
                        reset(true);

                        Life--;
                        MessageBox.Show("Eehe ehehe ehee");
                        deathA.Play();

                    }
                    else
                    {

                        Endgame(true);
                        MessageBox.Show("GAME OVER");
                        Life = 5;
                        permDeathA.Play();


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
                musik.Play();
                EndGame = false;
                //MessageBox.Show("Gl");
            }


            if (e.KeyCode == Keys.Up)
            {
                if (marioGn)
                {

                    //  jump.Play();
                    //  jump.Play();

                    RörelseUp = true;
                    marioGn = false;
                    temp = 8;




                }

            }
            if (e.KeyCode == Keys.Left)
                if (marioGn2)
                {
                    RörelseBak = true;
                }


            if (e.KeyCode == Keys.Right)
                if (marioGn3)
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
