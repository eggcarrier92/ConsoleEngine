using System.Numerics;

namespace ConsoleEngine;

internal static class VertexProjector
{
    public static float GetProjected(float xy, float z, float focalLength)
    {
        return (focalLength * xy) / (focalLength + z);
    }

    public static List<Vector2> GetProjected(List<Vector3> vertices, float focalLength)
    {
        List<Vector2> projectedVertices = new();
        foreach (var vertex in vertices)
        {
            float projectedX = GetProjected(vertex.X, vertex.Z, focalLength);
            float projectedY = GetProjected(vertex.Y, vertex.Z, focalLength);
            projectedVertices.Add(new(projectedX, projectedY));
        }
        return projectedVertices;
    }
}