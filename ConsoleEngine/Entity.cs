using ConsoleEngine.Components;

namespace ConsoleEngine;

internal class Entity
{
    public string Name { get; set; }

    private List<EntityComponent> Components { get; } = new();

    public Entity(string name)
    {
        Name = name;
    }

    public T? GetComponent<T>() where T : EntityComponent
    {
        return (T?)Components.Find(x => x.GetType() == typeof(T));
    }

    public void AddComponent(EntityComponent component)
    {
        if (component.Entity != this)
            throw new Exception("Cannot add a component that is already attached to a different entity");
        Components.Add(component);
    }

    public void AddComponents(List<EntityComponent> components)
    {
        foreach (var component in components)
            Components.Add(component);
    }
}