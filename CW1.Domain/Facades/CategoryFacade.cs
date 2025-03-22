using CW1.Domain.DomainClasses;
using CW1.Domain.Factories;
using CW1.Domain.Repositories.Interfaces;

namespace CW1.Domain.Facades;

public class CategoryFacade
{
    private readonly ICategoryRepository _repository;
    private readonly DomainFactory _factory;

    public CategoryFacade(ICategoryRepository repository, DomainFactory factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public Category Create(CategoryType type, string name)
    {
        var category = _factory.CreateCategory(type, name);
        _repository.Add(category);
        return category;
    }

    public void DeleteCategory(Guid categoryId)
    {
        _repository.Delete(categoryId);
    }

    public Category? GetCategory(Guid categoryId)
    {
        return _repository.GetById(categoryId);
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _repository.GetAll();
    }
}