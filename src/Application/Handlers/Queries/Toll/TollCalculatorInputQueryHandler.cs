using Application.Contract.Queries.Toll;
using Application.Contract.Services.Holiday;
using Application.Contract.Services.TollFreeVehicleService;
using Application.Contract.Services.TollRuleService;
using MediatR;

namespace EShop.Application.Handlers.Queries.Toll;
public class TollCalculatorInputQueryHandler : IRequestHandler<TollCalculatorInputQuery, decimal>
{
    private readonly ITollRuleService _tollRuleService;
    private readonly ITollFreeVehicleService _tollFreeVehicleService;
    private readonly IHolidayService _holidayService;

    public TollCalculatorInputQueryHandler(ITollRuleService tollRuleService
        , ITollFreeVehicleService tollFreeVehicleService, IHolidayService holidayService)
    {
        _tollRuleService = tollRuleService;
        _tollFreeVehicleService = tollFreeVehicleService;
        _holidayService = holidayService;
    }

    public async Task<decimal> Handle(TollCalculatorInputQuery request, CancellationToken cancellationToken)
    {
        if (request.PassTimes == null || request.PassTimes.Length == 0)
        {
            return 0; // No passes, no tax
        }

        DateTime intervalStart = request.PassTimes[0];
        decimal totalFee = 0;

        foreach (DateTime passTime in request.PassTimes)
        {
            decimal currentFee = await CalculateTollFee(passTime, request.Vehicle);
            decimal nextPassFee = await CalculateTollFee(intervalStart, request.Vehicle);

            long minutesDifference = (passTime - intervalStart).Minutes;

            if (minutesDifference <= 60)
            {
                if (totalFee > 0) totalFee -= nextPassFee;
                if (currentFee >= nextPassFee) nextPassFee = currentFee;
                totalFee += nextPassFee;
            }
            else
            {
                totalFee += currentFee;
            }
        }

        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private async Task<decimal> CalculateTollFee(DateTime passTime, string vehicle)
    {
        if (IsTollFreeDate(passTime) || await IsTollFreeVehicle(vehicle)) return 0;

        int hour = passTime.Hour;
        int minute = passTime.Minute;

        return _tollRuleService.GetTollFee(hour, minute);
    }
    private bool IsTollFreeDate(DateTime passTime)
    {
        int year = passTime.Year;
        int month = passTime.Month;  
        int day = passTime.Day;

        if (passTime.DayOfWeek == DayOfWeek.Saturday || passTime.DayOfWeek == DayOfWeek.Sunday)
        {
            return true; // Weekend, no tax on Saturdays and Sundays
        }
        #region holiday
        // Define public holidays and days before public holidays
        //List<DateTime> publicHolidays = new List<DateTime>
        //{
        //    new DateTime(year, 1, 1),  // New Year's Day
        //    new DateTime(year, 3, 28), // Example: add other public holidays
        //    // ...
        //};

        //if (publicHolidays.Contains(passTime.Date))
        //{
        //    return true; // Public holiday
        //}

        #endregion


        #region new version of holiday 
        //in this part i get whole holydays and either july day as i seeded them before into databse
        var holidays = _holidayService.GetHolidays()
            .Select(holiday => new DateTime(year, holiday.Month, holiday.Day));

        var publicHolidays = holidays.Select(holiday => new DateTime(year, holiday.Month, holiday.Day));
        
        if (publicHolidays.Contains(passTime.Date))
        {
            return true; // Public holiday
        }


        #endregion 

        // Check for days before public holidays
        foreach (var holiday in publicHolidays)
        {
            if (passTime.Date == holiday.AddDays(-1).Date)
            {
                return true; // Day before a public holiday
            }
        }

        return false;
    }
    private async Task<bool> IsTollFreeVehicle(string vehicle)
    {
        if (vehicle == null) return false;

        return await _tollFreeVehicleService.IsTollFreeVehicle(vehicle);
    }
}
