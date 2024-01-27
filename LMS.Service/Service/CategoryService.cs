using LMS.Core;
using LMS.DataAccess;

namespace LMS.Service.Service;

public class CategoryService:IService<Category>
{
    private readonly IRepository<Category> _repository;
    public CategoryService(IRepository<Category> repository)
    {
        _repository = repository;
    }
    public IEnumerable<Category?> GetAllDocs()
    {
        return _repository.GetAll();
    }

   

    public Category? GetDocById(int? id)
    {
        return _repository.GetById(id);
    }

    public void AddDoc(Category entity)
    {
        _repository.Add(entity);
    }

    public void UpdateDoc(Category entity)
    {
        _repository.Update(entity);
    }

    public void DeleteDoc(Category entity)
    {
        _repository.Delete(entity);
    }
}