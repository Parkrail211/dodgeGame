﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pong
{
    public partial class Form1 : Form
    {
        int paddle1X = 200;



        int paddle2X = 400;



        int paddleWidth = 5;
        int paddleHeight = 5;
        int p1Score = 0;
        int p2Score = 0;


        int ballX = 100;
        int ballY = 390;
        int ball2X = 500;
        int ball2Y = 390;
        int ballSpeed = 6;
        int paddleSpeed = 6;

        int timeLeft = 0; 
        int ballWidth = 10;
        int ballHeight = 10;

        List<int> asteroidsX = new List<int>();
        List<int> asteroidsY = new List<int>();
        List<int> asteroidsDirection = new List<int>();



        bool wDown = false;
        bool sDown = false;
        bool upDown = false;
        bool downDown = false;
        bool menu = true;
        bool gen = true;


        Random rnd = new Random();
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upDown = true;
                    break;
                case Keys.Down:
                    downDown = true;
                    break;
                case Keys.Space:
                    if (menu)
                    {
                        closeMenu();

                    }
                    break;
                case Keys.Escape:
                    if (menu)
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upDown = false;
                    break;
                case Keys.Down:
                    downDown = false;
                    break;
            }
        }



        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft >= 400)
            {
                if (p1Score > p2Score) {openMenu("Player One Wins");}
                else if (p1Score < p2Score) { openMenu("Player Two Wins"); }
                else { openMenu("Tie"); }

            }

            if (gen)
            {
                for (int loop = 0; loop <= 24; loop++)
                {
                    asteroidsX.Add(rnd.Next(0, 601));
                    asteroidsY.Add(rnd.Next(50, 351));
                    if (rnd.Next(0, 2) == 0)
                    {
                        asteroidsDirection.Add(-1);
                    }
                    else
                    {
                        asteroidsDirection.Add(1);
                    }
                    gen = false;
                }
            }

            //move player 1 
            if (wDown == true && ballY > 0)
            {
                ballY -= ballSpeed;
            }

            if (sDown == true && ballY < 390)
            {
                ballY += ballSpeed;
            }

            if (upDown == true && ball2Y > 0)
            {
                ball2Y -= ballSpeed;
            }

            if (downDown == true && ball2Y < 390)
            {
                ball2Y += ballSpeed;
            }

            for (int loop = 0; loop < asteroidsX.Count(); loop++)
            {
                asteroidsX[loop] += paddleSpeed * asteroidsDirection[loop];
            }

            for (int loop = 0; loop < asteroidsX.Count(); loop++)
            {
                if (asteroidsX[loop] <= 0 || asteroidsX[loop] >= 600)
                {
                    asteroidsDirection[loop] = asteroidsDirection[loop] * -1;
                }
            }


            //create Rectangles of objects on screen to be used for collision detection

            Rectangle ball = new Rectangle(ballX, ballY, ballWidth, ballHeight);
            Rectangle ball2 = new Rectangle(ball2X, ball2Y, ballWidth, ballHeight);


            for (int loop = 0; loop < asteroidsX.Count(); loop++)
            {
                Rectangle paddle1 = new Rectangle(asteroidsX[loop], asteroidsY[loop], paddleWidth, paddleHeight);


                if (ball.IntersectsWith(paddle1))
                {
                    ballX = 100;
                    ballY = 390;
                }
                if (ball2.IntersectsWith(paddle1))
                {
                    ball2X = 500;
                    ball2Y = 390;
                }
            }

            if (ballY <= 0)
            {
                p1Score++;
                p1ScoreLabel.Text = $"{p1Score}";
                ballX = 100;
                ballY = 390;
            }

            if (ball2Y <= 0)
            {
                p2Score++;
                p2ScoreLabel.Text = $"{p2Score}";
                ball2X = 500;
                ball2Y = 390;
            }




            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            if (menu)
            {


                for (int loop = 0; loop < asteroidsX.Count(); loop++)
                {
                    e.Graphics.FillRectangle(whiteBrush, asteroidsX[loop], asteroidsY[loop], paddleWidth, paddleHeight);
                }

            }


            else
            {
                e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);
                e.Graphics.FillRectangle(whiteBrush, ball2X, ball2Y, ballWidth, ballHeight);
                for (int loop = 0; loop < asteroidsX.Count(); loop++)
                {
                    e.Graphics.FillRectangle(blueBrush, asteroidsX[loop], asteroidsY[loop], paddleWidth, paddleHeight);
                }


            }
            e.Graphics.FillRectangle(whiteBrush, 296, timeLeft, 8, 400);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeLeft++;
        }

        private void openMenu(string text)
        {
            menu = true;
            this.BackColor = Color.White;
            winLoseLabel.Visible = true;
            winLoseLabel.Text = text;
            buttonLabel.Visible = true;
            timer.Enabled = false;
            timeLeft = 0;
        }

        private void closeMenu()
        {
            gameTimer.Enabled = true;
            menu = false;
            this.BackColor = Color.Black;
            winLoseLabel.Visible = false;
            buttonLabel.Visible = false;
            asteroidsX.Clear();
            asteroidsY.Clear();
            gen = true;
            timer.Enabled = true;

            ballX = 50;
            ballY = 390;
            ball2X = 500;
            ball2Y = 390;

        }
    }
}
