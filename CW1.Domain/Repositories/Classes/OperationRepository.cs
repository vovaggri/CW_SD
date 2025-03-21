using CW1.Domain.DomainClasses;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Repositories.Classes;

public class OperationRepository : IOperationRepository
{
    private readonly Dictionary<Guid, Operation> _operations = new Dictionary<Guid, Operation>();
    
    public void Add(Operation operation)
    {
        _operations[operation.Id] = operation;
    }

    public void Update(Operation operation)
    {
        _operations[operation.Id] = operation;
    }

    public void Delete(Guid operationId)
    {
        _operations.Remove(operationId);
    }

    public Operation? GetById(Guid operationId)
    {
        _operations.TryGetValue(operationId, out var operation);
        return operation;
    }

    public IEnumerable<Operation> GetAll()
    {
        return _operations.Values;
    }
}