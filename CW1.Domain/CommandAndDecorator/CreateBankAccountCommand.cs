using CW1.Domain.Facades;

namespace CW1.Domain.CommandAndDecorator;

public class CreateBankAccountCommand : ICommand
{
    private readonly BankAccountFacade _facade;
    private readonly string _name;
    private readonly decimal _initialBalance;

    public CreateBankAccountCommand(BankAccountFacade facade, string name, decimal initialBalance)
    {
        _facade = facade;
        _name = name;
        _initialBalance = initialBalance;
    }
    
    public void Execute()
    {
        var account = _facade.CreateAccount(_name, _initialBalance);
        Console.WriteLine($"Account was created: {account.Id}, Name: {account.Name}, Balance: {account.Balance}");
    }
}