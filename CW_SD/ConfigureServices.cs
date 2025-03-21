using System.Text.Json;
using CW1.Domain.Analytics;
using CW1.Domain.CommandAndDecorator;
using CW1.Domain.DomainClasses;
using CW1.Domain.Exporter;
using CW1.Domain.Exporter.Interfaces;
using CW1.Domain.Facades;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Classes;
using CW1.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CW_SD;

public static class ConfigureServices
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton<IBankAccountRepository, BankAccountRepository>();
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<IOperationRepository, OperationRepository>();
        services.AddSingleton<DomainFactory>();
        
        services.AddTransient<BankAccountFacade>();
        services.AddTransient<CategoryFacade>();
        services.AddTransient<OperationFacade>();
        services.AddTransient<AnalyticsService>();
    }
}