using System.Text;
using CW1.Domain.DomainClasses;
using CW1.Domain.Exporter.Interfaces;

namespace CW1.Domain.Exporter;

public class CsvExportVisitor : IVisitor
{
    private readonly string _filePath;

    public CsvExportVisitor(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Visit(List<Operation> operations)
    {
        using StreamWriter writer = new StreamWriter(_filePath);
        {
            writer.WriteLine("ID;Type;BankAccountId;Amount;Date;CategoryId;Description");
            foreach (var op in operations)
            {
                writer.WriteLine($"{op.Id};{op.Type};{op.BankAccountId};{op.Amount};{op.Date};" +
                                 $"{op.CategoryId};{op.Description ?? ""}");
            }

            Console.WriteLine($"Data was exported to file {_filePath} successfully.");
        }
    }
}