using System.Numerics;
using ConsoleEngine.Components;

namespace ConsoleEngine;

public static class Program
{
    private const int ScreenWidth = 100;
    private const int ScreenHeight = 75;
    private const float focalLength = 75;

    private static void Main()
    {
        Game game = new(ScreenWidth, ScreenHeight, focalLength);

        Entity cube = new("cube");
        game.Entities.Add(cube);
        cube.AddComponents(new()
        {
            new Transform(
                position: new Vector3(0f, -25f, 200f),
                rotation: Quaternion.Identity,
                scale: Vector3.One * 50f,
                entity: cube),
            Wireframes.Pyramid(entity: cube)
        });
        
        game.Run();

        //while (true)
        //{
        //    if (Console.KeyAvailable)
        //    {
        //        char charToFill = Console.ReadKey().KeyChar;
        //        _screen.Fill(charToFill);
        //    }

        //    _screen.Update();
        //}
    }
}