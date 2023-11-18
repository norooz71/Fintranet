using System.Reflection;
using EShop.Domain;
using EShop.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Persistence;

public class ApplicationDbContext:DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    public ApplicationDbContext(DbContextOptions options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        :base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TollRule> TollRules { get; set; }
    public DbSet<TollFreeVehicle> TollFreeVehicles { get; set; }
    public DbSet<Holiday> Holidays { get; set; }

}
