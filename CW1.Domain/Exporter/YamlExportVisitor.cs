using CW1.Domain.DomainClasses;
using CW1.Domain.Exporter.Interfaces;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CW1.Domain.Exporter;

public class YamlExportVisitor : IVisitor
{
    private readonly string? _filePath;

    public YamlExportVisitor(string filePath)
    {
        _filePath = filePath;
    }
    
    public void Visit(List<Operation> operations)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        string yaml = serializer.Serialize(operations);
        File.WriteAllText(_filePath ?? "", yaml);
        Console.WriteLine($"Data about operations was saved to: {_filePath} successfully.");
    }
}