using KnightsTourApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourApp
{
    public partial class MainForm : Form
    {
        /*------------------------------------------------------*/
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner 
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        /*------------------------------------------------------*/

        Board chessboard;
        PictureBox knight;
        Thread drawingThread;

        public MainForm()
        {
            InitializeComponent();

            applyRounding();

            logBox.AppendText(getTime() + " Program started...\n");

            solveButton.Text = "S O L V I N G . . .";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            initChessboard();

            chessboard = new Board(8, 8, 1, 0, this);

            drawingThread = new Thread(moveKnight);
        }

        private void applyRounding()
        {
            //MainForm
            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //Buttons
            quitButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, quitButton.Width, quitButton.Height, 35, 35));
            solveButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, solveButton.Width, solveButton.Height, 35, 35));
            resetButton.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, resetButton.Width, resetButton.Height, 35, 35));
        }

        private string getTime()
        {
            return "[" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "]";
        }



        private void initChessboard()
        {
            int tileSize = 80;
            int gridSize = 8;
            
            chessboardContainer.Controls.Clear();

            knight = new PictureBox
            {
                Image = Resources.knight,
                Location = new Point(30 + 1 * tileSize + 15, 30 + 0 * tileSize + 15),
                Size = new Size(Resources.knight.Width, Resources.knight.Height),
                BackColor = Color.Transparent
            };

            knight.Parent = chessboardContainer;
            //chessboardContainer.Controls.Add(knight);


            for (var x = 0; x < gridSize; x++)
            {
                for (var y = 0; y < gridSize; y++)
                {
                    var cellImage = new PictureBox
                    {
                        Image = y % 2 == 0 ? (x % 2 != 0 ? Resources.cell1 : Resources.cell2) : (x % 2 != 0 ? Resources.cell2 : Resources.cell1),
                        Location = new Point(30 + tileSize * x, 30 + tileSize * y),
                        Size = new Size(tileSize, tileSize)
                    };

                    if (!(x == 0 && y == 0 || x == 7 && y == 0 || x == 0 && y == 7 || x == 7 && y == 7))
                        chessboardContainer.Controls.Add(cellImage);

                    if (x == 0)
                    {
                        Label numLbl = new Label
                        {
                            Text = (8 - y).ToString(),
                            Location = new Point(5, 30 + y * tileSize + tileSize / 2 - 40),
                            BackColor = Color.Transparent,
                            ForeColor = y % 2 == 0 ? Color.FromArgb(251, 85, 26) : Color.FromArgb(112, 246, 209),
                            Size = new Size(20, 80),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Impact", 16, FontStyle.Regular, GraphicsUnit.Pixel)
                        };

                        chessboardContainer.Controls.Add(numLbl);
                    }
                }

                Label charLbl = new Label
                {
                    Text = Convert.ToChar(65 + x).ToString(),
                    Location = new Point(30 + x * tileSize + tileSize / 2 - 40, 5),
                    BackColor = Color.Transparent,
                    ForeColor = x % 2 == 0 ? Color.FromArgb(251, 85, 26) : Color.FromArgb(112, 246, 209),
                    Size = new Size(80, 20),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Impact", 16, FontStyle.Regular, GraphicsUnit.Pixel)
                };

                chessboardContainer.Controls.Add(charLbl);
            }
        }

        

        public void addText(string str)
        {
            logBox.AppendText(getTime() + " " + str);
            logBox.ScrollToCaret();
        }

        public void changeSolve(string str, bool enabled)
        {
            solveButton.Text = str;
            solveButton.Enabled = enabled;
            resetButton.Enabled = enabled;
        }



        private void resetButton_Click(object sender, EventArgs e)
        {
            solveButton.Enabled = resetButton.Enabled = false;
            solveButton.Text = "S O L V I N G . . .";
            initChessboard();

            if (drawingThread.IsAlive)
            {
                try
                {
                    drawingThread.Resume();
                }
                catch { }
                finally
                {
                    drawingThread.Abort();
                    drawingThread = new Thread(moveKnight);
                }
            }

            chessboard.getSolution();
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            if (solveButton.Text == "S H O W   S O L U T I O N" || solveButton.Text == "R E S U M E")
            {
                solveButton.Text = "P A U S E";

                if (drawingThread.IsAlive)
                { 
                    drawingThread.Resume();
                }
                else
                {
                    drawingThread = new Thread(moveKnight);
                    drawingThread.Start();
                }
            }
            else
            { 
                solveButton.Text = "R E S U M E";
                drawingThread.Suspend();
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            chessboard.cancelSolving();

            if (drawingThread.IsAlive)
            {
                try
                {
                    drawingThread.Resume();
                }
                catch { }
                finally
                {
                    drawingThread.Abort();
                }
            }    

            Close();
        }

        private void moveKnight()
        {
            int[] returned;
            int speedModifier = 100;
            
            for (int current = 1; current <= chessboard.width * chessboard.height - 4; ++current)
            {
                returned = findStep(current);

                Invoke((MethodInvoker) delegate
                {
                    Label _lbl = new Label
                    {
                        Text = current.ToString(),
                        AutoSize = false,
                        Size = new Size(60, 60),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(30 + returned[0] * 80 + 10, 30 + returned[1] * 80 + 10),
                        ForeColor = returned[1] % 2 == 0 ? (returned[0] % 2 != 0 ? Color.FromArgb(112, 246, 209) : Color.FromArgb(251, 85, 26)) : (returned[0] % 2 != 0 ? Color.FromArgb(251, 85, 26) : Color.FromArgb(112, 246, 209)),
                        Font = new Font("Impact", 20, FontStyle.Regular, GraphicsUnit.Pixel)
                    };

                    chessboardContainer.Controls.Add(_lbl);
                    chessboardContainer.Controls[chessboardContainer.Controls.Count - 1].BringToFront();
                    knight.BringToFront();

                    knight.Location = new Point(30 + returned[0] * 80 + 15, 30 + returned[1] * 80 + 15);
                    speedModifier = speedModifierBar.Value;
                    addText($"Knight jumped to {Convert.ToChar(65 + returned[0])}{8 - returned[1]}\n");
                });

                Thread.Sleep(1100 - speedModifier);

                if (current == chessboard.width * chessboard.height - 4)
                {
                    returned = findStep(1);

                    Invoke((MethodInvoker)delegate
                    {
                        knight.Location = new Point(30 + returned[0] * 80 + 15, 30 + returned[1] * 80 + 15);

                        addText($"Knight returned to {Convert.ToChar(65 + returned[0])}{8 - returned[1]}\n");

                        solveButton.Text = "D O N E";
                        solveButton.Enabled = false;
                    });
                }
            }

            drawingThread.Abort();
        }

        private int[] findStep(int num)
        {
            int xPos = -1;
            int yPos = -1;
            
            for (int y = 0; y < chessboard.height; ++y)
                for (int x = 0; x < chessboard.width; ++x)
                    if (chessboard.fields[y, x] == num)
                    {
                        xPos = x;
                        yPos = y;
                    }

            return new int[] {xPos, yPos};
        }

        private void speedModifierBar_ValueChanged(object sender, EventArgs e)
        {
            speedLabel.Text = string.Format($"Speed: {1100 - speedModifierBar.Value} ms");
        }
    }
}
