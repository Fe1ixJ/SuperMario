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
        int ticks = 0;
        bool MarioFlying = true;
        private const int NumberOfBlocks = 6;
        private PictureBox[] images = new PictureBox[NumberOfBlocks];
        bool turbo = false;
        double accelration = 1;
        int temporär2;
        int temporär;



        public void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            // if den till x.left = mario.right dont do that I if loppen och sedan har.top efter 2 andra if satser
            if (!MarioFlying)
            {
                gravity = -128;
                MarioFlying = true;
            }
            MessageBox.Show("Lol");
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

                        //   gravity = 4;

                    }

                }
                else
                    gravity = 8;



            }
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && (string)y.Tag == "Flagstolpe")
                {
                    if (Mario.Bounds.IntersectsWith(y.Bounds))
                    {
                        MessageBox.Show("You WIN");
                        //endgame;
                    }


                }
            }
            foreach (Control z in this.Controls)
            {
                if (z is PictureBox && (string)z.Tag == "Flagstolpsknop")
                {
                    if (Mario.Bounds.IntersectsWith(z.Bounds))
                    {
                        MessageBox.Show("You WIN + 1UP");
                        //endgame;
                    }


                }





            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
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
            if (e.KeyCode == Keys.Up)
            {



                // if (gravity != 0)
                //   gravity = 8;

            }

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

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {

        }
    }
}
