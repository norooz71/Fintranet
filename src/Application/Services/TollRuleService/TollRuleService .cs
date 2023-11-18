using Application.Contract.Services.TollRuleService;
using EShop.Domain;

namespace EShop.Application.Services.TollRuleService;
public class TollRuleService : Service, ITollRuleService
{
    private readonly ITollRuleRepository _tollRuleRepository;

    public TollRuleService(ITollRuleRepository tollRuleRepository)
    {
        _tollRuleRepository = tollRuleRepository;
    }

    public decimal GetTollFee(int hour, int minute)
    {
        var rule = _tollRuleRepository.GetBy(toll => toll.Hour == hour && toll.Minute == minute)
            .ToList()
            .FirstOrDefault();

        return rule?.Fee ?? 0;
    }
}