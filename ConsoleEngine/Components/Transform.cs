using System.Numerics;

namespace ConsoleEngine.Components;

internal class Transform : EntityComponent
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public Matrix4x4 TransformationMatrix 
    { 
        get
        {
            Matrix4x4 translation = Matrix4x4.CreateTranslation(Position);
            Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(Rotation);
            Matrix4x4 scale = Matrix4x4.CreateScale(Scale);
            return scale * rotation * translation;
        } 
    }

    public Transform(Entity entity) : base(entity) 
    {
        Position = Vector3.Zero;
        Rotation = Quaternion.Identity;
        Scale = Vector3.One;
    }

    public Transform(Vector3 position, Quaternion rotation, Vector3 scale, Entity entity) : this(entity)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public void Translate(float x, float y, float z)
    {
        Position += new Vector3(x, y, z);
    }

    public void Translate(Vector3 translation)
    {
        Position += translation;
    }

    public void Rotate(Vector3 axis, float angleDegrees)
    {
        Rotation = Quaternion.CreateFromAxisAngle(axis, angleDegrees * (float)Math.PI / 180f) * Rotation;
    }
}