using System;
using System.Numerics;

namespace ConsoleEngine;

internal class WireframeEdge
{
    public Vector3 Vertex1 { get; }
    public Vector3 Vertex2 { get; }

    public WireframeEdge(Vector3 vertex1, Vector3 vertex2)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
    }
}