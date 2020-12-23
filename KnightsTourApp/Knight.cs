using KnightsTourApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourApp
{
    class Knight
    {
        //The location of the knight
        public int x;
        public int y;
        //The step patterns of the knight according to the rules of chess
        public int[] patternX = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        public int[] patternY = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

        public Knight(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
