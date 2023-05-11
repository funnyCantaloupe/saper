using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace saper
{

    public partial class Form2 : Form
    {
        public Form1 form1;
        public int[,] board;
        private PictureBox pig;
        private int buttonWidth;
        private int buttonHeight;


        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            board = new int[form1.n, form1.n];
            PlaceBombs();
            PlacePigs(form1.k);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.buttonWidth = buttonWidth;
            this.buttonHeight = buttonHeight;

            int matrixWidth = form1.n;
            int matrixHeight = form1.n;
            int panelWidth = panel1.Width;
            int panelHeight = panel1.Height;

            pig = new PictureBox();
            pig.Image = (Image)Properties.Resources.pig_angrybirds;
            

            

            
            if ((float)panelWidth / panelHeight < (float)matrixWidth / matrixHeight)
            {
                buttonWidth = panelWidth / matrixWidth;
                buttonHeight = buttonWidth;
            }
            else
            {
                buttonHeight = panelHeight / matrixHeight;
                buttonWidth = buttonHeight;
            }

            for (int i = 0; i < matrixHeight; i++)
            {
                for (int j = 0; j < matrixWidth; j++)
                {
                    Button button = new Button();
                    button.Width = buttonWidth;
                    button.Height = buttonHeight;
                    button.Top = i * buttonHeight;
                    button.Left = j * buttonWidth;

                    int index = i * form1.n + j;
                    button.Tag = index;

                    button.Click += new EventHandler(Button_Click);
                    panel1.Controls.Add(button);
                }
            }


            StartTimer();
        }


        private int timeLeft;
        private Timer timer;

        public void StartTimer()
        {
            timeLeft = form1.k * 3;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            label1.Text = timeLeft.ToString();
            if (timeLeft <= 0)
            {
                timer.Stop();
                MessageBox.Show("Koniec czasu. Przegrałeś!");
                this.Hide();
            }
        }



        public void PlaceBombs()
        {
            Random rnd = new Random();
            int bombsCount = 0;

            while (bombsCount < 1)
            {
                int row = rnd.Next(form1.n);
                int col = rnd.Next(form1.n);

                if (board[row, col] == 0)
                {
                    board[row, col] = -1; 
                    bombsCount++;
                }
            }
        }

        public void PlacePigs(int k)
        {
            Random rnd = new Random();

            

            for (int i = 0; i < k; i++)
            {
                int row, col;

             
                do
                {
                    row = rnd.Next(form1.n);
                    col = rnd.Next(form1.n);
                } while (board[row, col] != 0);

               
                board[row, col] = 1;


                PictureBox pig = new PictureBox();
                pig.Image = (Image)Properties.Resources.pig_angrybirds;
                pig.Size = new Size(this.buttonWidth, this.buttonHeight);
                pig.Location = new Point(col * this.buttonWidth, row * this.buttonHeight);


                panel1.Controls.Add(pig);
            }
        }


        public bool IsBomb(int[,] board, int row, int col)
        {
            return board[row, col] == -1;
        }

        public bool IsPig(int row, int col)
        {
            if (board[row, col] == 1)
            {
                foreach (Control control in panel1.Controls)
                {
                    if (control is Button && (int)control.Tag == row * form1.n + col)
                    {
                        PictureBox pig = new PictureBox();
                        pig.Image = (Image)Properties.Resources.pig_angrybirds;
                        pig.Size = new Size(buttonWidth, buttonHeight);
                        pig.Location = control.Location;
                        panel1.Controls.Add(pig);
                        panel1.Controls.Remove(control);

                        return true;
                    }
                }

            }

            return false;
        }

        public void CheckWin()
        {
            int counter = 0;
            foreach (Control control in panel1.Controls)
            {
                if (control is Button && board[(int)control.Tag / form1.n, (int)control.Tag % form1.n] == 1)
                {
                    return;
                }
            }



            timer.Stop();
            MessageBox.Show("Wygrałeś!");
                this.Hide();
            


        }



        public void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int index = (int)button.Tag;
            int row = index / form1.n;
            int col = index % form1.n;

            if (IsBomb(board, row, col))
            {
                timer.Stop();
                MessageBox.Show("Bomba. Przegrałeś!");
                this.Hide();
                return;
            }
            else if (IsPig(row, col))
            {
                panel1.Controls.Remove(button);

                pig.Location = button.Location;
                panel1.Controls.Add(pig);
                CheckWin();
            }
            else
            {
                
                return;
            }
            
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
