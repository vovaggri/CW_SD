using CW1.Domain.DomainClasses;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Facades;

public class BankAccountFacade
{
    private readonly IBankAccountRepository _repository;
    private readonly DomainFactory _factory;

    public BankAccountFacade(IBankAccountRepository repository, DomainFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public BankAccount CreateAccount(string name, decimal initialBalance = 0)
    {
        var account = _factory.CreateBankAccount(name, initialBalance);
        _repository.Add(account);
        return account;
    }

    public BankAccount? GetAccount(Guid id)
    {
        return _repository.GetById(id);
    }

    public void UpdateAccount(Guid accountId, string newName)
    {
        var account = _repository.GetById(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }
        
        account.UpdateName(newName);
        _repository.Update(account);
    }

    public void DeleteAccount(Guid accountId)
    {
        _repository.Delete(accountId);
    }

    public IEnumerable<BankAccount> GetAllAccounts()
    {
        return _repository.GetAll();
    }
    
    public void ChangeBalance(Guid accountId, decimal amount, OperationType operationType)
    {
        var account = _repository.GetById(accountId);
        if (account == null)
        {
            throw new Exception("Account not found");
        }

        if (operationType == OperationType.Expense)
        {
            if (account.Balance < amount)
            {
                throw new Exception("Insufficient balance");
            }
            account.Debit(amount);
        }
        else
        {
            account.Credit(amount);
        }
    }
}