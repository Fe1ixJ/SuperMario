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
    //Kollition i sidled Ge tagen vägen marken samt vämarken där den prioriterar en av dem
    //bygga ut kartor och bygga fler, totalt 8 stycken är målet.

    public partial class TestWorld : Form
    {
        public TestWorld()
        {
            InitializeComponent();
        }
        //Definition av nästan alla variablar
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

        //Definition av alla bool
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
        public static int Life = 3;

        //Ljudeffekternas koppling till Resources mappen.
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
            //Timmer vilken upptaterar hela spelet framåt.

            //Temporära variablar för data
            totalMarioRörelse += temporär;
            totalBackgroundRörelse += backgrundRörelse;
            label2.Text = mushroom.ToString();
            label1.Text = temp2.ToString();

            //Marios rörelse accelrerad rörelse och vanlig rörelse

            if (turbo== true)
            {
                accelration = 1.8;
            }
            else
            {
                accelration = 1;
            }
            if (Mario.Left<450)
            {
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
            }
            else
            {
                if (RörelseFram == true)
                {

                    Rörelse = 3;
                }
                else if (RörelseBak == true)
                {

                    Rörelse = -3;
                }
                else
                {

                    Rörelse = 0;
                }
            }
            //Marios gravitation
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

            //Räknar ut den riktiga hastigheten efter att turbo kan ge en accelration
            temporär = (int)Math.Round(Rörelse * accelration);
            //Kollar alla objekt i formuläret, Labels, Picturboxes osv
            foreach (Control x in this.Controls)
            {
                //Flyttar Bakgrunden och allt annat så det ser ut som Mario rör sig framåt
                if (x is PictureBox && (string)x.Tag != "Mario")
                {
                    if (Mario.Left>450 && Mario.Left<15500)
                    {
                        backgrundRörelse = (int)Math.Round((temporär* -2.5));
                        x.Left += backgrundRörelse;



                    }
                    label3.Text = backgrundRörelse.ToString();
                    label5.Text = temporär.ToString();


                }
            }
            //Föremålen i spelet som rör på sigs upptatering både i höjdled och sidled.
            Mario.Top += gravity;
            Mario.Left += temporär;

            Gumba1.Top += GumbaGravity;
            Gumba1.Left += Gumbarörelse;

            Mushroom1.Top += Mushroomgravity;
            Mushroom1.Left += Mushroomrörelse;

            //Sätter ett antal true/false satser till false så att de kan aktiveras och utföra sitt syfte, De låser rörelse i höjdled osv samt låter saker falla och kan låsa från att gå höger/vänster 
            marioGn = false;
            gumbaGn = false;
            mushroomGn = false;
            marioGn2 = false;
            marioGn3 = false;
            MarioFlying = false;
            //Kollar om mario Flyger eller inte
            if (gravity != 0)
            {
                MarioFlying = true;
            }
            else
                MarioFlying=false;
            foreach (Control x in this.Controls)
            {
                //Kollar om Mario rör marken samt uptaterar isåfall gör den så han inte kan falla.
                if (x is PictureBox && (string)x.Tag == "Marken")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        marioGn2 = true;
                        marioGn3 = true;
                        //x.Top = Mario.Bottom;


                        if (Mario.Bottom == x.Top)
                        {
                            marioGn = true;
                            temp2 = 0;

                        }
                        if (Mario.Top == x.Bottom)
                        {
                            marioGn=false;
                            gravity += 15;
                        }
                    }
                    //Kollar om gumba och likande rör vid marken så att de inte ska falla genom marken
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn = true;

                    }
                    if (Mushroom1.Bounds.IntersectsWith(x.Bounds))
                    {
                        mushroomGn = true;
                    }
                }


                //Kollar om Mario faller ut ur världen och dödar honom
                if (Mario.Top>900)
                {
                    death(true);
                    if (mushroom == true)
                    {
                        reset(true);

                        Life--;

                        //musik.Stop();
                        deathA.Play();
                        MessageBox.Show("Eehe ehehe ehee");
                    }

                }
                //Gör att Mario inte kan röra sig längre åt vänster
                if (Mario.Left<=0)
                {
                    Mario.Left = 0;
                }

                //Testar om mario rör Flagstoplsknopen och ger ett liv samt avslutar spelet
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
                //Testar om mario rör flagstlpen och avlsutar spelet
                if (x is PictureBox && (string)x.Tag == "Flagstolpe")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        Endgame(true);
                        MessageBox.Show("You WIN");
                        //endgame;
                    }
                }
                //ger Mario en powerup som förendrar spelfältet
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
                //Kollisiontest med en gumba som kollar om man dör till den, förlorar sin powerup eller om man döda monstret.
                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    //Om mushroom inte är igång dör man
                    if (mushroom == false)
                    {
                        if (Mario.Bounds.IntersectsWith(x.Bounds))
                        {
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
                            }
                        }
                    }
                    else
                    {
                        //Vad som händer om man har mushroom
                        if (Mario.Bounds.IntersectsWith(x.Bounds))
                        {
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
                //En pipe och vad som händer när man rör vid den, Att allt ska flyttas och få nya positioner så Man kan göra längre bannor.
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
                    }
                }
            }
            //Ett gravitations system då det accelerar lite när det faller
            #region
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
            #endregion

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
                        Life = 3;
                    }
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
            frm1.FormBorderStyle = FormBorderStyle.None;
            frm1.Bounds = Screen.PrimaryScreen.Bounds;

            this.Hide();
            frm1.Show();

        }

        private void TestWorld_KeyDown(object sender, KeyEventArgs e)
        {
            //Här är alla mina Knappar och vad som händer när man trycker på dem, Beskrving av vad knapparna gör kan man hitta under "How to play"
            if (e.KeyCode == Keys.P)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
                EndGame = false;
                musik.Play();
            }


            if (e.KeyCode == Keys.Up)
            {
                if (marioGn)
                {
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
            //Här är när man slutar hålla nere knapparna och man t.ex stanar.
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


    }

}