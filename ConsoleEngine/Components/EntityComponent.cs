namespace ConsoleEngine.Components;

internal abstract class EntityComponent
{
    public Entity? Entity { get; }

    public EntityComponent(Entity? entity)
    {
        Entity = entity;
    }
}