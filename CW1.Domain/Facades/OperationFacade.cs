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

    public void DeleteOperation(Guid operationId)
    {
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