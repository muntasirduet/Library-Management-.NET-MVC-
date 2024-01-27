using LMS.Core;
using LMS.DataAccess;

namespace LMS.Service.Service;

public class BookService:IService<Book>
{
    private readonly IRepository<Book> _repository;
    public BookService(IRepository<Book> repository)
    {
        _repository = repository;
    }
    public IEnumerable<Book?> GetAllDocs()
    {
        return _repository.GetAll(includeProperties:"category");
    }

    public Book? GetDocById(int? id)
    {
        return _repository.GetById(id);
    }

    public void AddDoc(Book entity)
    {
        _repository.Add(entity);
    }

    public void UpdateDoc(Book entity)
    {
        var objFromDb = _repository.GetById(entity.Id);
        if (objFromDb != null)
        {
            objFromDb.Title = entity.Title;
            objFromDb.Author = entity.Author;
            objFromDb.Publication = entity.Publication;
            objFromDb.CategoryId = entity.CategoryId;
            if (entity.ImageUrl != null)
            {
                objFromDb.ImageUrl = entity.ImageUrl;
            } 
            
        }
        _repository.Update(objFromDb);
    }

    public void DeleteDoc(Book entity)
    {
        _repository.Delete(entity);
    }
}