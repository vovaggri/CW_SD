using CW1.Domain.DomainClasses;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Facades;

public class OperationFacade
{
    private readonly IOperationRepository _repository;
    private readonly DomainFactory _factory;

    public OperationFacade(IOperationRepository repository, DomainFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, 
        DateTime date, Guid categoryId, string description = null)
    {
        var operation = _factory.CreateOperation(type, bankAccountId, amount, date, categoryId, description);
        _repository.Add(operation);
        return operation;
    }

    public void EditOperation(Guid operationId, string newDescription)
    {
        var operation = _repository.GetById(operationId);
        if (operation == null)
        {
            throw new Exception("Operation not found");
        }
        
        operation.Description = newDescription;
    }

    public void DeleteOperation(Guid operationId, BankAccountFacade bankAccountFacade)
    {
        var operation = _repository.GetById(operationId);
        if (operation == null)
        {
            throw new Exception("Operation not found");
        }

        decimal amount = operation.Amount;
        if (operation.Type == OperationType.Expense)
        {
            var bankAccount = bankAccountFacade.GetAccount(operation.BankAccountId);
            if (bankAccount == null)
            {
                throw new Exception("Bank account not found");
            }
            bankAccountFacade.ChangeBalance(bankAccount.Id, amount, OperationType.Income);
        }
        else
        {
            var bankAccount = bankAccountFacade.GetAccount(operation.BankAccountId);
            if (bankAccount == null)
            {
                throw new Exception("Bank account not found");
            }
            bankAccountFacade.ChangeBalance(bankAccount.Id, amount, OperationType.Expense);
        }
        _repository.Delete(operationId);
    }

    public Operation? GetOperation(Guid operationId)
    {
        return _repository.GetById(operationId);
    }

    public IEnumerable<Operation> GetAllOperations()
    {
        return _repository.GetAll();
    }
}