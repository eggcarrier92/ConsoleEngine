using ConsoleEngine.Components;
using System.Numerics;

namespace ConsoleEngine;

internal class Screen
{
    public int Width { get; }
    public int Height { get; }
    public float FocalLength { get; }
    public char[] Buffer { get; private set; }

    public Screen(int width, int height, float focalLength)
    {
        Width = width;
        Height = height;
        FocalLength = focalLength;
        Buffer = new char[Width * Height];
        Console.SetWindowSize(Width, Height);
        Console.SetBufferSize(Width, Height);
        Console.CursorVisible = false;
        Clear();
    }

    public ref char GetPixel(int x, int y)
    {
        return ref Buffer[(Height / 2 - y) * Width + Width / 2 + x];
    }

    public void SetPixel(int x, int y, char c = '█')
    {
        if (x <= -Width / 2 || x >= Width / 2 || y <= -Height / 2 || y >= Height / 2)
            return;
        GetPixel(x, y) = c;
    }

    public void ClearPixel(int x, int y)
    {
        Buffer[(Height / 2 - y) * (Width / 2) + x] = ' ';
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

    public void DrawLine(int x1, int y1, int x2, int y2)
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

        SetPixel(x1, y1);

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
            SetPixel(x, y);
        }
    }

    public void RenderWireframe(Wireframe wireframe, Transform transform)
    {
        List<Vector2> projectedCube = VertexProjector.GetProjected(
            wireframe.GetTransformedVertices(transform.TransformationMatrix), FocalLength);

        foreach (var edge in wireframe.Edges)
        {
            Vector2 point1 = projectedCube[edge.Item1];
            Vector2 point2 = projectedCube[edge.Item2];
            int x1 = (int)Math.Round(point1.X);
            int x2 = (int)Math.Round(point2.X);
            int y1 = (int)Math.Round(point1.Y);
            int y2 = (int)Math.Round(point2.Y);
            DrawLine(x1, y1, x2, y2);
        }
    }
}
