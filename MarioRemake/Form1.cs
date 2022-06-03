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
    //Ljud startas om efter död
    //Size 1459; 896
    //bygga ut kartor och bygga fler, totalt 8 stycken är målet.

    public partial class World11 : Form
    {
        public World11()
        {
            InitializeComponent();
        }
        //Definition av nästan alla variablar
        int gravity = 0;
        int Rörelse = 0;
        int Gumbarörelse = -1;
        int GumbaGravity = 2;
        int Gumbarörelse2 = -1;
        int GumbaGravity2 = 2;
        int Gumbarörelse3 = -1;
        int GumbaGravity3 = 2;
        int Gumbarörelse4 = -1;
        int GumbaGravity4 = 2;
        int Mushroomrörelse = -1;
        int Mushroomgravity = 2;
        int backgrundRörelse = 0;
        int totalBackgroundRörelse = 0;
        int totalMarioRörelse = 0;
        int temp = 0;
        int temp2 = 0;
        int temp3 = 0;
        int stuts = 0;
        int temp4 = 1;
        int temp5 = 1;
        int temp6 = 1;
        int temp7 = 1;
        int temp8 = 1;

        //Definition av alla bool
        bool RörelseFram = false;
        bool RörelseBak = false;
        bool RörelseUp = false;
        bool RörelseUp2 = false;
        bool Gumba1Flying = true;
        bool turbo = false;
        double accelration = 1;
        bool marioGn = false;
        bool marioGn2 = false;
        bool marioGn3 = false;
        bool gumbaGn = false;
        bool gumbaGn2 = false;
        bool gumbaGn3 = false;
        bool gumbaGn4 = false;
        bool mushroomGn = false;
        bool mushroom = false;
        bool neripipe = false;
        bool teleportera = false;

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
            label13.Text = temp3.ToString();

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
                    //PIPE KONtrOL Och Skicka iVäg Den

                    label3.Text = backgrundRörelse.ToString();
                    label5.Text = temporär.ToString();


                }

            }
            //Föremålen i spelet som rör på sigs upptatering både i höjdled och sidled.
            Mario.Top += gravity;
            Mario.Left += temporär;

            Gumba1.Top += GumbaGravity;
            Gumba1.Left += Gumbarörelse;

            Gumba2.Top += GumbaGravity2;
            Gumba2.Left += Gumbarörelse2;

            Gumba3.Top += GumbaGravity3;
            Gumba3.Left += Gumbarörelse3;

            Gumba4.Top += GumbaGravity4;
            Gumba4.Left += Gumbarörelse4;

            Mushroom1.Top += Mushroomgravity;
            Mushroom1.Left += Mushroomrörelse;

            //Sätter ett antal true/false satser till false så att de kan aktiveras och utföra sitt syfte, De låser rörelse i höjdled osv samt låter saker falla och kan låsa från att gå höger/vänster 
            marioGn = false;
            gumbaGn = false;
            gumbaGn2 = false;
            gumbaGn3 = false;
            gumbaGn4 = false;
            mushroomGn = false;
            marioGn2 = false;
            marioGn3 = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "VägOMark")
                {

                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        label6.Text = Mario.Bottom.ToString();
                        label7.Text = x.Top.ToString();
                        label8.Text = "False";
                        label9.Text = "False";
                        if (Mario.Bottom == x.Top ||Mario.Bottom == x.Top+1 || Mario.Bottom == x.Top+2 || Mario.Bottom == x.Top+3 || Mario.Bottom == x.Top+4 || Mario.Bottom == x.Top+5|| Mario.Bottom == x.Top+6 || Mario.Bottom == x.Top+7 || Mario.Bottom == x.Top+8 ||Mario.Bottom == x.Top+9 ||Mario.Bottom == x.Top+10 ||Mario.Bottom == x.Top+11 ||Mario.Bottom == x.Top+12)
                        {
                            marioGn = true;
                            temp2 = 0;
                            //MessageBox.Show("noob");
                            //label8.Text = "True";


                        }
                        else //if (Mario.Bottom == x.Top+13 ||Mario.Bottom == x.Top+14 || Mario.Bottom == x.Top+15 || Mario.Bottom == x.Top+16 || Mario.Bottom == x.Top+17 || Mario.Bottom == x.Top+18|| Mario.Bottom == x.Top+19 || Mario.Bottom == x.Top+20 || Mario.Bottom == x.Top+21 ||Mario.Bottom == x.Top+22 ||Mario.Bottom == x.Top+23 ||Mario.Bottom == x.Top+24 ||Mario.Bottom == x.Top+25 || Mario.Bottom == x.Top+26 ||Mario.Bottom == x.Top+27 || Mario.Bottom == x.Top+28 || Mario.Bottom == x.Top+29 || Mario.Bottom == x.Top+30 || Mario.Bottom == x.Top+31|| Mario.Bottom == x.Top+32 || Mario.Bottom == x.Top+33 || Mario.Bottom == x.Top+34 ||Mario.Bottom == x.Top+35 ||Mario.Bottom == x.Top+36 ||Mario.Bottom == x.Top+37 ||Mario.Bottom == x.Top+38 || Mario.Bottom == x.Top+39 ||Mario.Bottom == x.Top+40 || Mario.Bottom == x.Top+41 || Mario.Bottom == x.Top+42 || Mario.Bottom == x.Top+43 || Mario.Bottom == x.Top+44|| Mario.Bottom == x.Top+45 || Mario.Bottom == x.Top+46 || Mario.Bottom == x.Top+47 ||Mario.Bottom == x.Top+48 ||Mario.Bottom == x.Top+49 ||Mario.Bottom == x.Top+50 ||Mario.Bottom == x.Top+51 || Mario.Bottom == x.Top ||Mario.Bottom == x.Top+52 || Mario.Bottom == x.Top+53 || Mario.Bottom == x.Top+54 || Mario.Bottom == x.Top+55 || Mario.Bottom == x.Top+56|| Mario.Bottom == x.Top+57 || Mario.Bottom == x.Top+58 || Mario.Bottom == x.Top+59 ||Mario.Bottom == x.Top+60 ||Mario.Bottom == x.Top+61 ||Mario.Bottom == x.Top+62 ||Mario.Bottom == x.Top+63 || Mario.Bottom == x.Top+64)
                        {

                            {
                                //   label9.Text = "true";
                                //MessageBox.Show("FuckYou");

                                if (Mario.Right == x.Left+1)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-1)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+2)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-2)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+3)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-3)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+4)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-4)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+5)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-5)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+6)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-6)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+7)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-7)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+8)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-8)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+9)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-9)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+10)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-10)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+11)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-11)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+12)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-12)
                                {

                                    stuts = 30;

                                }
                                Mario.Left += stuts;


                            }


                        }




                        // else
                        {
                            // marioGn2 = true;
                            // marioGn3 = true;
                        }




                    }
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba1.Bottom == x.Top ||Gumba1.Bottom == x.Top+1 || Gumba1.Bottom == x.Top+2 || Gumba1.Bottom == x.Top+3 || Gumba1.Bottom == x.Top+4 || Gumba1.Bottom == x.Top+5|| Gumba1.Bottom == x.Top+6 || Gumba1.Bottom == x.Top+7 || Gumba1.Bottom == x.Top+8 ||Gumba1.Bottom == x.Top+9 ||Gumba1.Bottom == x.Top+10 ||Gumba1.Bottom == x.Top+11 ||Gumba1.Bottom == x.Top+12)
                        {
                            gumbaGn = true;


                        }
                        else
                        {
                            temp4 = temp4*-1;

                            label10.Text = temp4.ToString();
                        }
                    }

                    if (Gumba2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba2.Bottom == x.Top ||Gumba2.Bottom == x.Top+1 || Gumba2.Bottom == x.Top+2 || Gumba2.Bottom == x.Top+3 || Gumba2.Bottom == x.Top+4 || Gumba2.Bottom == x.Top+5|| Gumba2.Bottom == x.Top+6 || Gumba2.Bottom == x.Top+7 || Gumba2.Bottom == x.Top+8 ||Gumba2.Bottom == x.Top+9 ||Gumba2.Bottom == x.Top+10 ||Gumba2.Bottom == x.Top+11 ||Gumba2.Bottom == x.Top+12)
                        {
                            gumbaGn2 = true;


                        }
                        else
                        {
                            temp6 = temp6*-1;

                            label10.Text = temp4.ToString();
                        }
                    }

                    if (Gumba3.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba3.Bottom == x.Top ||Gumba3.Bottom == x.Top+1 || Gumba3.Bottom == x.Top+2 || Gumba3.Bottom == x.Top+3 || Gumba3.Bottom == x.Top+4 || Gumba3.Bottom == x.Top+5|| Gumba3.Bottom == x.Top+6 || Gumba3.Bottom == x.Top+7 || Gumba3.Bottom == x.Top+8 ||Gumba3.Bottom == x.Top+9 ||Gumba3.Bottom == x.Top+10 ||Gumba3.Bottom == x.Top+11 ||Gumba3.Bottom == x.Top+12)
                        {
                            gumbaGn3 = true;


                        }
                        else
                        {
                            temp7 = temp7*-1;

                            label10.Text = temp4.ToString();
                        }
                    }

                    if (Gumba4.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba4.Bottom == x.Top ||Gumba4.Bottom == x.Top+1 || Gumba4.Bottom == x.Top+2 || Gumba4.Bottom == x.Top+3 || Gumba4.Bottom == x.Top+4 || Gumba4.Bottom == x.Top+5|| Gumba4.Bottom == x.Top+6 || Gumba4.Bottom == x.Top+7 || Gumba4.Bottom == x.Top+8 ||Gumba4.Bottom == x.Top+9 ||Gumba4.Bottom == x.Top+10 ||Gumba4.Bottom == x.Top+11 ||Gumba4.Bottom == x.Top+12)
                        {
                            gumbaGn4 = true;


                        }
                        else
                        {
                            temp8 = temp8*-1;

                            label10.Text = temp4.ToString();
                        }
                    }
                    //Kollar om gumba och likande rör vid marken så att de inte ska falla genom marken
                    if (Mushroom1.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Mushroom1.Bottom == x.Top ||Mushroom1.Bottom == x.Top+1 || Mushroom1.Bottom == x.Top+2 || Mushroom1.Bottom == x.Top+3 || Mushroom1.Bottom == x.Top+4 || Mushroom1.Bottom == x.Top+5|| Mushroom1.Bottom == x.Top+6 || Mushroom1.Bottom == x.Top+7 || Mushroom1.Bottom == x.Top+8 ||Mushroom1.Bottom == x.Top+9 ||Mushroom1.Bottom == x.Top+10 ||Mushroom1.Bottom == x.Top+11 ||Mushroom1.Bottom == x.Top+12)
                        {
                            mushroomGn = true;
                        }
                        else
                        {
                            temp5 = temp5*-1;


                        }
                    }
                }

                //Kollar om Mario rör marken samt uptaterar isåfall gör den så han inte kan falla.
                if (x is PictureBox && (string)x.Tag == "Marken")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        //marioGn2 = true;
                        //marioGn3 = true;
                        //x.Top = Mario.Bottom;



                    }
                    //Kollar om gumba och likande rör vid marken så att de inte ska falla genom marken
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn = true;

                    }
                    if (Gumba2.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn2 = true;

                    }
                    if (Gumba3.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn3 = true;

                    }
                    if (Gumba4.Bounds.IntersectsWith(x.Bounds))
                    {

                        gumbaGn4 = true;

                    }
                    if (Mushroom1.Bounds.IntersectsWith(x.Bounds))
                    {
                        mushroomGn = true;
                    }
                }

                if (x is PictureBox && (string)x.Tag == "Väg")
                {
                    {


                        if (Mario.Bounds.IntersectsWith(x.Bounds))
                        {
                            if (Mario.Bottom >= x.Top)
                            {
                                marioGn2 = true;
                                marioGn3 = true;
                                //x.Top = Mario.Bottom;


                                if (Mario.Right == x.Left+1)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-1)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+2)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-2)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+3)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-3)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+4)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-4)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+5)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-5)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+6)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-6)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+7)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-7)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+8)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-8)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+9)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-9)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+10)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-10)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+11)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-11)
                                {

                                    stuts = 30;

                                }
                                if (Mario.Right == x.Left+12)
                                {

                                    stuts = -30;

                                }
                                if (Mario.Left == x.Right-12)
                                {

                                    stuts = 30;

                                }
                            }


                            else

                            {
                                marioGn2 = true;
                                marioGn3 = true;
                            }

                            Mario.Left += stuts;

                        }
                    }
                    //Kollar om gumba och likande rör vid marken så att de inte ska falla genom marken
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {
                        gumbaGn = true;

                    }
                    if (Gumba2.Bounds.IntersectsWith(x.Bounds))
                    {
                        gumbaGn2 = true;

                    }
                    if (Mushroom1.Bounds.IntersectsWith(x.Bounds))
                    {
                        mushroomGn = true;
                    }
                }
                if (x is PictureBox && (string)x.Tag == "pipe1") //Den mario under måste sättas i en utanför pipe denna jag skriver på nu då den endast flyttar pipen annars,
                {

                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        temp2 = 0;
                        if (Mario.Bottom == x.Top)
                        {

                            if (neripipe == true)
                            {
                                /*
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {
                                        x.Left += 1000;
                                    }
                                }
                                Mario.Left += 300;
                                */
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+1)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+2)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+3)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);

                            }
                        }
                        else if (Mario.Bottom == x.Top+4)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+5)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+6)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+7)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+8)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+9)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+10)
                        {

                            if (neripipe == true)
                            {
                                teleport1(true);
                            }

                        }

                    }
                }

                if (x is PictureBox && (string)x.Tag == "pipe2") //Den mario under måste sättas i en utanför pipe denna jag skriver på nu då den endast flyttar pipen annars,
                {

                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        temp2 = 0;
                        if (Mario.Bottom == x.Top)
                        {

                            if (neripipe == true)
                            {
                                /*
                                if (x is PictureBox && (string)x.Tag != "Mario")
                                {
                                    if (Mario.Left>450 && Mario.Left<15500)
                                    {
                                        x.Left += 1000;
                                    }
                                }
                                Mario.Left += 300;
                                */
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+1)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+2)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+3)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);

                            }
                        }
                        else if (Mario.Bottom == x.Top+4)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+5)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+6)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+7)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+8)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+9)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }
                        }
                        else if (Mario.Bottom == x.Top+10)
                        {

                            if (neripipe == true)
                            {
                                teleport2(true);
                            }

                        }

                    }
                }
                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba1 != x)
                        {
                            temp4 = temp4*-1;
                        }
                    }
                    if (Gumba2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba2 != x)
                        {
                            temp6 = temp6*-1;
                        }

                    }
                    if (Gumba3.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba3 != x)
                        {
                            temp7 = temp7*-1;
                        }

                    }
                    if (Gumba4.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba4 != x)
                        {
                            temp8 = temp8*-1;
                        }

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
                Gumbarörelse = -3*temp4;
            }
            if (!gumbaGn2)
            {
                GumbaGravity2 = 4;
                Gumbarörelse2 = 0;
            }
            else
            {
                GumbaGravity2 = 0;
                Gumbarörelse2 = -3*temp6;

            }
            if (!gumbaGn3)
            {
                GumbaGravity3 = 4;
                Gumbarörelse3 = 0;
            }
            else
            {
                GumbaGravity3 = 0;
                Gumbarörelse3 = -3*temp7;

            }
            if (!gumbaGn4)
            {
                GumbaGravity4 = 4;
                Gumbarörelse4 = 0;
            }
            else
            {
                GumbaGravity4 = 0;
                Gumbarörelse4 = -3*temp8;

            }

            if (!mushroomGn)
            {
                Mushroomgravity = 4;
                Mushroomrörelse = 0;
            }
            else
            {
                Mushroomgravity = 0;
                Mushroomrörelse = -3*temp5;
            }
            if (temp3 >100)
            {
                timer2.Stop();
                temp3 = 0;
            }

            lbl_Lifes.Text = Life.ToString();
            temp--;
            label12.Text =temp.ToString();

        }


        private void Endgame(bool x)
        {
            if (x == true)
            {
                Mario.Left = 128;
                Mario.Top = 512;
                timer1.Stop();
                musik.Stop();

                World11 frm1 = new World11();
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

            World11 frm1 = new World11();
            LoadingScreen frm2 = new LoadingScreen();
            WorldSelector frm3 = new WorldSelector();
            frm1.FormBorderStyle = FormBorderStyle.None;
            frm1.Bounds = Screen.PrimaryScreen.Bounds;

            this.Hide();
            frm1.Show();

        }
        private void teleport1(bool x) //FIXA TILL DETTA
        {
            foreach (Control y in this.Controls)
            {
                if (temp3 == 0)
                {
                    if (y is PictureBox && (string)y.Tag != "Mario")
                    {
                        if (Mario.Left>450 && Mario.Left<15500)
                        {
                            y.Left -= 1280;
                            timer2.Start();
                        }



                    }
                }
            }
            Mario.Left += 0;
        }

        private void teleport2(bool x)
        {
            foreach (Control y in this.Controls)
            {
                if (temp3 == 0)
                {
                    if (y is PictureBox && (string)y.Tag != "Mario")
                    {
                        if (Mario.Left>450 && Mario.Left<15500)
                        {
                            y.Left += 1360;
                            timer2.Start();
                        }



                    }
                }
            }
            Mario.Left += 0;
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
                //musik.Play();
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
            {
                //   if (!marioGn3)
                //  {
                RörelseBak = true;
                //    }
                //    else
                //    {
                //        RörelseBak = false;
                //    }
            }


            if (e.KeyCode == Keys.Right)
            {
                // if (!marioGn2)
                //{
                RörelseFram = true;
                //}
                //else
                // {
                //    RörelseFram = false;
                //}
            }


            //Rörelse =32;

            if (e.KeyCode == Keys.ShiftKey)
            {
                if (marioGn)
                {
                    turbo = true;
                    //timer2.Start();

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
                //timer2.Stop();
            }
            if (e.KeyCode == Keys.Down)
            {
                neripipe = false;
            }
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void TestWorld_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox43_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            temp3++;
        }

        private void pictureBox73_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox72_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox91_Click(object sender, EventArgs e)
        {

        }
    }

}