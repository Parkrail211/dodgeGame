using System;
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
        int currentTick = 0;

        int paddle1X = 200;



        int paddle2X = 400;



        int paddleWidth = 10;
        int paddleHeight = 60;


        int ballX = 50;
        int ballY = 200;
        int ballSpeed = 6;
        int paddleSpeed = 6;

        int ballWidth = 10;
        int ballHeight = 10;

        List<int> p1Y = new List<int>();
        List<int> p2Y = new List<int>();


        bool wDown = false;
        bool sDown = false;
        bool dDown = false;
        bool aDown = false;
        bool menu = true;



        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Font screenFont = new Font("Consolas", 12);
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
                case Keys.D:
                    dDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.Space:
                    if (menu)
                    {
                        gameTimer.Enabled = true;
                        menu = false;
                        this.BackColor = Color.Black;
                        winLoseLabel.Visible = false;
                        buttonLabel.Visible = false;
                        p1Y.Clear();
                        p2Y.Clear();
                        ballX = 50;
                        ballY = 200;
                       
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
                case Keys.D:
                    dDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
            }
        }



        private void gameTimer_Tick(object sender, EventArgs e)
        {
            currentTick++;

            if (currentTick % 20 == 1)
            {
                p1Y.Add(400);
                p2Y.Add(-60);
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

            if (dDown == true && ballX < 591)
            {
                ballX += ballSpeed;
            }

            if (aDown == true && ballX > 0)
            {
                ballX -= ballSpeed;
            }

            for (int loop = 0; loop < p1Y.Count(); loop++)
            {
                p1Y[loop] -= paddleSpeed;
            }
            for (int loop = 0; loop < p1Y.Count(); loop++)
            {
                p2Y[loop] += paddleSpeed;
            }



            //create Rectangles of objects on screen to be used for collision detection

            Rectangle ball = new Rectangle(ballX, ballY, ballWidth, ballHeight);

            for (int loop = 0; loop < p1Y.Count(); loop++)
            {
                Rectangle paddle1 = new Rectangle(paddle1X, p1Y[loop], paddleWidth, paddleHeight);
                Rectangle paddle2 = new Rectangle(paddle2X, p2Y[loop], paddleWidth, paddleHeight);

                if (ball.IntersectsWith(paddle1) || ball.IntersectsWith(paddle2))
                {
                    gameTimer.Enabled = false;
                    menu = true;
                    this.BackColor = Color.White;
                    winLoseLabel.Visible = true;
                    winLoseLabel.Text = "Loser!!!";
                    buttonLabel.Visible = true;
                }
            }

            if (ballX > 590)
            {
                gameTimer.Enabled = false;
                menu = true;
                this.BackColor = Color.White;
                winLoseLabel.Visible = true;
                winLoseLabel.Text = "Winner!!!";
                buttonLabel.Visible = true;
            }





            //check if ball hits either paddle. If it does change the direction 
            //and place the ball in front of the paddle hit 


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            if (menu)
            {

                e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);
                for (int loop = 0; loop < p1Y.Count(); loop++)
                {
                    e.Graphics.FillRectangle(whiteBrush, paddle1X, p1Y[loop], paddleWidth, paddleHeight);
                }
                for (int loop = 0; loop < p2Y.Count(); loop++)
                {
                    e.Graphics.FillRectangle(whiteBrush, paddle2X, p2Y[loop], paddleWidth, paddleHeight);
                }
            }
 

            else
            {
                e.Graphics.FillRectangle(whiteBrush, ballX, ballY, ballWidth, ballHeight);
                for (int loop = 0; loop < p1Y.Count(); loop++)
                {
                    e.Graphics.FillRectangle(blueBrush, paddle1X, p1Y[loop], paddleWidth, paddleHeight);
                }
                for (int loop = 0; loop < p2Y.Count(); loop++)
                {
                    e.Graphics.FillRectangle(blueBrush, paddle2X, p2Y[loop], paddleWidth, paddleHeight);
                }

            }
        }
    }
}
