using CW1.Domain.Analytics;
using CW1.Domain.Facades;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace MenuLibrary;

public class Menu
{
    public static void MainMenu(IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository,
        IOperationRepository operationRepository, DomainFactory factory, BankAccountFacade accountFacade,
        CategoryFacade categoryFacade, OperationFacade operationFacade, AnalyticsService analytics)
    {
        
    }
}