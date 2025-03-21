using System;
using System.IO;
using CW1.Domain.DomainClasses;

namespace CW1.Domain.Importer;

public abstract class DataImporter
{
    public void Import(string filePath) 
    {
        var data = ReadFile(filePath);
    }

    protected string ReadFile(string filePath)
    {
        return File.ReadAllText(filePath);
    }

    protected abstract List<Operation> ParseData(string data);
}