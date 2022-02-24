using System;
using System.Drawing;
using System.Runtime.InteropServices;   // For dll import



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
