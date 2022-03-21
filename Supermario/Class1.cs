using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermario
{
    internal class Class1
    {


        private int x = 0;
        private int y = 0;
        private int radie = 0;


        public Class1()
        {
            // this.game = game;
            this.y = y;
            this.radie = radie < 0 ? -radie : radie;
        }
        #region Egenskaper
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int Radie
        {
            get { return radie; }
            set { if (value < 0) radie = -value; else radie = value; }
        }
        #endregion

    }
}
