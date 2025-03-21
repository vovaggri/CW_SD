using CW1.Domain.DomainClasses;
using YamlDotNet.Serialization;

namespace CW1.Domain.Importer;

public class YamlDataImporter : DataImporter
{
    protected override List<Operation> ParseData(string data)
    {
        try
        {
            var deserializer = new DeserializerBuilder().Build();
            var records = deserializer.Deserialize<List<Operation>>(data);
            return records;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing data: {ex.Message}");
            return new List<Operation>();
        }
    }
}