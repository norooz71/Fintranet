using EShop.Domain;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.RepositoryService;
public class TollFreeVehicleRepository : BaseRepository<TollFreeVehicle>, ITollFreeVehicleRepository
{
    private readonly DbSet<TollFreeVehicle> _entity;
    public TollFreeVehicleRepository(ApplicationDbContext context) : base(context)
    {
        this._entity = context.Set<TollFreeVehicle>();
    }
}

