using System.Text.Json;
using CW1.Domain.DomainClasses;
using CW1.Domain.Exporter.Interfaces;

namespace CW1.Domain.Exporter;

public class JsonExportVisitor : IVisitor
{
    private readonly string? _filePath;

    public JsonExportVisitor(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Visit(List<Operation> operations)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(operations, options);
        File.WriteAllText(_filePath ?? "", json);
        Console.WriteLine($"Json was exported to file: {_filePath} successfully.");
    }
}