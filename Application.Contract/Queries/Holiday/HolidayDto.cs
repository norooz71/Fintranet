using Application.Contract.Common.Mappings;
using EShop.Domain;

namespace Application.Contract.Queries;
public class HolidayDto : IMapFrom<Holiday>
{
    public int Day { get; set; }

    public int Month { get; set; }

}
