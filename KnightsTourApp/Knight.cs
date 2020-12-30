using KnightsTourApp.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace KnightsTourApp
{
    class Knight
    {
        public PictureBox Image { get; set; }
        public int Current { get; set; }

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
            Current = 1;

            Image = new PictureBox
            {
                Image = Resources.knight,
                Size = new Size(Resources.knight.Width, Resources.knight.Height),
            };
        }
    }
}
