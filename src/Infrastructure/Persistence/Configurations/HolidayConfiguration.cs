using EShop.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.Infrastructure.Persistence.Configurations;
public class HolidayConfiguration : BaseConfiguration<Holiday>
{
    public override void Configure(EntityTypeBuilder<Holiday> builder)
    {

        builder.HasData(
                   new Holiday { Id = 1, Month = 1, Day = 1 },
                   new Holiday { Id = 2, Month = 3, Day = 28 },
                   // Add more holiday seed data as needed like Whole days of july
                   new Holiday { Id = 3, Month = 7, Day = 1 },
                   new Holiday { Id = 4, Month = 7, Day = 2 },
                   //..other days of july
                   new Holiday { Id = 35, Month = 7, Day = 30 },
                   new Holiday { Id = 36, Month = 7, Day = 31 }
               );

        base.Configure(builder);
    }
}
