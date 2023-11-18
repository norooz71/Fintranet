using Application.Contract.Queries;
using Application.Contract.Services.Holiday;
using AutoMapper;
using EShop.Domain;

namespace EShop.Application.Services.Holiday;
public class HolidayService : Service, IHolidayService
{
    private readonly IHolidayRepository _holidayRepository;
    private readonly IMapper _mapper;
    public HolidayService(IHolidayRepository holidayRepository, IMapper mapper)
    {
        _holidayRepository = holidayRepository;
        _mapper = mapper;
    }

    public IEnumerable<HolidayDto> GetHolidays()
    {
        var holidays = _holidayRepository.Get().ToList();   

        var result = _mapper.Map<IEnumerable<HolidayDto>>(holidays);

        return result;
    }
}
