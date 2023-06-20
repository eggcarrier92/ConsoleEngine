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
}

internal static class Wireframes
{
    public static Wireframe Cube(Entity entity) => new(
            vertices: new()
            {
                new(-30f, -30f, 30f),    // 0
                new( 30f, -30f, 30f),    // 1
                new(-30f,  30f, 30f),    // 2
                new( 30f,  30f, 30f),    // 3
                new(-30f, -30f, 60f),    // 4
                new( 30f, -30f, 60f),    // 5
                new(-30f,  30f, 60f),    // 6
                new( 30f,  30f, 60f),    // 7
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
}