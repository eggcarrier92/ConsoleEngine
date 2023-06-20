using ConsoleEngine.Components;
using System.Numerics;

namespace ConsoleEngine;

internal class Game
{
    public Screen Screen { get; }

    public List<Entity> Entities { get; } = new();

    public float DeltaTime { get; private set; }

    public Game(int width, int height, float focalLength)
    {
        Screen = new(width, height, focalLength);
    }

    public void Run()
    {
        long microsecondsOld = DateTime.Now.Ticks / TimeSpan.TicksPerMicrosecond;
        while (true)
        {
            long microseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMicrosecond;
            Update();
            DeltaTime = (microseconds - microsecondsOld) / 1000000f;
            microsecondsOld = microseconds;
        }
    }

    private void Update()
    {
        Screen.Clear();
        Wireframe? cubeWF = Entities[0].GetComponent<Wireframe>();

        for (int i = 0; i < cubeWF?.Vertices.Count; i++)
        {
            cubeWF.Vertices[i] += new Vector3(-1f, 1f, 0f) * 20 * DeltaTime;
        }

        foreach(var entity in Entities)
        {
            Wireframe? entityWireframe = entity.GetComponent<Wireframe>();
            if (entityWireframe != null)
                Screen.RenderWireframe(entityWireframe);
        }

        Screen.Update();
    }
}