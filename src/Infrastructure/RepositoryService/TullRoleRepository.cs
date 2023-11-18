using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;
public class TullRoleRepository : BaseRepository<TollRule>, ITollRuleRepository
{
    private readonly DbSet<TollRule> _entity;
    public TullRoleRepository(ApplicationDbContext context) : base(context)
    {
        this._entity = context.Set<TollRule>();
    }
}

