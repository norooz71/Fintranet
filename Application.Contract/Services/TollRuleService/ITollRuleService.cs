namespace Application.Contract.Services.TollRuleService;
public interface ITollRuleService : IService
{
    decimal GetTollFee(int hour, int minute);
}
