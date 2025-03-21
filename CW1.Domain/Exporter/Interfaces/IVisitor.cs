using CW1.Domain.DomainClasses;

namespace CW1.Domain.Exporter.Interfaces;

public interface IVisitor
{
    void Visit(List<Operation> operations);
}