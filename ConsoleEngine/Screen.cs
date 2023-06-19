using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine;

internal class Screen
{
    public int Width { get; }
    public int Height { get; }
    public char[] Buffer { get; private set; }

    public Screen(int width, int height)
    {
        Width = width;
        Height = height;
        Buffer = new char[Width * Height];
        Console.SetWindowSize(Width, Height);
        Console.SetBufferSize(Width, Height);
        Console.CursorVisible = false;
        Clear();
    }

    public void SetPixel(int x, int y, char c = '█')
    {
        Buffer[(Height - y) * Width + x] = c;
    }

    public void ClearPixel(int x, int y)
    {
        Buffer[(Height - y) * Width + x] = ' ';
    }

    public void Fill(char c = '█')
    {
        Array.Fill(Buffer, c);
    }

    public void Clear()
    {
        Array.Fill(Buffer, ' ');
    }

    public void Update()
    {
        Console.SetCursorPosition(0, 0);
        Console.Write(Buffer);
    }
}
