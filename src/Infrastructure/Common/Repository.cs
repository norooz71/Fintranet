using System.Linq.Expressions;
using EShop.Domain.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace EShop.Infrastructure.Common;

public class BaseRepository<T> : IRepository<T> where T : BaseAuditableEntity

{

    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entity;
    string errorMessage = string.Empty;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;

        _entity = context.Set<T>();
    }

    public virtual async Task<T> GetLast()
    {
        return await _entity.OrderByDescending(r => r.Id).FirstAsync();
    }

    public async Task<T> GetAsync(long id)
    {
        return await _entity.SingleOrDefaultAsync(s => s.Id == id);
    }

    public void Insert(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        //entity.Created = DateTime.Now;
        //entity.CreatedBy = currentUserId;
        
        _entity.Add(entity);
    }

    public async Task<long> InsertAndGetId(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        //entity.Created = DateTime.Now;
        //entity.CreatedBy = currentUserId;
        await _entity.AddAsync(entity);
        await SaveChangesAsync();
        return entity.Id;

    }

    public void Update(T entity)
    {

        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        //entity.LastModified = DateTime.Now;
        //entity.LastModifiedBy = currentUserId;
    }

    public async Task Delete(int id)
    {
        var entity =await GetAsync(id);
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        //entity.Deleted = DateTime.Now;
        //entity.DeletedBy = currentUserId;
        _entity.Remove(entity);

    }

    public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return _entity.Where(predicate);
    }

    public IQueryable<T> Get()
    {
        return _entity.AsQueryable();
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entity.AnyAsync(predicate);
    }

    void IRepository<T>.Delete(int id)
    {
        throw new NotImplementedException();
    }
}
