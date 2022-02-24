using System;
using System.Drawing;



namespace my
{
    public class myObj_000 : myObject
    {
        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        // -------------------------------------------------------------------------

        public myObj_000()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void Show(Graphics g)
        {
            switch (color)
            {
                case 0:
                    g.FillRectangle(Brushes.Red, X, Y, Size, Size);
                    break;

                case 1:
                    g.FillRectangle(Brushes.Yellow, X, Y, Size, Size);
                    break;

                case 2:
                    g.FillRectangle(Brushes.Blue, X, Y, Size, Size);
                    break;

                case 3:
                    g.FillRectangle(Brushes.Orange, X, Y, Size, Size);
                    break;

                case 4:
                    g.FillRectangle(Brushes.Aqua, X, Y, Size, Size);
                    break;

                case 5:
                    g.FillRectangle(Brushes.Violet, X, Y, Size, Size);
                    break;

                default:
                    g.FillRectangle(Brushes.White, X, Y, Size, Size);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public static void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            var list = new System.Collections.Generic.List<myObj_000>();

            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();
            System.Threading.Thread.Sleep(666);

            int staticStarsCnt = 500;

            // Add static stars
            Count += staticStarsCnt;

            for (int i = 0; i < staticStarsCnt; i++)
            {
                list.Add(new myObj_000_b());
            }

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                }

                System.Threading.Thread.Sleep(33);
                form.Invalidate();

                // Gradually increase number of moving stars, until the limit is reached
                if (list.Count != Count)
                {
                    list.Add(new myObj_000_a());
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    }

    // ===========================================================================================================
    // ===========================================================================================================

    public class myObj_000_a : myObj_000
    {
        protected override void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);
            max = rand.Next(75) + 20;
            cnt = 0;
            color = rand.Next(50);

            x = X;
            y = Y;

            int speed = rand.Next(10) + 1;

            int x0 = Width / 2;
            int y0 = Height / 2;

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - y0) * sp_dist);

            Size = 0;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            if (cnt++ > max)
            {
                cnt = 0;
                Size++;
            }

#if false
            float dAcc = 1.0f + (1.0f * acc / 1000.0f);
            dx *= dAcc;
            dy *= dAcc;
#else
            dx *= 1.005f;
            dy *= 1.005f;
#endif
            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }

            return;
        }
    };

    // ===========================================================================================================
    // ===========================================================================================================

    public class myObj_000_b : myObj_000
    {
        static SolidBrush br = new SolidBrush(Color.Black);

        private int lifeCounter = 0;

        protected override void generateNew()
        {
            lifeCounter = rand.Next(500) + 50;

            X = rand.Next(Width);
            Y = rand.Next(Height);
            color = rand.Next(50);

            max = rand.Next(125) + 25;
            cnt = 0;
            Size = 0;
        }

        public override void Move()
        {
            if (lifeCounter-- == 0)
            {
                generateNew();
            }
            else
            {
                if (cnt++ > max)
                {
                    cnt = 0;
                    Size = rand.Next(3);
                }
            }

            return;
        }

        protected override void Show(Graphics g)
        {
            // Draw static stars ...
            base.Show(g);

            // ... and make them semitransparent
            int alpha = rand.Next(50) + 75;
            br.Color = Color.FromArgb(alpha, 0, 0, 0);
            g.FillRectangle(br, X, Y, Size, Size);
        }
    };

};
