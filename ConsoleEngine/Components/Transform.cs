using System.Numerics;

namespace ConsoleEngine.Components;

internal class Transform : EntityComponent
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    public Transform(Entity entity) : base(entity)
    {
    }
}