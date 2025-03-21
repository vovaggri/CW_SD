using CW1.Domain.Analytics;
using CW1.Domain.Facades;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using MenuLibrary;

namespace CW_SD;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices.Configure(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var bankAccountRepository = serviceProvider.GetService<IBankAccountRepository>();
        var categoryRepository = serviceProvider.GetService<ICategoryRepository>();
        var operationRepository = serviceProvider.GetService<IOperationRepository>();
        var domainFactory = serviceProvider.GetService<DomainFactory>();
        var bankAccountFacade = serviceProvider.GetService<BankAccountFacade>();
        var categoryFacade = serviceProvider.GetService<CategoryFacade>();
        var operationFacade = serviceProvider.GetService<OperationFacade>();
        var analytics = serviceProvider.GetService<AnalyticsService>();

        if (bankAccountRepository == null || categoryRepository == null || operationRepository == null ||
            domainFactory == null || bankAccountFacade == null || categoryFacade == null ||
            operationFacade == null || analytics == null)
        {
            Console.WriteLine("Error of initialization");
            return;
        }
        
        Menu.MainMenu(bankAccountRepository, categoryRepository, operationRepository, domainFactory, 
            bankAccountFacade, categoryFacade,operationFacade, analytics);

        Console.WriteLine("Welcome to CW_SD!");
        Console.ReadLine();
    }
}
