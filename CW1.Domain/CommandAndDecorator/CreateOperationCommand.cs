using CW1.Domain.DomainClasses;
using CW1.Domain.Facades;

namespace CW1.Domain.CommandAndDecorator;

public class CreateOperationCommand : ICommand
{
    private readonly OperationFacade _facade;
    private readonly OperationType _type;
    private readonly Guid _bankAccountId;
    private readonly Guid _categoryId;
    private readonly decimal _amount;
    private readonly DateTime _date;
    private readonly string _description;

    public CreateOperationCommand(OperationFacade facade, OperationType type, Guid bankAccountId,
        Guid categoryId, decimal amount, DateTime date, string description = null)
    {
        _facade = facade;
        _type = type;
        _bankAccountId = bankAccountId;
        _categoryId = categoryId;
        _amount = amount;
        _date = date;
        _description = description;
    }
    
    public void Execute()
    {
        var operation = _facade.CreateOperation(_type, _bankAccountId, _amount, _date, _categoryId,_description);
        Console.WriteLine($"Operation was created: {operation.Id}");
        Console.WriteLine($"Bank Account Id: {_bankAccountId}, CategoryId: {_categoryId} Amount: {_amount}, " +
                          $"\nDate: {_date}, Description: {_description}");
    }
}