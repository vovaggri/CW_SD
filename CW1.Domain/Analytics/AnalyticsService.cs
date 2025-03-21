using CW1.Domain.DomainClasses;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Analytics;

public class AnalyticsService
{
    private readonly IOperationRepository _operationRepository;
    private readonly ICategoryRepository _categoryRepository;

    public AnalyticsService(ICategoryRepository categoryRepository, IOperationRepository operationRepository)
    {
        _operationRepository = operationRepository;
        _categoryRepository = categoryRepository;
    }

    public decimal GetBalanceDifference(DateTime start, DateTime end)
    {
        var operations = _operationRepository.GetAll()
            .Where(o => o.Date >= start && o.Date <= end);
        
        var incomeSum = operations
            .Where(o => o.Type == OperationType.Income)
            .Sum(o => o.Amount);
        
        var expenseSum = operations
            .Where(o => o.Type == OperationType.Expense)
            .Sum(o => o.Amount);
        
        return incomeSum - expenseSum;
    }

    public Dictionary<Guid, decimal> GetExpensesGroupedByCategory(DateTime start, DateTime end)
    {
        var operations = _operationRepository.GetAll()
            .Where(o => o.Type == OperationType.Expense
            && o.Date >= start && o.Date <= end);

        return operations
            .GroupBy(o => o.CategoryId)
            .ToDictionary(g => g.Key, g => g.Sum(x => x.Amount));
    }
}