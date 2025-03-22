using CW1.Domain.Exporter.Interfaces;

namespace CW1.Domain.DomainClasses;

public class Operation
{
    public Guid Id { get; private set; }
    public OperationType Type { get; private set; }
    public Guid BankAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime Date { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Description { get; set; }

    public Operation(Guid id, OperationType type, Guid bankAccountId, decimal amount, 
        DateTime date, Guid categoryId,string description = null)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }

        Id = id;
        Type = type;
        BankAccountId = bankAccountId;
        Amount = amount;
        Date = date;
        CategoryId = categoryId;
        Description = description;
    }
}