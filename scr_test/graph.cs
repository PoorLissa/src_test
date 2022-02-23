using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // For dll import

public class star
{
    public int size;
    public int X;
    public int Y;

    private int dx;
    private int dy;

    private int MaxX;
    private int MaxY;

    public star(int maxX, int maxY)
    {
        var rand = new Random();

        MaxX = maxX;
        MaxY = maxY;

        X = rand.Next(maxX);
        Y = rand.Next(maxY);

        int speed = 20;

        dx = (rand.Next(speed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
        dy = (rand.Next(speed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

        size = rand.Next(10) + 1;
    }

    public void move()
    {
        X += dx;
        Y += dy;

        if (X < 0 || X > MaxX)
        {
            dx *= -1;
        }

        if (Y < 0 || Y > MaxY)
        {
            dy *= -1;
        }
    }
};

public class myGraphics
{
    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    private static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("user32.dll", ExactSpelling = true)]
    private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

    [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    private static extern IntPtr GetDesktopWindow();

    public static void DrawBitmapToScreen(Bitmap bmp)
    {
        int width = bmp.Width;
        int height = bmp.Height;

        IntPtr hwnd = GetDesktopWindow();
        IntPtr hdc = GetDC(hwnd);
        using (Graphics g = Graphics.FromHdc(hdc))
        {
            g.DrawImage(bmp, new Point(0, 0));
        }

        ReleaseDC(hwnd, hdc);
    }

    public static void DrawBitmapToWindow(Bitmap bmp, IntPtr handle)
    {
        int width = bmp.Width;
        int height = bmp.Height;

        IntPtr hwnd = handle;
        IntPtr hdc = GetDC(hwnd);
        using (Graphics g = Graphics.FromHdc(hdc))
        {
            g.DrawImage(bmp, new Point(0, 0));
        }

        ReleaseDC(hwnd, hdc);
    }

}
