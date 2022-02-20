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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mario.Top += gravity;
            Mario.Left += Rörelse;
            // Det sitter som om testbox2s botten == mario botten så faller den inte VET INTE RIKITGT. Fassnar på testblox2 och faller igenom testblox 
            // Kan tvinga ut någon om de är i ett block 
            {//testblock 

                if (Mario.Left+64 >= TestBox.Left||Mario.Left-64<=TestBox.Left)
                {
                    if (Mario.Bottom == TestBox.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= TestBox.Left||Mario.Left+64<=TestBox.Left)
                {
                    if (Mario.Bottom == TestBox.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == TestBox.Bottom))
                {
                    if (Mario.Left+64 == TestBox.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == TestBox.Left+64)
                    { Rörelse = 0; }
                }



                /* if (Mario.Bottom == Marken1.Top)
                {
                    gravity = 0;
                    MarioFlying = false;
                    //timer2.Stop();
                } */

            }
            {//Marken1 

                if (Mario.Left+64 >= Marken1.Left||Mario.Left-64<=Marken1.Left)
                {
                    if (Mario.Bottom == Marken1.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= Marken1.Left||Mario.Left+64<=Marken1.Left)
                {
                    if (Mario.Bottom == Marken1.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == Marken1.Bottom))
                {
                    if (Mario.Left+64 == Marken1.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == Marken1.Left+64)
                    { Rörelse = 0; }
                }

            }
            {//Marken2

                if (Mario.Left+64 >= Marken2.Left||Mario.Left-64<=Marken2.Left)
                {
                    if (Mario.Bottom == Marken2.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= Marken2.Left||Mario.Left+64<=Marken2.Left)
                {
                    if (Mario.Bottom == TestBox.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == Marken2.Bottom))
                {
                    if (Mario.Left+64 == Marken2.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == Marken2.Left+64)
                    { Rörelse = 0; }
                }

            }
            {//Marken3

                if (Mario.Left+64 >= Marken3.Left||Mario.Left-64<=Marken3.Left)
                {
                    if (Mario.Bottom == Marken3.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= Marken3.Left||Mario.Left+64<=Marken3.Left)
                {
                    if (Mario.Bottom == Marken3.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == Marken3.Bottom))
                {
                    if (Mario.Left+64 == Marken3.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == Marken3.Left+64)
                    { Rörelse = 0; }
                }

            }
            {//marken4 

                if (Mario.Left+64 >= Marken4.Left||Mario.Left-64<=Marken4.Left)
                {
                    if (Mario.Bottom == Marken4.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= Marken4.Left||Mario.Left+64<=Marken4.Left)
                {
                    if (Mario.Bottom == Marken4.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == Marken4.Bottom))
                {
                    if (Mario.Left+64 == Marken4.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == Marken4.Left+64)
                    { Rörelse = 0; }
                }

            }
            {//Marken5 

                if (Mario.Left+64 >= Marken5.Left||Mario.Left-64<=Marken5.Left)
                {
                    if (Mario.Bottom == Marken5.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= Marken5.Left||Mario.Left+64<=Marken5.Left)
                {
                    if (Mario.Bottom == Marken5.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == Marken5.Bottom))
                {
                    if (Mario.Left+64 == Marken5.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == Marken5.Left+64)
                    { Rörelse = 0; }
                }

            }
            {//testblock2 

                if (Mario.Left+64 >= TestBox2.Left||Mario.Left-64<=TestBox2.Left)
                {
                    if (Mario.Bottom == TestBox2.Top)
                    {
                        gravity = 0;
                        MarioFlying = false;
                    }
                }
                if (Mario.Left-64 >= TestBox2.Left||Mario.Left+64<=TestBox2.Left)
                {
                    if (Mario.Bottom == TestBox2.Top)
                    {
                        gravity = 16;
                        MarioFlying = true;
                    }
                }
                if ((Mario.Bottom == TestBox2.Bottom))
                {
                    if (Mario.Left+64 == TestBox2.Left)
                    { Rörelse = 0; }
                    else if (Mario.Left == TestBox2.Left+64)
                    { Rörelse = 0; }
                }



                /* if (Mario.Bottom == Marken1.Top)
                {
                    gravity = 0;
                    MarioFlying = false;
                    //timer2.Stop();
                } */

            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                timer1.Start();

            if (e.KeyCode == Keys.Up)
            {
                if (!MarioFlying)
                {
                    gravity = -128;
                    MarioFlying = true;

                }

            }
            if (MarioFlying == true)
            {
                if (e.KeyCode == Keys.Left)
                    Rörelse = -8;
                if (e.KeyCode == Keys.Right)
                    Rörelse = 8;
            }
            // Ovan är en tempoär fix
            {//testblock
             // if mario emellan bottom och top
                if (Mario.Bottom == TestBox.Bottom)
                {
                    if (Mario.Left+64 == TestBox.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == TestBox.Left+64)
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
            {//Marken1
             // if mario emellan bottom och top
                if (Mario.Bottom == Marken1.Bottom)
                {
                    if (Mario.Left+64 == Marken1.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == Marken1.Left+64)
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
            {//Marken2
             // if mario emellan bottom och top
                if (Mario.Bottom == Marken2.Bottom)
                {
                    if (Mario.Left+64 == Marken2.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == Marken2.Left+64)
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
            {//Marken3
             // if mario emellan bottom och top
                if (Mario.Bottom == Marken3.Bottom)
                {
                    if (Mario.Left+64 == Marken3.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == Marken3.Left+64)
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
            {//Marken4
             // if mario emellan bottom och top
                if (Mario.Bottom == Marken4.Bottom)
                {
                    if (Mario.Left+64 == Marken4.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == Marken4.Left+64)
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
            {//Marken5
             // if mario emellan bottom och top
                if (Mario.Bottom == Marken5.Bottom)
                {
                    if (Mario.Left+64 == Marken5.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == Marken5.Left+64)
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
            {//testblock2
             // if mario emellan bottom och top
                if (Mario.Bottom == TestBox2.Bottom)
                {
                    if (Mario.Left+64 == TestBox2.Left)
                    {
                        if (e.KeyCode == Keys.Left)
                            Rörelse = -8;
                        if (e.KeyCode == Keys.Right)
                            Rörelse = 0;
                    }
                    else if (Mario.Left == TestBox2.Left+64)
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

        private void TestBox_Click(object sender, EventArgs e)
        {

        }

        private void Mario_Click(object sender, EventArgs e)
        {

        }

        private void Bakgrund_Click(object sender, EventArgs e)
        {

        }

        private void Marken2_Click(object sender, EventArgs e)
        {

        }
    }
}
