using System.Text.Json;
using CW1.Domain.DomainClasses;

namespace CW1.Domain.Importer;

public class JsonDataImporter : DataImporter
{
    protected override List<Operation> ParseData(string data)
    {
        try
        {
            var records = JsonSerializer.Deserialize<List<Operation>>(data);
            return records ?? new List<Operation>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while parsing json data: {ex.Message}");
            return new List<Operation>();
        }
    }
}