using System.Linq.Expressions;
using LMS.Core;
using Microsoft.EntityFrameworkCore;

namespace LMS.DataAccess.Repository;

public class Repository<T>:IRepository<T> where T:BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _table;

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();

    }
    public IEnumerable<T?> GetAll(string? includeProperties=null)
    {
        IQueryable<T> query = _table;

        
        return query.ToList();
    }

    public T? Get(Expression<Func<T?, bool>> filter)
    {
        IQueryable<T?> query = _table;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public T? GetById(int? id)
    {
        return _table.FirstOrDefault(u => u.Id == id);
    }

    public void Add(T entity)
    {
        _table.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _table.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _table.Remove(entity);
        _context.SaveChanges();
    }
}