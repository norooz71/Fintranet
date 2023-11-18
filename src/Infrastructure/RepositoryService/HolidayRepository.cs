using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;
public class HolidayRepository : BaseRepository<Holiday>, IHolidayRepository
{
    private readonly DbSet<Holiday> _entity;
    public HolidayRepository(ApplicationDbContext context) : base(context)
    {
        this._entity = context.Set<Holiday>();
    }
}
