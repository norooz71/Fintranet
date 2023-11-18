using System.Linq.Expressions;

namespace EShop.Domain.Common;

public interface IRepository<T> where T : BaseEntity
{

    

    Task<T> GetAsync(long id);
    Task<T> GetLast();
    void Insert(T entity);
    Task<long> InsertAndGetId(T entity);
    void Update(T entity);
    void Delete(int id);
    IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get();
    Task<int> SaveChangesAsync();



}



