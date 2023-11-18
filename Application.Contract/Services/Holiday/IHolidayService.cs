using Application.Contract.Queries;

namespace Application.Contract.Services.Holiday;
public interface IHolidayService : IService
{
    IEnumerable<HolidayDto> GetHolidays();
}
