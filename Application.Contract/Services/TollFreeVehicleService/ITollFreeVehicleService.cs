namespace Application.Contract.Services.TollFreeVehicleService;
public interface ITollFreeVehicleService : IService
{
    Task<bool> IsTollFreeVehicle(string vehicleType);
    IEnumerable<string> GetTollFreeVehicleTypes();
}
