using System.Linq.Expressions;

namespace LMS.Service;

public interface IService<T>
{
    IEnumerable<T?> GetAllDocs();
    public T? GetDocById(int? id);
    public void AddDoc(T entity);
    public void UpdateDoc(T entity);
    public void DeleteDoc(T entity);
}