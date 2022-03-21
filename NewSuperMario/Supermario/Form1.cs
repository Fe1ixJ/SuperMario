using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Media;


namespace Supermario
{// när man går av blocket så får man inte flying state
    //Så att bakgrunden rör sig
    //Gameover ger död och startar om programmet igen.
    //när man dör öppna ett nytt form som vissar ens mängd liv och att det blir mindre :(

    public partial class Form1 : Form
    {
        private Class1 game = new Class1();

        public Form1()
        {
            InitializeComponent();

        }

        int gravity = 16;
        int Rörelse = 0;
        int Gumbarörelse = -4;
        int GumbaGravity = 16;
        int backgrundRörelse = 0;
        int totalBackgroundRörelse = 0;

        bool MarioFlying = true;
        bool Gumba1Flying = true;
        bool turbo = false;
        double accelration = 1;

        int temporär;
        bool EndGame;
        public static int Life = 5;

        SoundPlayer musik = new SoundPlayer();
        SoundPlayer effekt = new SoundPlayer();

        // public static musiken test, Overworld, Titlescreen, Worldselector;



        //string[] musiklista = { test, Overworld, Titlescreen, Worldselector };









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
            Gumba1.Top += GumbaGravity;
            Gumba1.Left += Gumbarörelse;






            foreach (Control z in this.Controls)
            {
                //Varför funkar det inte
                if (z is PictureBox && (string)z.Tag != "Mario")
                {
                    if (Mario.Left>pictureBox11.Left && Mario.Left<pictureBox20.Left)
                    {
                        backgrundRörelse = (temporär * -1);
                        if (backgrundRörelse < 0)
                            z.Left += backgrundRörelse;





                    }




                }



            }


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
                    if (Gumba1.Bounds.IntersectsWith(x.Bounds))
                    {

                        Gumba1Flying = false;
                        Gumba1.Top=x.Top - Gumba1.Height;
                        /*
                        if (Gumbarörelse<0)
                        { Gumbarörelse = -6; }
                        else
                        { Gumbarörelse = 6; } */

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
                        //mario top vad som händer(Det under är för top)
                    }

                    else
                    {

                        // gravity = 4;

                    }
                }
                else
                    gravity = 8;

                if (Mario.Top>900)
                {
                    death(true);

                }
                if (Mario.Left<=0)
                {
                    Mario.Left = 0;

                }

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
            /* foreach (Control å in this.Controls)
                 foreach (Control ä in this.Controls)
                 {
                     if (å is PictureBox && (string)å.Tag == "Gumba2")
                     {
                         Gumbarörelse = -1;
                         å.Left += Gumbarörelse;
                         å.Top += GumbaGravity;

                         if (ä is PictureBox && (string)ä.Tag == "Marken")
                         {
                             if (Gumba1.Bounds.IntersectsWith(ä.Bounds))
                             {
                                 if (Gumbarörelse<0) Gumbarörelse=1;
                                 else Gumbarörelse = -1;
                                 GumbaGravity = 0;
                                 å.Top=ä.Top - å.Height;
                             }
                         }



                         if (Mario.Bounds.IntersectsWith(å.Bounds))
                         {
                             // På vänstersida höger sida eller underifrån
                             //death(true);

                         }


                     }


                 } */



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

            Form1 frm1 = new Form1();
            Form2 frm2 = new Form2();
            Form3 frm3 = new Form3();

            this.Hide();
            frm1.Show();

        }
        /*        private void Gumba(bool x)
              {
                  foreach (Control å in this.Controls)
                  {
                      if (å is PictureBox && (string)å.Tag == "Gumba")
                      {
                          if (Mario.Bounds.IntersectsWith(å.Bounds))
                          {
                              // På vänstersida höger sida eller underifrån


                          }

                      }


                  }
              } */


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

        private void Bakgrund_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {

        }

        private void Mario_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
    }
}