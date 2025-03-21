using CW1.Domain.DomainClasses;

namespace CW1.Domain.Repositories.Interfaces;

public interface IBankAccountRepository
{
    void Add(BankAccount account);
    void Update(BankAccount account);
    void Delete(Guid accountId);
    BankAccount? GetById(Guid id);
    IEnumerable<BankAccount> GetAll();
}