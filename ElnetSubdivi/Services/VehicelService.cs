using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Views.Shared;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class VehicelService : IVehicleService
    {
        private readonly ApplicationDbContext _context;
        public VehicelService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            try
            {
                var vehicles = await _context.Vehicle.ToListAsync();
                return vehicles;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching vehicles: {ex.Message}");
                return new List<Vehicle>();
            }
        }
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}