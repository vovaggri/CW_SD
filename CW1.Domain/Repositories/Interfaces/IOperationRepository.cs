using CW1.Domain.DomainClasses;

namespace CW1.Domain.Repositories.Interfaces;

public interface IOperationRepository
{
    void Add(Operation operation);
    void Update(Operation operation);
    void Delete(Guid operationId);
    Operation? GetById(Guid operationId);
    IEnumerable<Operation> GetAll();
}