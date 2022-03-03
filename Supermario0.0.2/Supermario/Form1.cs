namespace Supermario
{
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



        public void Form1_Load(object sender, EventArgs e)
        {
            images[0] = TestBox;
            images[1] = Marken1;
            images[2] = Marken2;
            images[3] = Marken3;
            images[4] = Marken4;
            images[5] = Marken5;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ticks++;

            Mario.Top += gravity;
            Mario.Left += Rörelse;
            // Det sitter som om testbox2s botten == mario botten så faller den inte VET INTE RIKITGT. Fassnar på testblox2 och faller igenom testblox 
            // Kan tvinga ut någon om de är i ett block 
            Console.WriteLine("tick funkar");
            for (int i = 0; i < NumberOfBlocks; i++)
            {
                if (Mario.Bottom == images[i].Top)
                {
                    if (Mario.Left+64 >= images[i].Left && Mario.Left <=images[i].Left+64)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                    else
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }


                }
                if (Mario.Bottom >=images[i].Bottom && Mario.Bottom <= images[i].Bottom +64)
                {
                    if (Mario.Left+64 == images[i].Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == images[i].Left+64)
                    { Rörelse = 0; }
                }
                +-
                string test = i.ToString();
                MessageBox.Show(test);
            }


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                timer1.Start();
                Console.WriteLine("Funkar ens detta");
            }


            if (e.KeyCode == Keys.Up)
            {
                if (!MarioFlying)
                {
                    gravity = -128;
                    MarioFlying = true;
                }

            }

            // Ovan är en tempoär fix
            {//testblock
             // if mario emellan bottom och top

                for (int i = 0; i < NumberOfBlocks; i++)
                {
                    if (Mario.Bottom >= images[i].Bottom && Mario.Bottom < images[i].Bottom+64)
                    {
                        if (Mario.Left+64 == images[i].Left)
                        {
                            if (e.KeyCode == Keys.Left)
                                Rörelse = -8;
                            if (e.KeyCode == Keys.Right)
                                Rörelse = 0;
                        }
                        else if (Mario.Left == images[i].Left+64)
                        {
                            if (e.KeyCode == Keys.Left)
                                Rörelse = 0;
                            if (e.KeyCode == Keys.Right)
                                Rörelse = 8;
                        }
                        else
                        {
                            if (e.KeyCode == Keys.Left)
                                Rörelse = -8;
                            if (e.KeyCode == Keys.Right)
                                Rörelse = 8;
                        }
                    }
                    else
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 8;
                    }

                }



            }


        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (gravity != 0)
                    gravity = 8;

            }

            if (e.KeyCode == Keys.Left)
                Rörelse = 0;
            if (e.KeyCode == Keys.Right)
                Rörelse = 0;
        }

    }
}
