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
    //Det finns några variablar som inte används dock på grund av att jag inte vill förstöra koden så kommer jag inte att pila på dem och eventuellt förstöra programmet 

    public partial class World11 : Form
    {
        public World11() //World 1-1 Fungerar likadant i kommande formulär
        {
            InitializeComponent();
        }
        //Definition av nästan alla variablar. Gumba1,2,3 och 4 är för de olika gumbas
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
        int TicksEfterHopp = 0;
        int TicksILuften = 0;
        int TicksEfterTeleportering = 0;
        int stuts = 0;
        int Gumba1Riktning = 1;
        int Mushroom1Riktning = 1;
        int Gumba2Riktning = 1;
        int Gumba3Riktning = 1;
        int gumba4Riktning = 1;

        //Definition av alla bool 
        bool RörelseFram = false;
        bool RörelseBak = false;
        bool RörelseUp = false;
        bool turbo = false;
        double accelration = 1;
        //Alla som heter något med gn har med rörelse att göra, Alla utom mariogn2 och mariogn3 är för gravitationen den sätts true/false beroende på om mario, gumba eller mushroom faller
        bool marioGn = false;
        bool marioGn2 = false;
        bool marioGn3 = false;
        bool gumbaGn = false; //För gumba1 resterande är för gumba sedan sitt nummer
        bool gumbaGn2 = false;
        bool gumbaGn3 = false;
        bool gumbaGn4 = false;
        bool mushroomGn = false;
        bool mushroom = false; //om man är stora mario
        bool neripipe = false; //Kollar om man klicka att man vill ner i en pipe

        int Hastigheten; //Beräknar hastigheten när man springer
        bool EndGame;
        public static int Life = 3;

        //Ljudeffekternas koppling till Resources mappen.
        SoundPlayer musik = new SoundPlayer(Resources.Mario4min);

        SoundPlayer jump = new SoundPlayer(Resources.MarioJump);

        SoundPlayer deathA = new SoundPlayer(Resources.MarioDeath);

        SoundPlayer permDeathA = new SoundPlayer(Resources.MarioPermDeath);

        SoundPlayer Vinst = new SoundPlayer(Resources.Vinst);


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Timmer vilken upptaterar hela spelet framåt.

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
                if (TicksEfterHopp==7 || TicksEfterHopp== 8)
                {
                    gravity = -25;
                }
                else if (TicksEfterHopp == 1||TicksEfterHopp==2)
                {
                    gravity = -15;
                }
                else if (TicksEfterHopp==3||TicksEfterHopp==4||TicksEfterHopp==5||TicksEfterHopp==6)
                {
                    gravity= -20;
                }
            }
            if (gravity == 0)
            {
                TicksILuften = 0;
            }

            //Räknar ut den riktiga hastigheten efter att turbo kan ge en accelration
            Hastigheten = (int)Math.Round(Rörelse * accelration);
            //Kollar alla objekt i formuläret, Labels, Picturboxes osv
            foreach (Control x in this.Controls)
            {
                //Flyttar Bakgrunden och allt annat så det ser ut som Mario rör sig framåt
                if (x is PictureBox && (string)x.Tag != "Mario")
                {

                    if (Mario.Left>450 && Mario.Left<15500)
                    {
                        backgrundRörelse = (int)Math.Round((Hastigheten* -2.5));
                        x.Left += backgrundRörelse;
                    }

                }

            }
            //Föremålen i spelet som rör på sigs upptatering både i höjdled och sidled.
            Mario.Top += gravity;
            Mario.Left += Hastigheten;

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
            //Kontrol för hur kollisionen funkar. Om mario är nära toppen så ska han så på blocket annars ska han studsa ifrån det.
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "VägOMark")
                {

                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {

                        if (Mario.Bottom == x.Top ||Mario.Bottom == x.Top+1 || Mario.Bottom == x.Top+2 || Mario.Bottom == x.Top+3 || Mario.Bottom == x.Top+4 || Mario.Bottom == x.Top+5|| Mario.Bottom == x.Top+6 || Mario.Bottom == x.Top+7 || Mario.Bottom == x.Top+8 ||Mario.Bottom == x.Top+9 ||Mario.Bottom == x.Top+10 ||Mario.Bottom == x.Top+11 ||Mario.Bottom == x.Top+12 ||Mario.Bottom == x.Top+13 ||Mario.Bottom == x.Top+14 ||Mario.Bottom == x.Top+15 ||Mario.Bottom == x.Top+16)
                        {
                            marioGn = true;
                            TicksILuften = 0;

                        }
                        else
                        {
                            //studs ifrån
                            {

                                if (Mario.Right == x.Left+1)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-1)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+2)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-2)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+3)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-3)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+4)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-4)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+5)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-5)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+6)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-6)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+7)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-7)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+8)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-8)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+9)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-9)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+10)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-10)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+11)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-11)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+12)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-12)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+13)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-13)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+14)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-14)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+15)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-15)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+16)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-16)
                                {

                                    stuts = 32;

                                }
                                Mario.Left += stuts;
                            }
                        }
                    }
                    //kontroll för kollision mellan gumba och block. Detta sker sedan för alla gumbas
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba1.Bottom == x.Top ||Gumba1.Bottom == x.Top+1 || Gumba1.Bottom == x.Top+2 || Gumba1.Bottom == x.Top+3 || Gumba1.Bottom == x.Top+4 || Gumba1.Bottom == x.Top+5|| Gumba1.Bottom == x.Top+6 || Gumba1.Bottom == x.Top+7 || Gumba1.Bottom == x.Top+8 ||Gumba1.Bottom == x.Top+9 ||Gumba1.Bottom == x.Top+10 ||Gumba1.Bottom == x.Top+11 ||Gumba1.Bottom == x.Top+12)
                        {
                            gumbaGn = true;
                        }
                        else
                        {
                            Gumba1Riktning = Gumba1Riktning*-1;
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
                            Gumba2Riktning = Gumba2Riktning*-1;
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
                            Gumba3Riktning = Gumba3Riktning*-1;
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
                            gumba4Riktning = gumba4Riktning*-1;
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
                            Mushroom1Riktning = Mushroom1Riktning*-1;
                        }
                    }
                }

                //Kollar om Mario rör marken samt uptaterar isåfall gör den så han inte kan falla.
                if (x is PictureBox && (string)x.Tag == "Marken")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;

                    }
                    //Kollar om gumba och likande rör vid marken så att de inte ska falla genom marken, Visa block använder tagen marken istället och de kontrolleras här
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
                //kontroll för kollision mot block som bara har taggen väg
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

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-1)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+2)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-2)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+3)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-3)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+4)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-4)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+5)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-5)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+6)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-6)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+7)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-7)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+8)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-8)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+9)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-9)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+10)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-10)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+11)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-11)
                                {

                                    stuts = 32;

                                }
                                if (Mario.Right == x.Left+12)
                                {

                                    stuts = -32;

                                }
                                if (Mario.Left == x.Right-12)
                                {

                                    stuts = 32;

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
                //Pipe att man flyttas i sidled och kontroll att man inte faller igenom marken
                if (x is PictureBox && (string)x.Tag == "pipe1") //Den mario under måste sättas i en utanför pipe denna jag skriver på nu då den endast flyttar pipen annars,
                {

                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        marioGn = true;
                        TicksILuften = 0;
                        if (Mario.Bottom == x.Top)
                        {

                            if (neripipe == true)
                            {
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
                        TicksILuften = 0;
                    }
                }
                //kollision mellan gumbas så de vänder när de studsar mot varandra
                if (x is PictureBox && (string)x.Tag == "monster")
                {
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba1 != x)
                        {
                            Gumba1Riktning = Gumba1Riktning*-1;
                        }
                    }
                    if (Gumba2.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba2 != x)
                        {
                            Gumba2Riktning = Gumba2Riktning*-1;
                        }

                    }
                    if (Gumba3.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba3 != x)
                        {
                            Gumba3Riktning = Gumba3Riktning*-1;
                        }

                    }
                    if (Gumba4.Bounds.IntersectsWith(x.Bounds))
                    {
                        if (Gumba4 != x)
                        {
                            gumba4Riktning = gumba4Riktning*-1;
                        }

                    }
                }



                //Kollar om Mario faller ut ur världen och dödar honom
                if (Mario.Top>900)
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
                        Life = 3;
                        Endgame(true);
                        musik.Stop();
                        permDeathA.Play();
                        MessageBox.Show("GAME OVER");
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
                        Vinst.Play();
                        MessageBox.Show("You WIN + 1UP");
                    }

                }
                //Testar om mario rör flagstlpen och avlsutar spelet
                if (x is PictureBox && (string)x.Tag == "Flagstolpe")
                {
                    if (Mario.Bounds.IntersectsWith(x.Bounds))
                    {
                        Endgame(true);
                        Vinst.Play();
                        MessageBox.Show("You WIN");

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
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+1)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+2)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+3)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+4)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+5)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+6)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+7)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+8)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+9)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+10)
                            {
                                TicksEfterHopp = 4;
                                gravity = -48;
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
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+1)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+2)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+3)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+4)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+5)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+6)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+7)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+8)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+9)
                            {
                                TicksEfterHopp = 4;
                                gravity = -40;
                                x.Dispose();
                            }
                            else if (Mario.Bottom == x.Top+10)
                            {
                                TicksEfterHopp = 4;
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
                TicksILuften++;
                if (TicksILuften == 1)
                {
                    gravity = 2;
                }
                else if (TicksILuften == 2)
                {
                    gravity = 2;
                }
                else if (TicksILuften == 3)
                {
                    gravity = 4;
                }
                else if (TicksILuften == 4)
                {
                    gravity = 4;
                }
                else if (TicksILuften == 5)
                {
                    gravity = 4;
                }
                else if (TicksILuften == 6)
                {
                    gravity = 6;
                }
                else if (TicksILuften == 7)
                {
                    gravity = 6;
                }
                else if (TicksILuften == 8)
                {
                    gravity = 6;
                }
                else if (TicksILuften == 9)
                {
                    gravity = 6;
                }
                else if (TicksILuften == 10)
                {
                    gravity = 8;
                }
                else if (TicksILuften == 11)
                {
                    gravity = 8;
                }
                else if (TicksILuften == 12)
                {
                    gravity = 8;
                }
                else if (TicksILuften == 13)
                {
                    gravity = 8;
                }
                else if (TicksILuften == 14)
                {
                    gravity = 8;
                }
                else if (TicksILuften == 15)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 16)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 17)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 18)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 19)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 20)
                {
                    gravity = 10;
                }
                else if (TicksILuften == 21)
                {
                    gravity = 12;
                }
                else if (TicksILuften == 22)
                {
                    gravity = 12;
                }
                else if (TicksILuften == 23)
                {
                    gravity = 12;
                }
                else if (TicksILuften == 24)
                {
                    gravity = 12;
                }
                else if (TicksILuften == 25)
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

            //Kollar om gumban faller eller om den ska röra sig i sidled gäller för alla gumbas och mushrooms
            if (!gumbaGn)
            {
                GumbaGravity = 4;
                Gumbarörelse = 0;
            }
            else
            {
                GumbaGravity = 0;
                Gumbarörelse = -3*Gumba1Riktning;
            }
            if (!gumbaGn2)
            {
                GumbaGravity2 = 4;
                Gumbarörelse2 = 0;
            }
            else
            {
                GumbaGravity2 = 0;
                Gumbarörelse2 = -3*Gumba2Riktning;

            }
            if (!gumbaGn3)
            {
                GumbaGravity3 = 4;
                Gumbarörelse3 = 0;
            }
            else
            {
                GumbaGravity3 = 0;
                Gumbarörelse3 = -3*Gumba3Riktning;

            }
            if (!gumbaGn4)
            {
                GumbaGravity4 = 4;
                Gumbarörelse4 = 0;
            }
            else
            {
                GumbaGravity4 = 0;
                Gumbarörelse4 = -3*gumba4Riktning;

            }

            if (!mushroomGn)
            {
                Mushroomgravity = 4;
                Mushroomrörelse = 0;
            }
            else
            {
                Mushroomgravity = 0;
                Mushroomrörelse = -3*Mushroom1Riktning;
            }
            if (TicksEfterTeleportering >100)
            {
                timer2.Stop();
                TicksEfterTeleportering = 0;
            }

            //upptaterar antalet liv samt TicksEfterHopp-- räknar tiden efter föra hoppet, Vilket sätter en gräns så man inte kan hoppa direkt


            lbl_Lifes.Text = Life.ToString();
            TicksEfterHopp--;


        }

        //När spelet tar slut
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
        //ifall av död
        private void death(bool y)
        {
            if (mushroom!= true)
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
                    Life = 3;
                    Endgame(true);
                    musik.Stop();
                    permDeathA.Play();
                    MessageBox.Show("GAME OVER");
                    Life = 3;

                }

            }
        }
        //När spelet ska resetas till standad layout
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
        //pipe teleporteringens funktion
        private void teleport1(bool x)
        {
            foreach (Control y in this.Controls)
            {
                if (TicksEfterTeleportering == 0)
                {
                    if (y is PictureBox && (string)y.Tag != "Mario")
                    {
                        if (Mario.Left>450 && Mario.Left<15500)
                        {
                            y.Left -= 1280;
                            y.Top -= 64;
                            timer2.Start();
                        }
                    }
                }
            }
            Mario.Left += 0;
            Mario.Top += 0;
        }
        //Funktionen är buggig därför är den inaktiverad Meningen var att teleportera tillbaks
        private void teleport2(bool x)
        {
            foreach (Control y in this.Controls)
            {
                if (TicksEfterTeleportering == 0)
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
            if (e.KeyCode == Keys.Q)
            {
                Application.Exit();
            }
            if (e.KeyCode == Keys.R)
            {
                if (Life>1)
                {
                    Life--;
                    Endgame(true);
                    musik.Stop();
                    deathA.Play();
                    MessageBox.Show("Eehe ehehe ehee");

                }
                else
                {
                    Life = 3;
                    Endgame(true);
                    musik.Stop();
                    permDeathA.Play();
                    MessageBox.Show("GAME OVER");

                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                EndGame = false;
                musik.Play();
                timer1.Start();


            }

            if (e.KeyCode == Keys.Up)
            {
                if (marioGn)
                {
                    RörelseUp = true;
                    marioGn = false;
                    TicksEfterHopp = 8;
                }

            }
            if (e.KeyCode == Keys.Left)
            {
                RörelseBak = true;
            }

            if (e.KeyCode == Keys.Right)
            {
                RörelseFram = true;
            }

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

            }
            if (e.KeyCode == Keys.Down)
            {
                neripipe = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TicksEfterTeleportering++;
        }

        private void Mushroom1_Click(object sender, EventArgs e)
        {

        }

        private void World11_Load(object sender, EventArgs e)
        {

        }
    }
}