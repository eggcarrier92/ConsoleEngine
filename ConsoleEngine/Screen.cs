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
        foreach ((int, int) edge in wireframe.Indices)
        {
            Vector3 vertex1 = wireframe.GetTransformedVertices(transform.TransformationMatrix)[edge.Item1];
            Vector3 vertex2 = wireframe.GetTransformedVertices(transform.TransformationMatrix)[edge.Item2];

            // If the line between vertex1 and vertex2 is in front of the clipping plane, do a normal projection
            if (vertex1.Z >= FocalLength && vertex2.Z >= FocalLength)
            {
                int projectedX1 = (int)Math.Round(FocalLength * vertex1.X / vertex1.Z);
                int projectedY1 = (int)Math.Round(FocalLength * vertex1.Y / vertex1.Z);
                int projectedX2 = (int)Math.Round(FocalLength * vertex2.X / vertex2.Z);
                int projectedY2 = (int)Math.Round(FocalLength * vertex2.Y / vertex2.Z);

                DrawLine(projectedX1, projectedY1, projectedX2, projectedY2);
            }
            // If the line between vertex1 and vertex2 intersects the clipping plane, 
            // find the point of intersection and connect the vertex that is in front
            // of the clipping plane and that point
            if (vertex1.Z >= FocalLength && vertex2.Z < FocalLength)
            {
                int projectedX1 = (int)Math.Round(FocalLength * vertex1.X / vertex1.Z);
                int projectedY1 = (int)Math.Round(FocalLength * vertex1.Y / vertex1.Z);

                int projectedX2 = (int)Math.Round((FocalLength - vertex1.Z) * (vertex2.X - vertex1.X) / (vertex2.Z - vertex1.Z) + vertex1.X);
                int projectedY2 = (int)Math.Round((FocalLength - vertex1.Z) * (vertex2.Y - vertex1.Y) / (vertex2.Z - vertex1.Z) + vertex1.Y);

                DrawLine(projectedX1, projectedY1, projectedX2, projectedY2);
            }
            if (vertex1.Z < FocalLength && vertex2.Z >= FocalLength)
            {
                int projectedX1 = (int)Math.Round((FocalLength - vertex2.Z) * (vertex1.X - vertex2.X) / (vertex1.Z - vertex2.Z) + vertex2.X);
                int projectedY1 = (int)Math.Round((FocalLength - vertex2.Z) * (vertex1.Y - vertex2.Y) / (vertex1.Z - vertex2.Z) + vertex2.Y);

                int projectedX2 = (int)Math.Round(FocalLength * vertex2.X / vertex2.Z);
                int projectedY2 = (int)Math.Round(FocalLength * vertex2.Y / vertex2.Z);

                DrawLine(projectedX1, projectedY1, projectedX2, projectedY2);
            }
            // If both vertices are behind the clipping plane, do not do anything
        }
    }
}
