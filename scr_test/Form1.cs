using System;
using System.Drawing;
using System.Windows.Forms;


// https://sites.harding.edu/fmccown/screensaver/screensaver.html


namespace scr_test
{
    public partial class Form1 : Form
    {
        private bool isAlive = false;
        private Point oldMouseLocation;
        Graphics g = null;

        // -------------------------------------------------------------------

        public Form1(Rectangle bounds)
        {
            InitializeComponent();

            //Cursor.Hide();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Bounds = bounds;
            this.TopMost = true;
            this.DoubleBuffered = true;

            zzz2();
        }

        private void zzz1()
        {
            g = this.CreateGraphics();
            isAlive = true;

            new System.Threading.Tasks.Task(() =>
            {
                var list = new System.Collections.Generic.List<star>();

                for (int i = 0; i < 500; i++)
                {
                    list.Add(new star(this.Width, this.Height));
                }

                while (isAlive)
                {
                    g.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);

                    foreach (var s in list)
                    {
                        g.FillRectangle(Brushes.Red, s.X, s.Y, s.size, s.size);
                        s.move();
                    }

                    System.Threading.Thread.Sleep(10);
                }

                g.Dispose();

            }).Start();
        }

        // Using form's background image as our drawing surface
        private void zzz2()
        {
            Bitmap buffer = new Bitmap(this.Width, this.Height);    // set the size of the image
            g = Graphics.FromImage(buffer);                         // set the graphics to draw on the image

            isAlive = true;
            this.BackgroundImage = buffer;                          // set the PictureBox's image to be the buffer

            new System.Threading.Tasks.Task(() =>
            {
                var list = new System.Collections.Generic.List<star>();

                for (int i = 0; i < 1500; i++)
                {
                    list.Add(new star(this.Width, this.Height));
                }

                while (isAlive)
                {
                    g.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);

                    foreach (var s in list)
                    {
                        g.FillRectangle(Brushes.Green, s.X, s.Y, s.size, s.size);
                        s.move();
                    }

                    this.Invalidate();
                    System.Threading.Thread.Sleep(33);
                }

            }).Start();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int dist = 50;

            if (!oldMouseLocation.IsEmpty)
            {
                // Terminate if mouse is moved a significant distance
                if (Math.Abs(oldMouseLocation.X - e.X) > dist || Math.Abs(oldMouseLocation.Y - e.Y) > dist)
                {
                    isAlive = false;
                    g.Dispose();
                    Application.Exit();
                }
            }

            // Update mouse location
            oldMouseLocation = e.Location;
        }
    }
}
