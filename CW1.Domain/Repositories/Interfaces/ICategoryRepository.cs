using System.Net.Sockets;
using CW1.Domain.DomainClasses;

namespace CW1.Domain.Repositories.Interfaces;

public interface ICategoryRepository
{
    void Add(Category category);
    void Update(Category category);
    void Delete(Guid categoryId);
    Category? GetById(Guid categoryId);
    IEnumerable<Category> GetAll();
}