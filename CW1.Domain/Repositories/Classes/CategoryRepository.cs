using CW1.Domain.DomainClasses;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Repositories.Classes;

public class CategoryRepository : ICategoryRepository
{
    private readonly Dictionary<Guid, Category> _categories = new Dictionary<Guid, Category>();
    
    public void Add(Category category)
    {
        _categories[category.Id] = category;
    }

    public void Update(Category category)
    {
        _categories[category.Id] = category;
    }

    public void Delete(Guid categoryId)
    {
        _categories.Remove(categoryId);
    }

    public Category? GetById(Guid categoryId)
    {
        _categories.TryGetValue(categoryId, out var category);
        return category;
    }

    public IEnumerable<Category> GetAll()
    {
        return _categories.Values;
    }
}