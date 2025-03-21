using CW1.Domain.Exporter.Interfaces;

namespace CW1.Domain.DomainClasses;

public class BankAccount
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }

    public BankAccount(Guid id, string name, decimal balance)
    {
        Id = id;
        Name = name;
        Balance = balance;
    }

    public void UpdateName(string newName)
    {
        Name = newName;
    }

    public void Credit(decimal amount)
    {
        Balance += amount;
    }

    public void Debit(decimal amount)
    {
        Balance -= amount;
    }
}