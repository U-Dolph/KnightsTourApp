
namespace KnightsTourApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.titleImage = new System.Windows.Forms.PictureBox();
            this.chessboardContainer = new System.Windows.Forms.Panel();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.speedModifierBar = new System.Windows.Forms.TrackBar();
            this.resetButton = new CustomButton.gradientButton();
            this.solveButton = new CustomButton.gradientButton();
            this.quitButton = new CustomButton.gradientButton();
            this.speedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedModifierBar)).BeginInit();
            this.SuspendLayout();
            // 
            // titleImage
            // 
            this.titleImage.Image = global::KnightsTourApp.Properties.Resources.title;
            this.titleImage.Location = new System.Drawing.Point(0, 0);
            this.titleImage.Name = "titleImage";
            this.titleImage.Size = new System.Drawing.Size(300, 100);
            this.titleImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.titleImage.TabIndex = 0;
            this.titleImage.TabStop = false;
            // 
            // chessboardContainer
            // 
            this.chessboardContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.chessboardContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.chessboardContainer.Location = new System.Drawing.Point(299, 0);
            this.chessboardContainer.Name = "chessboardContainer";
            this.chessboardContainer.Size = new System.Drawing.Size(680, 681);
            this.chessboardContainer.TabIndex = 1;
            // 
            // logBox
            // 
            this.logBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.logBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logBox.ForeColor = System.Drawing.Color.White;
            this.logBox.Location = new System.Drawing.Point(10, 425);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(280, 200);
            this.logBox.TabIndex = 3;
            this.logBox.Text = "";
            // 
            // speedModifierBar
            // 
            this.speedModifierBar.LargeChange = 100;
            this.speedModifierBar.Location = new System.Drawing.Point(10, 335);
            this.speedModifierBar.Maximum = 1090;
            this.speedModifierBar.Minimum = 100;
            this.speedModifierBar.Name = "speedModifierBar";
            this.speedModifierBar.Size = new System.Drawing.Size(280, 45);
            this.speedModifierBar.SmallChange = 50;
            this.speedModifierBar.TabIndex = 4;
            this.speedModifierBar.TickFrequency = 1090;
            this.speedModifierBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.speedModifierBar.Value = 100;
            this.speedModifierBar.ValueChanged += new System.EventHandler(this.speedModifierBar_ValueChanged);
            // 
            // resetButton
            // 
            this.resetButton.baseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(142)))));
            this.resetButton.baseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(239)))), ((int)(((byte)(125)))));
            this.resetButton.clickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(204)))));
            this.resetButton.clickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.resetButton.disabledColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.resetButton.disabledColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.resetButton.Enabled = false;
            this.resetButton.hoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(9)))), ((int)(((byte)(121)))));
            this.resetButton.hoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.resetButton.Location = new System.Drawing.Point(10, 290);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(280, 35);
            this.resetButton.TabIndex = 7;
            this.resetButton.Text = "R E S E T";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // solveButton
            // 
            this.solveButton.baseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(142)))));
            this.solveButton.baseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(239)))), ((int)(((byte)(125)))));
            this.solveButton.clickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(204)))));
            this.solveButton.clickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.solveButton.disabledColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.solveButton.disabledColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.solveButton.Enabled = false;
            this.solveButton.hoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(9)))), ((int)(((byte)(121)))));
            this.solveButton.hoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.solveButton.Location = new System.Drawing.Point(10, 245);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(280, 35);
            this.solveButton.TabIndex = 5;
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.baseColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(153)))), ((int)(((byte)(142)))));
            this.quitButton.baseColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(239)))), ((int)(((byte)(125)))));
            this.quitButton.clickColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(204)))));
            this.quitButton.clickColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))));
            this.quitButton.disabledColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.quitButton.disabledColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.quitButton.hoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(9)))), ((int)(((byte)(121)))));
            this.quitButton.hoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.quitButton.Location = new System.Drawing.Point(10, 635);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(280, 35);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "C L O S E";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // speedLabel
            // 
            this.speedLabel.Font = new System.Drawing.Font("Impact", 14F);
            this.speedLabel.ForeColor = System.Drawing.Color.White;
            this.speedLabel.Location = new System.Drawing.Point(10, 385);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(280, 35);
            this.speedLabel.TabIndex = 8;
            this.speedLabel.Text = "Speed: 1000 ms";
            this.speedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.ClientSize = new System.Drawing.Size(979, 681);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.speedModifierBar);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.chessboardContainer);
            this.Controls.Add(this.titleImage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Knight\'s Tour App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.titleImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedModifierBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox titleImage;
        private System.Windows.Forms.Panel chessboardContainer;
        private CustomButton.gradientButton quitButton;
        private System.Windows.Forms.RichTextBox logBox;
        private System.Windows.Forms.TrackBar speedModifierBar;
        private CustomButton.gradientButton solveButton;
        private CustomButton.gradientButton resetButton;
        private System.Windows.Forms.Label speedLabel;
    }
}

