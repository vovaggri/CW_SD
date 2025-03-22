using System.Globalization;
using CW1.Domain.Analytics;
using CW1.Domain.CommandAndDecorator;
using CW1.Domain.DomainClasses;
using CW1.Domain.Facades;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace MenuLibrary;

public static class Middleware
{
    public static void CreateBankAccount(BankAccountFacade bankAccountFacade)
    {
        string? name;
        bool flag = false;
        do
        {
            Console.Write("Type the name of the bank account: ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("The name of bank account should not be empty.");
                continue;
            }
            flag = true;
        } while (!flag);
        
        flag = false;
        decimal balance;
        do
        {
            Console.Write("Enter the balance of the bank account: ");
            if (decimal.TryParse(Console.ReadLine(), out balance) && balance > 0)
            {
                flag = true;
            }
            else
            {
                Console.WriteLine("The balance of the bank account is invalid.");
            }
        } while (!flag);
        
        var createCommand = new CreateBankAccountCommand(bankAccountFacade, name ?? "anon", balance);
        var timedCommand = new TimingCommandDecorator(createCommand);
        timedCommand.Execute();
    }

    public static void UpdateBankAccount(BankAccountFacade bankAccountFacade)
    {
        Console.Write("Enter the id of the bank account: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            bool flagName = false;
            try
            {
                string? newName;
                do
                {
                    Console.Write("Enter the new name of the bank account: ");
                    newName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        Console.WriteLine("The new name of bank account should not be empty.");
                        continue;
                    }

                    flagName = true;
                } while (!flagName);

                bankAccountFacade.UpdateAccount(id, newName ?? "anon");
                Console.WriteLine("Edited successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again later.");
            }
        }
        else
        {
            Console.WriteLine("The id of the bank account is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void DeleteBankAccount(BankAccountFacade bankAccountFacade)
    {
        Console.Write("Enter the id of the bank account: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            bankAccountFacade.DeleteAccount(id);
            Console.WriteLine("Deleted successfully.");
        }
        else
        {
            Console.WriteLine("The id of the bank account is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void GetAllBankAccounts(BankAccountFacade bankAccountFacade)
    {
        IEnumerable<BankAccount> bankAccounts = bankAccountFacade.GetAllAccounts();
        List<BankAccount> bankAccountsList = bankAccounts.ToList();

        if (bankAccountsList.Count == 0)
        {
            Console.WriteLine("No bank accounts found.");
            return;
        }
        Console.WriteLine("All bank accounts:");
        foreach (var bankAccount in bankAccountsList)
        {
            Console.WriteLine($"ID: {bankAccount.Id}, Name: {bankAccount.Name}, Balance: {bankAccount.Balance}");
        }
        Console.WriteLine();
    }

    public static void CreateCategory(CategoryFacade categoryFacade)
    {
        string? name;
        bool flag = false;
        do
        {
            Console.Write("Type the name of the category: ");
            name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("The name of category should not be empty.");
                continue;
            }
            flag = true;
        } while (!flag);
        
        flag = false;
        // For initializing gave the type Income, but then it will change to the correct type.
        CategoryType categoryType = CategoryType.Income;
        do
        {
            Console.Write("Select the operation type:\n1. Income\n2. Expense\nYour choice: ");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                categoryType = CategoryType.Income;
                flag = true;
            }
            else if (choice == "2")
            {
                categoryType = CategoryType.Expense;
                flag = true;
            }
            else
            {
                Console.WriteLine("Invalid input.");
                Console.WriteLine("Please try again.");
            }
        } while (!flag);
        
        var createCommand = new CreateCategoryCommand(categoryFacade, categoryType, name ?? "anon");
        var timedCommand = new TimingCommandDecorator(createCommand);

        Console.WriteLine($"Category Type: \"{categoryType}\" was created successfully.");
        timedCommand.Execute();
    }

    public static void RemoveCategory(CategoryFacade categoryFacade)
    {
        Console.Write("Enter the id of the Category: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            categoryFacade.DeleteCategory(id);
            Console.WriteLine("Deleted successfully.");
        }
        else
        {
            Console.WriteLine("The id of the category is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void GetAllCategories(CategoryFacade categoryFacade)
    {
        IEnumerable<Category> categories = categoryFacade.GetAllCategories();
        List<Category> categoriesList = categories.ToList();

        if (categoriesList.Count == 0)
        {
            Console.WriteLine("No categories found.");
            return;
        }
        
        Console.WriteLine("All categories:");
        foreach (var category in categoriesList)
        {
            Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Type: {category.Type}");
        }
        Console.WriteLine();
    }

    public static void GetCategory(CategoryFacade categoryFacade)
    {
        Console.Write("Enter the id of the category: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var category = categoryFacade.GetCategory(id);
            if (category == null)
            {
                Console.WriteLine("No category found.");
                return;
            }
            Console.WriteLine($"ID: {category.Id}, Name: {category.Name}, Type: {category.Type}");
        }
        else
        {
            Console.WriteLine("The id of the category is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void CreateOperation(OperationFacade operationFacade, CategoryFacade categoryFacade, 
        BankAccountFacade bankAccountFacade, IBankAccountRepository bankAccountRepository, 
        ICategoryRepository categoryRepository)
    {
        if (bankAccountFacade.GetAllAccounts().Count() == 0)
        {
            Console.WriteLine("No bank accounts found.");
            return;
        }
        
        if (categoryFacade.GetAllCategories().Count() == 0)
        {
            Console.WriteLine("No categories found.");
            return;
        }

        OperationType type;
        while (true) 
        {
            Console.Write("Choose an operation:\n1. Income,\n2. Expense\nYour choice: ");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                type = OperationType.Income;
                break;
            }
            else if (choice == "2")
            {
                type = OperationType.Expense;
                break;
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        
        GetAllBankAccounts(bankAccountFacade);

        Guid id; 
        BankAccount? bankAccount; 
        while (true)
        {
            Console.Write("Enter the id of bank account for operation: ");
            if (!Guid.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid input."); 
                continue;
            }

            bankAccount = bankAccountRepository.GetById(id);
            if (bankAccount == null)
            {
                Console.WriteLine("No bank account found. Try again.");
                continue;
            }
            break;
        }
        
        decimal amount; 
        while (true) 
        { 
            Console.Write("Enter the amount for operation: ");
            if (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0) 
            { 
                Console.WriteLine("Invalid input."); 
                continue;
            } 
            break;
        }
        
        GetAllCategories(categoryFacade); 
        Guid categoryId; 
        Category? category;
        while (true) 
        { 
            Console.Write("Enter the id of category: "); 
            if (!Guid.TryParse(Console.ReadLine(), out categoryId)) 
            { 
                Console.WriteLine("Invalid input."); 
                continue;
            } 
            category = categoryRepository.GetById(categoryId); 
            if (category == null) 
            { 
                Console.WriteLine("No category found. Try again."); 
                continue;
            } 
            break;
        }
        
        Console.Write("Enter the description of operation (optional): "); 
        var description = Console.ReadLine();

        try
        {
            var createCommand = new CreateOperationCommand(operationFacade, type, id, categoryId, amount,
                DateTime.Now, description ?? "");
            bankAccountFacade.ChangeBalance(id, amount, type);
            var timedCommand = new TimingCommandDecorator(createCommand);
            timedCommand.Execute();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void GetOperation(OperationFacade operationFacade)
    {
        Console.Write("Enter the id of the operation: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            var operation = operationFacade.GetOperation(id);
            if (operation == null)
            {
                Console.WriteLine("No category found.");
                return;
            }

            Console.WriteLine($"Operation ID: {operation.Id}");
            Console.WriteLine($"Bank Account Id: {operation.BankAccountId}, CategoryId: {operation.CategoryId} Amount: {operation.Amount}, " +
                              $"\nDate: {operation.Date}, Description: {operation.Description}");
        }
        else
        {
            Console.WriteLine("The id of the operation is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void RemoveOperation(OperationFacade operationFacade, BankAccountFacade bankAccountFacade)
    {
        Console.Write("Enter the id of the Operation: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            try
            {
                operationFacade.DeleteOperation(id, bankAccountFacade);
                Console.WriteLine("Deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again later.");
            }
        }
        else
        {
            Console.WriteLine("The id of the operation is invalid.");
            Console.WriteLine("Please try again later.");
        }
    }

    public static void GetAllOperations(OperationFacade operationFacade)
    {
        IEnumerable<Operation> operations = operationFacade.GetAllOperations();
        List<Operation> operationsList = operations.ToList();

        if (operationsList.Count == 0)
        {
            Console.WriteLine("No categories found. Try again later.");
            return;
        }

        Console.WriteLine("Operations:");
        foreach (var operation in operationsList)
        {
            Console.WriteLine($"Operation ID: {operation.Id}");
            Console.WriteLine($"Bank Account Id: {operation.BankAccountId}, CategoryId: {operation.CategoryId} Amount: {operation.Amount}, " +
                              $"\nDate: {operation.Date}, Description: {operation.Description}");
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public static void EditCategory(CategoryFacade categoryFacade)
    {
        Console.WriteLine("Enter the id of the category: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            try
            {
                Console.Write("Enter the new name of the category: ");
                string newName = Console.ReadLine();
                categoryFacade.EditCategory(id, newName ?? "anon");
                Console.WriteLine("Updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again later.");
            }
        }
    }

    public static void EditOperation(OperationFacade operationFacade)
    {
        Console.WriteLine("Enter the id of the operation: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            try
            {
                Console.Write("Enter the new description: ");
                string? newDescription = Console.ReadLine();
                operationFacade.EditOperation(id, newDescription ?? "");
                Console.WriteLine("Updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please try again later.");
            }
        }
    }

    public static void GetBalanceDifference(AnalyticsService analyticsService)
    {
        DateTime startDate;
        while (true)
        {
            Console.Write("Enter start date (example, 01.01.2025): ");
            string inputStartDate = Console.ReadLine();
            if (!DateTime.TryParseExact(inputStartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            break;
        }

        DateTime endDate;
        while (true)
        {
            Console.Write("Enter end date (example, 01.01.2025): ");
            string inputEndDate = Console.ReadLine();
            if (!DateTime.TryParseExact(inputEndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            break;
        }
        
        decimal difference = analyticsService.GetBalanceDifference(startDate, endDate);
        Console.WriteLine($"Difference: {difference}");
    }

    public static void GetExpensesGroupedByCategory(AnalyticsService analyticsService)
    {
        DateTime startDate;
        while (true)
        {
            Console.Write("Enter start date (example, 01.01.2025): ");
            string inputStartDate = Console.ReadLine();
            if (!DateTime.TryParseExact(inputStartDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            break;
        }

        DateTime endDate;
        while (true)
        {
            Console.Write("Enter end date (example, 01.01.2025): ");
            string inputEndDate = Console.ReadLine();
            if (!DateTime.TryParseExact(inputEndDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }
            break;
        }
        
        var expenses = analyticsService.GetExpensesGroupedByCategory(startDate, endDate);

        if (expenses.Count == 0)
        {
            Console.WriteLine("No categories found on this time.");
        }
        else
        {
            Console.WriteLine("Expenses:");
            foreach (var item in expenses)
            {
                Console.WriteLine($"Category ID: {item.Key}");
            }
        }
    }
}