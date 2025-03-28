﻿using System.Diagnostics.SymbolStore;
using CW1.Domain.Analytics;
using CW1.Domain.DomainClasses;
using CW1.Domain.Facades;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace MenuLibrary;

public static class Menu
{
    private static IBankAccountRepository _bankAccountRepository;
    private static ICategoryRepository _categoryRepository;
    private static IOperationRepository _operationRepository;
    private static DomainFactory _factory;
    private static BankAccountFacade _bankAccountFacade;
    private static CategoryFacade _categoryFacade;
    private static OperationFacade _operationFacade;
    private static AnalyticsService _analyticsService;
    
    public static void MainMenu(IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository,
        IOperationRepository operationRepository, DomainFactory factory, BankAccountFacade accountFacade,
        CategoryFacade categoryFacade, OperationFacade operationFacade, AnalyticsService analytics)
    {
        _bankAccountRepository = bankAccountRepository;
        _categoryRepository = categoryRepository;
        _operationRepository = operationRepository;
        _factory = factory;
        _bankAccountFacade = accountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _analyticsService = analytics;
        
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("-------Main Menu-------");
            Console.WriteLine("1. Manage Bank Accounts");
            Console.WriteLine("2. Manage Categories");
            Console.WriteLine("3. Manage Operations");
            Console.WriteLine("4. Look analytics");
            Console.WriteLine("5. Export");
            Console.WriteLine("6. Exit");
            
            Console.Write("Choose an option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ManageBankAccounts();
                    break;
                case "2":
                    ManageCategories();
                    break;
                case "3":
                    ManageOperations();
                    break;
                case "4":
                    ManageAnalytics();
                    break;
                case "5":
                    ManageExport();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option! Try again!");
                    break;
            }
        }
    }

    private static void ManageBankAccounts()
    {
        Console.Clear();
        Console.WriteLine("-------Manage Bank Accounts-------");
        Console.WriteLine("1. Add Bank Account");
        Console.WriteLine("2. Edit Bank Account");
        Console.WriteLine("3. Delete Bank Account");
        Console.WriteLine("4. Get All Bank Accounts");
        Console.WriteLine("5. Return to Main Menu");
        
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Middleware.CreateBankAccount(_bankAccountFacade);
                Console.Write("Touch enter to return to Manage Bank Accounts");
                Console.ReadLine();
                break;
            case "2":
                Middleware.UpdateBankAccount(_bankAccountFacade);
                Console.Write("Touch enter to return to Manage Bank Accounts");
                Console.ReadLine();
                break;
            case "3":
                Middleware.DeleteBankAccount(_bankAccountFacade);
                Console.Write("Touch enter to return to Manage Bank Accounts");
                Console.ReadLine();
                break;
            case "4":
                Middleware.GetAllBankAccounts(_bankAccountFacade);
                Console.Write("Touch enter to return to Manage Bank Accounts");
                Console.ReadLine();
                break;
            case "5":
                break;
            default:
                Console.WriteLine("Invalid option! Try again!");
                Console.Write("Touch enter to return to Manage Bank Accounts");
                Console.ReadLine();
                break;
        }
    }

    private static void ManageCategories()
    {
        Console.Clear();
        Console.WriteLine("-------Manage Categories-------");
        Console.WriteLine("1. Create Category");
        Console.WriteLine("2. Delete Category");
        Console.WriteLine("3. Edit Category");
        Console.WriteLine("4. Get a Category");
        Console.WriteLine("5. Get All Categories");
        Console.WriteLine("6. Return to Main Menu");
        
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Middleware.CreateCategory(_categoryFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "2":
                Middleware.RemoveCategory(_categoryFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "3":
                Middleware.EditCategory(_categoryFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "4":
                Middleware.GetCategory(_categoryFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "5":
                Middleware.GetAllCategories(_categoryFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "6":
                break;
            default:
                Console.WriteLine("Invalid option! Try again!");
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
        }
    }

    private static void ManageOperations()
    {
        Console.Clear();
        Console.WriteLine("-------Manage Operations-------");
        Console.WriteLine("1. Add Operation");
        Console.WriteLine("2. Edit Operation");
        Console.WriteLine("3. Delete Operation");
        Console.WriteLine("4. Get an Operation");
        Console.WriteLine("5. Get all Operations");
        Console.WriteLine("6. Return to Main Menu");
        
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Middleware.CreateOperation(_operationFacade, _categoryFacade, _bankAccountFacade, 
                    _bankAccountRepository, _categoryRepository);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "2" :
                Middleware.EditOperation(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "3":
                Middleware.RemoveOperation(_operationFacade, _bankAccountFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "4":
                Middleware.GetOperation(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "5":
                Middleware.GetAllOperations(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "6":
                break;
            default:
                Console.WriteLine("Invalid option! Try again!");
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
        }
    }

    private static void ManageAnalytics()
    {
        Console.Clear();
        Console.WriteLine("-------Look Analytics-------");
        Console.WriteLine("1. Balance Difference");
        Console.WriteLine("2. Expenses grouped by Category");
        Console.WriteLine("3. Return to Main Menu");
        
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Middleware.GetBalanceDifference(_analyticsService);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "2":
                Middleware.GetExpensesGroupedByCategory(_analyticsService);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "3":
                break;
            default:
                Console.WriteLine("Invalid option! Try again!");
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
        }
    }

    private static void ManageExport()
    {
        Console.Clear();
        Console.WriteLine("-------Export-------");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. YAML");
        Console.WriteLine("4. Return to Main Menu");
        
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Middleware.ExportCsvOperation(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "2":
                Middleware.ExportJsonOperation(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "3":
                Middleware.ExportYamlOperation(_operationFacade);
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
            case "4":
                break;
            default:
                Console.WriteLine("Invalid option! Try again!");
                Console.Write("Touch enter to return to Main Menu");
                Console.ReadLine();
                break;
        }
    }
}