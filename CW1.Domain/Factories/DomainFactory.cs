using CW1.Domain.DomainClasses;

namespace CW1.Domain.Factories;

public class DomainFactory
{
    public BankAccount CreateBankAccount(string name, decimal initialBalance)
    {
        return new BankAccount(Guid.NewGuid(), name, initialBalance);
    }

    public Category CreateCategory(CategoryType type, string name)
    {
        return new Category(Guid.NewGuid(), type, name);
    }

    public Operation CreateOperation(OperationType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId,
        string description = null)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }
        return new Operation(Guid.NewGuid(), type, bankAccountId, amount, date, categoryId, description);
    }
}