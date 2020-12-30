using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KnightsTourApp
{
    class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] fields { get; set; }

        //Initial starting position
        int startX, startY;
        int attempts = 0;

        //The knight which will travel across the board
        public Knight knight { get;}
        public Stopwatch stopwatch = new Stopwatch();

        private MainForm parent;

        public Board(int w, int h, int sx, int sy, MainForm parent)
        {
            Width = w;
            Height = h;

            startX = sx;
            startY = sy;

            this.parent = parent;

            knight = new Knight(startX, startY);

            getSolution();
        }

        private int[,] initBoard(int width, int height)
        {
            //Initialize a board with 0-s
            int[,] _board = new int[height, width];

            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                    _board[y, x] = 0;

            //Remove corners
            _board[0, 0] = _board[0, width - 1] = _board[height - 1, 0] = _board[height - 1, width - 1] = -1;

            return _board;
        }

        private bool isValidStep(int xPos, int yPos)
        {
            //Checks if the given cell is visited or not, also checks if it is inside the board
            return ((xPos >= 0 && yPos >= 0) && (xPos < Width && yPos < Height)) && (fields[yPos, xPos] == 0);
            //     |                    Boundary check                         |    |   field value check   |
        }

        private int getDegree(int xPos, int yPos)
        {
            int count = 0;

            //Counts the unvisited cells around it using all the steps
            for (int i = 0; i < knight.patternX.Length; ++i)
                if (isValidStep(xPos + knight.patternX[i], yPos + knight.patternY[i]))
                    count++;

            return count;
        }

        private bool nextMove()
        {
            //index of the smallest degree step
            int minDegIdx = -1;
            //smallest degree
            int minDeg = 9;

            int degrees, nextX, nextY;

            int startRandom = new Random().Next(8);

            //iterating trough the steps
            for (int count = 0; count < knight.patternX.Length; ++count)
            {
                //minimal randomization for steps to add variety and get different results
                int i = (startRandom + count) % knight.patternX.Length;

                //coordinates of the next step
                nextX = knight.x + knight.patternX[i];
                nextY = knight.y + knight.patternY[i];

                //checks if the next step has a smaller degree than the current minimum
                if (isValidStep(nextX, nextY) && (degrees = getDegree(nextX, nextY)) < minDeg)
                {
                    minDegIdx = i;
                    minDeg = degrees;
                }
            }

            //if did not found a suitable step, return false
            if (minDegIdx == -1) return false;

            //if found a suitable step, set the upcoming step's value to the minimal degree step
            nextX = knight.x + knight.patternX[minDegIdx];
            nextY = knight.y + knight.patternY[minDegIdx];

            //sets the boards value to the upcoming value at the coords' of the next step
            fields[nextY, nextX] = fields[knight.y, knight.x] + 1;

            //sets the current cell's coords to the next cell's coords
            knight.x = nextX;
            knight.y = nextY;

            return true;
        }

        private bool isClosedPath()
        {
            //Checks if the knight can return to its starting position
            for (int i = 0; i < knight.patternX.Length; ++i)
                if ((knight.x + knight.patternX[i]) == startX && (knight.y + knight.patternY[i]) == startY)
                    return true;

            return false;
        }

        private bool findRoute()
        {
            attempts++;

            //Initializes the board at every attempt
            fields = initBoard(Width, Height);
            //Sets the starting position to 1
            fields[startY, startX] = 1;

            //Moves the Knight to the starting position
            knight.x = startX;
            knight.y = startY;

            //Moves trough every field except the initial cell and the removed corners (-5)
            for (int i = 0; i < Width * Height - 5; ++i)
                if (!nextMove()) return false;

            //After it is done, checks if it is a closed route or not
            return isClosedPath();
        }

        private void solve()
        {
            attempts = 0;

            parent.Invoke(new Action(() => { parent.addText("Solving Started..."); }));

            //Tries to find a route
            stopwatch.Restart();

            while (!findRoute()) {}

            stopwatch.Stop();

            parent.Invoke(new Action(() =>
            { 
                parent.addText($"Solved in {attempts} attempts, under ~{stopwatch.ElapsedTicks / 10000.0} ms");
                parent.modifyButtons("S H O W   S O L U T I O N", true, true, true);
            }));
        }

        public void getSolution()
        {
            Task.Run(() => solve());
        }
    }
}
