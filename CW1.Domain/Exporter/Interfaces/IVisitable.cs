namespace CW1.Domain.Exporter.Interfaces;

public interface IVisitable
{
    void Accept(IVisitor visitor);
}