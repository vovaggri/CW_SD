using CW1.Domain.DomainClasses;

namespace CW1.Domain.Importer;

public class CsvDataImporter : DataImporter
{
    protected override List<Operation> ParseData(string data)
    {
        var operations = new List<Operation>();
        var lines = data.Split('\n');

        foreach (var operation in lines.Skip(1))
        {
            var parts = operation.Split(';');
            if (parts.Length < 6)
            {
                continue;
            }
        }

        return operations;
    }
}