using CW1.Domain.DomainClasses;
using CW1.Domain.Facades;

namespace CW1.Domain.CommandAndDecorator;

public class CreateCategoryCommand : ICommand
{
    private readonly CategoryFacade _facade;
    private readonly CategoryType _type;
    private readonly string _name;

    public CreateCategoryCommand(CategoryFacade facade, CategoryType type, string name)
    {
        _facade = facade;
        _type = type;
        _name = name;
    }
    
    public void Execute()
    {
        var category = _facade.Create(_type, _name);
        Console.WriteLine($"Category was created: {category.Id}, type: {_type}, name: {_name}");
    }
}