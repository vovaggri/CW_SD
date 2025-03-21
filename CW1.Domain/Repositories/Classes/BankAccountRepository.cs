using CW1.Domain.DomainClasses;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Repositories.Classes;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly Dictionary<Guid, BankAccount> _accounts = new Dictionary<Guid, BankAccount>();
    
    public void Add(BankAccount account)
    {
        _accounts[account.Id] = account;
    }

    public void Update(BankAccount account)
    {
        _accounts[account.Id] = account;
    }

    public void Delete(Guid accountId)
    {
        _accounts.Remove(accountId);
    }

    public BankAccount? GetById(Guid id)
    {
        _accounts.TryGetValue(id, out var bankAccount);
        return bankAccount;
    }

    public IEnumerable<BankAccount> GetAll()
    {
        return _accounts.Values;
    }
}