using System.Runtime.CompilerServices;

namespace ConsoleEngine;

public class Program
{
    private const int ScreenWidth = 100;
    private const int ScreenHeight = 75;

#pragma warning disable CS8618 
    private static Screen _screen;
#pragma warning restore CS8618 

    private static void Main()
    {
        _screen = new(ScreenWidth, ScreenHeight);

        DrawLine(20, 10, 30, 5);
        _screen.Update();

        //while (true)
        //{
        //    if (Console.KeyAvailable)
        //    {
        //        char charToFill = Console.ReadKey().KeyChar;
        //        _screen.Fill(charToFill);
        //    }

        //    _screen.Update();
        //}
    }

    private static void DrawLine(int x1, int y1, int x2, int y2)
    {
        int x = x1, y = y1;
        int deltaX = Math.Abs(x2 - x1);
        int deltaY = Math.Abs(y2 - y1);
        int signX = Math.Sign(x2 - x1);
        int signY = Math.Sign(y2 - y1);

        bool interchange = false;
        if (deltaY > deltaX)
        {
            (deltaX, deltaY) = (deltaY, deltaX);
            interchange = true;
        }

        float e = 2 * deltaY - deltaX;
        float a = 2 * deltaY;
        float b = 2 * deltaY - 2 * deltaX;

        _screen.SetPixel(x1, y1);

        for (int i = 1; i <= deltaX; i++)
        {
            if (e < 0)
            {
                if (interchange)
                    y += signY;
                else
                    x += signX;
                e += a;
            }
            else
            {
                y += signY;
                x += signX;
                e += b;
            }
            _screen.SetPixel(x, y);
        }
    }
}