using KnightsTourApp.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourApp
{
    

    class Board
    {
        public int width { get; set; }
        public int height { get; set; }

        public int[,] fields { get; set; }

        //int[,] fields;
        //The board's dimensions
        //int width, height;
        //Initial starting position
        int startX, startY;

        public bool solved = false;
        public int attempts = 0;

        //The knight which will travel across the board
        Knight knight;

        public Stopwatch sw = new Stopwatch();
        Thread solveThread;
        MainForm parent;

        public Board(int w, int h, int sX, int sY, MainForm parent)
        {
            width = w;
            height = h;

            startX = sX;
            startY = sY;


            this.parent = parent;

            knight = new Knight(sX, sY);

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
            return ((xPos >= 0 && yPos >= 0) && (xPos < width && yPos < height)) && (fields[yPos, xPos] == 0);
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

        private Knight nextMove(Knight _knight)
        {
            //index of the smallest degree step
            int minDegIdx = -1;
            //smallest degree
            int minDeg = 9;

            int degrees, nextX, nextY;

            //Helps to randomize the steps for different results
            int randomStart = new Random().Next(knight.patternX.Length) % knight.patternX.Length;

            //iterating trough the steps
            for (int count = 0; count < knight.patternX.Length; ++count)
            {
                //minimal randomization for steps to add variety and get different results
                int i = (randomStart + count) % knight.patternX.Length;

                //coordinates of the next step
                nextX = _knight.x + knight.patternX[i];
                nextY = _knight.y + knight.patternY[i];

                //checks if the next step has a smaller degree than the current minimum
                if (isValidStep(nextX, nextY) && (degrees = getDegree(nextX, nextY)) < minDeg)
                {
                    minDegIdx = i;
                    minDeg = degrees;
                }
            }

            //if did not found a suitable step, return null
            if (minDegIdx == -1) return null;

            //if found a suitable step, set the upcoming step's value to the minimal degree step
            nextX = _knight.x + knight.patternX[minDegIdx];
            nextY = _knight.y + knight.patternY[minDegIdx];

            //sets the boards value to the upcoming value at the coords' of the next step
            fields[nextY, nextX] = fields[_knight.y, _knight.x] + 1;

            //sets the current cell's coords to the next cell's coords
            _knight.x = nextX;
            _knight.y = nextY;

            return _knight;
        }

        private bool neighbour()
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
            fields = initBoard(width, height);
            //Sets the starting position to 1
            fields[startY, startX] = 1;

            //Moves the Knight to the starting position
            knight.x = startX;
            knight.y = startY;

            //Moves trough every field except the initial cell and the removed corners (-5)
            for (int i = 0; i < width * height - 5; ++i)
                if (nextMove(knight) == null) return false;

            //After it is done, checks if it is a closed route or not
            return neighbour();
        }

        private void solve()
        {
            parent.Invoke(new Action(() =>
            {
                parent.addText("Solving Started...\n");
            }));

            sw.Start();
            //Tries to find a route
            while (!findRoute()) { }
            sw.Stop();

            //If it is done, mark the board as solved
            solved = true;

            parent.Invoke((MethodInvoker) delegate 
            { 
                parent.addText(string.Format($"Solved in {attempts} attempts, under ~{(double)(sw.ElapsedTicks / 10000.0)} ms\n"));
                parent.changeSolve("S H O W   S O L U T I O N", true);
            });

            solveThread.Abort();
        }

        public void getSolution()
        {
            attempts = 0;
            sw.Reset();
            solveThread = new Thread(solve);
            solveThread.Start();
        }

        public void cancelSolving()
        {
            if (solveThread.IsAlive) solveThread.Abort();
        }
    }
}
