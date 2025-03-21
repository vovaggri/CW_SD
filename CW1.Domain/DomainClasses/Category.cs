using CW1.Domain.Exporter.Interfaces;

namespace CW1.Domain.DomainClasses;

public class Category
{
    public Guid Id { get; private set; }
    public CategoryType Type { get; private set; }
    public string Name { get; private set; }

    public Category(Guid id, CategoryType type, string name)
    {
        Id = id;
        Type = type;
        Name = name;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }
}