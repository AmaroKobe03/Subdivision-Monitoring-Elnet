using ElnetSubdivi.Models;
using System.Collections.Generic;
using static ElnetSubdivi.Services.VehicelService;

namespace ElnetSubdivi.Services
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task AddVehicleAsync(Vehicle vehicle);
    }
}
