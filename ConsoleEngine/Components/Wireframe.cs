using System.Numerics;

namespace ConsoleEngine.Components;

internal class Wireframe : EntityComponent
{
    public List<Vector3> Vertices { get; set; }
    public List<(int, int)> Edges { get; }

    public Wireframe(List<Vector3> vertices, List<(int, int)> edges, Entity? entity) : base(entity)
    {
        Vertices = vertices;
        Edges = edges;
    }

    public List<Vector3> GetTransformedVertices(Matrix4x4 transformationMatrix)
    {
        List<Vector3> transformedVertices = new();
        foreach (var vertex in Vertices)
        {
            transformedVertices.Add(Vector3.Transform(vertex, transformationMatrix));
        }
        return transformedVertices;
    }
}

internal static class Wireframes
{
    public static Wireframe Cube(Entity entity) => new(
        vertices: new()
        {
            new(-1f, -1f, -1f),    // 0
            new( 1f, -1f, -1f),    // 1
            new(-1f,  1f, -1f),    // 2
            new( 1f,  1f, -1f),    // 3
            new(-1f, -1f,  1f),    // 4
            new( 1f, -1f,  1f),    // 5
            new(-1f,  1f,  1f),    // 6
            new( 1f,  1f,  1f),    // 7
        },
        edges: new()
        {
            (0, 1),
            (1, 3),
            (0, 2),
            (2, 3),
            (4, 5),
            (5, 7),
            (4, 6),
            (6, 7),
            (0, 4),
            (1, 5),
            (2, 6),
            (3, 7)
        },
        entity: entity);

    public static Wireframe Pyramid(Entity entity) => new(
        vertices: new()
        {
            new(-1f, 0f, -1f),
            new( 1f, 0f, -1f),
            new(-1f, 0f,  1f),
            new( 1f, 0f,  1f),
            new(0f, 2f, 0f)
        },
        edges: new()
        {
            (0,1),
            (0,2),
            (1,3),
            (2,3),
            (0,4),
            (1,4),
            (2,4),
            (3,4)
        },
        entity: entity);
}