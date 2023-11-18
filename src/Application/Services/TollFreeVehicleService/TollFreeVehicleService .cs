using Application.Contract.Services.TollFreeVehicleService;
using EShop.Domain;

namespace EShop.Application.Services.TollFreeVehicleService;
public class TollFreeVehicleService : Service, ITollFreeVehicleService
{
    private readonly ITollFreeVehicleRepository _tollFreeVehicleRepository;

    public TollFreeVehicleService(ITollFreeVehicleRepository tollFreeVehicleRepository)
    {
        _tollFreeVehicleRepository = tollFreeVehicleRepository;
    }

    public async Task<bool> IsTollFreeVehicle(string vehicleType)
    {
        return  await _tollFreeVehicleRepository.AnyAsync(v => v.VehicleType == vehicleType);
    }

    public IEnumerable<string> GetTollFreeVehicleTypes()
    {
        return _tollFreeVehicleRepository.Get().Select(v => v.VehicleType);
    }
}