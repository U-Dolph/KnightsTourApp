using KnightsTourApp.Properties;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnightsTourApp
{
    public partial class MainForm : Form
    {
        //https://stackoverflow.com/questions/18822067/rounded-corners-in-c-sharp-windows-forms
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

        Board   chessboard;
        bool    paused;

        public MainForm()
        {
            InitializeComponent();
            applyRounding();

            addText("Application started...");
            modifyButtons("S O L V I N G . . .", false, false, true);
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            chessboard = new Board(8, 8, 1, 0, this);
            initChessboardUI();

            speedLabel.Text = string.Format($"SPEED: {1100 - speedModifierBar.Value}ms");

            Task.Run(() => moveKnight());
        }

        
        public void addText(string str)
        {
            logBox.AppendText($"[{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}] {str}\n");
            logBox.ScrollToCaret();
        }

        public void modifyButtons(string str, bool solveEnabled, bool resetEnabled, bool isPaused)
        {
            solveButton.Text = str;

            solveButton.Enabled = solveEnabled;
            resetButton.Enabled = resetEnabled;

            paused = isPaused;
        }

        private void applyRounding()
        {
            //MainForm
            FormBorderStyle = FormBorderStyle.None;
            Region          = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            //Buttons
            solveButton.Region  = Region.FromHrgn(CreateRoundRectRgn(0, 0, solveButton.Width, solveButton.Height, solveButton.Height, solveButton.Height));
            resetButton.Region  = Region.FromHrgn(CreateRoundRectRgn(0, 0, resetButton.Width, resetButton.Height, resetButton.Height, resetButton.Height));
            quitButton.Region   = Region.FromHrgn(CreateRoundRectRgn(0, 0, quitButton.Width, quitButton.Height, quitButton.Height, quitButton.Height));
        }

        private void initChessboardUI()
        {
            int tileSize = 80;
            int gridSize = 8;

            //Positions the knight to its initial place
            chessboard.knight.Image.Location = new Point(30 + chessboard.knight.x * tileSize + 15, 30 + chessboard.knight.y * tileSize + 15);
            chessboard.knight.Image.Parent = chessboardContainer;

            for (var x = 0; x < gridSize; x++)
            {
                for (var y = 0; y < gridSize; y++)
                {
                    //Chosing which cell image to use (orange | cyan)
                    var cellImage = new PictureBox
                    {
                        Image = y % 2 == 0 ? (x % 2 != 0 ? Resources.cell1 : Resources.cell2) : (x % 2 != 0 ? Resources.cell2 : Resources.cell1),
                        Location = new Point(30 + tileSize * x, 30 + tileSize * y),
                        Size = new Size(tileSize, tileSize)
                    };

                    if (!(x == 0 && y == 0 || x == 7 && y == 0 || x == 0 && y == 7 || x == 7 && y == 7))
                        chessboardContainer.Controls.Add(cellImage);

                    //Top row number labels
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

                //Left side collumn letter labels
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



        private void moveKnight()
        {
            int[] returned;
            int speedModifier = 100;

            while (true)
                if (!paused)
                {
                    returned = findStep(chessboard.knight.Current == chessboard.Width * chessboard.Height - 3? 1 : chessboard.knight.Current);

                    Invoke(new Action(() =>
                    {
                        Label _lbl = new Label
                        {
                            Text = chessboard.knight.Current.ToString(),
                            AutoSize = false,
                            Size = new Size(60, 60),
                            TextAlign = ContentAlignment.MiddleCenter,
                            Location = new Point(30 + returned[0] * 80 + 10, 30 + returned[1] * 80 + 10),
                            ForeColor = returned[1] % 2 == 0 ? (returned[0] % 2 != 0 ? Color.FromArgb(112, 246, 209) : Color.FromArgb(251, 85, 26)) : (returned[0] % 2 != 0 ? Color.FromArgb(251, 85, 26) : Color.FromArgb(112, 246, 209)),
                            Font = new Font("Impact", 20, FontStyle.Regular, GraphicsUnit.Pixel),
                            Name = "Position Display"
                        };

                        chessboardContainer.Controls.Add(_lbl);
                        chessboardContainer.Controls[chessboardContainer.Controls.Count - 1].BringToFront();

                        chessboard.knight.Image.BringToFront();
                        chessboard.knight.Image.Location = new Point(30 + returned[0] * 80 + 15, 30 + returned[1] * 80 + 15);

                        speedModifier = speedModifierBar.Value;
                        addText(string.Format(chessboard.knight.Current == chessboard.Width * chessboard.Height - 3 ? "Knight returned to " : "Knight jumped to ") + $"{Convert.ToChar(65 + returned[0])}{8 - returned[1]}");

                        if (chessboard.knight.Current == chessboard.Width * chessboard.Height - 3)
                            modifyButtons("D O N E", false, true, true);
                    }));

                    Thread.Sleep(1100 - speedModifier);

                    ++chessboard.knight.Current;
                }
        }

        private int[] findStep(int num)
        {
            for (int y = 0; y < chessboard.Height; ++y)
                for (int x = 0; x < chessboard.Width; ++x)
                    if (chessboard.fields[y, x] == num)
                        return new int[] { x, y };

            return null;
        }


        private void resetButton_Click(object sender, EventArgs e)
        {
            chessboard.getSolution();

            modifyButtons("S O L V I N G . . .", false, false, true);

            //Remove every step-counting label
            for (int i = chessboardContainer.Controls.Count - 1; i >= 0; --i)
                if (chessboardContainer.Controls[i].Name == "Position Display")
                    chessboardContainer.Controls.RemoveAt(i);

            int[] startPos = findStep(1);

            chessboard.knight.Image.Location = new Point(30 + startPos[0] * 80 + 15, 30 + startPos[1] * 80 + 15);
            chessboard.knight.Current = 1;
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            if (solveButton.Text == "S H O W   S O L U T I O N" || solveButton.Text == "R E S U M E")
                modifyButtons("P A U S E", true, false, false);
            else
                modifyButtons("R E S U M E", true, true, true);
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void speedModifierBar_ValueChanged(object sender, EventArgs e)
        {
            speedLabel.Text = string.Format($"SPEED: {1100 - speedModifierBar.Value}ms");
        }
    }
}
