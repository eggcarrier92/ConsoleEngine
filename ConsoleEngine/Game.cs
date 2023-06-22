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

    private bool rPressed;


    private void Update()
    {
        Screen.Clear();

        //Wireframe? cubeWF = Entities[0].GetComponent<Wireframe>();
        Transform? cubeTransform = Entities[0].GetComponent<Transform>();

        //cubeTransform?.Translate(new Vector3(0f, 0f, -1f) * 50 * DeltaTime);

        //if (Console.KeyAvailable && Console.ReadKey(true).KeyChar == 'w')
            cubeTransform?.Rotate(Vector3.UnitY, 90f * DeltaTime);

        foreach(var entity in Entities)
        {
            Wireframe? entityWireframe = entity.GetComponent<Wireframe>();
            Transform? entityTransform = entity.GetComponent<Transform>();
            if (entityWireframe != null && entityTransform != null)
                Screen.RenderWireframe(entityWireframe, entityTransform);
        }

        Screen.Update();
    }
}