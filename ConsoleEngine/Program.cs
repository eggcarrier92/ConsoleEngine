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

        DrawLine(1, 1, 6, 3);
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
        float error = 0;
        float slope = (y2 - y1) / (float)(x2 - x1);
        
        _screen.SetPixel(x1, y1);

        int y = y1;
        for (int x = x1 + 1; x <= x2; x++)
        {
            error += slope;

            if (error > 0.5f)
            {
                y++;
                error--;
            }

            _screen.SetPixel(x, y);
        }
    }
}