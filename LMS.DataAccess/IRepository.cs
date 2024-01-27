using System.Linq.Expressions;
using LMS.Core;

namespace LMS.DataAccess;

public interface IRepository<T> where T:BaseEntity
{
    IEnumerable<T?> GetAll(string? includeProperties=null);
    T? Get(Expression<Func<T?, bool>> filter);
    public T? GetById(int? id);
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}