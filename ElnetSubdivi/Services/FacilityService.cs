using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElnetSubdivi.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly ApplicationDbContext _context;

        public FacilityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Facility>> GetAllFacilitiesWithHoursAsync()
        {
            return await _context.Facilities
                .Include(f => f.OperatingHours)
                .ToListAsync();
        }


        public async Task AddFacilityAsync(Facility facility, List<FacilityOperatingHour> operatingHours)
        {
            if (facility == null)
                throw new ArgumentNullException(nameof(facility));

            // 🔐 Generate new Facility_Id
            var lastId = await _context.Facilities
                .Where(f => f.Facility_Id.StartsWith("FAC-"))
                .OrderByDescending(f => f.Facility_Id)
                .Select(f => f.Facility_Id)
                .FirstOrDefaultAsync();

            int nextId = 1;
            if (!string.IsNullOrEmpty(lastId))
            {
                var idPart = lastId.Replace("FAC-", "");
                if (int.TryParse(idPart, out int parsedId))
                    nextId = parsedId + 1;
            }

            facility.Facility_Id = $"FAC-{nextId:D4}";

            // 🕐 Link OperatingHours if any
            if (facility.OperatingHours != null && facility.OperatingHours.Any())
            {
                foreach (var hour in facility.OperatingHours)
                {
                    hour.FacilityId = facility.Facility_Id; // Set FK
                }

                // Fix for CS8604: Ensure OperatingHours is not null before calling AddRangeAsync
                await _context.FacilityOperatingHours.AddRangeAsync(operatingHours);
            }

            await _context.Facilities.AddAsync(facility);

            await _context.SaveChangesAsync();
        }


        public async Task<Facility> GetLastFacilityAsync()
        {
            try
            {
                var facilities = await _context.Facilities
                    .Select(f => new Facility
                    {
                        Facility_Id = f.Facility_Id ?? "",
                        Facility_Name = f.Facility_Name ?? "",
                        Facility_Category = f.Facility_Category ?? "",
                        Facility_Description = f.Facility_Description ?? "",
                        Facility_Img = f.Facility_Img ?? "",
                        Service_Fee_Per_Hour = f.Service_Fee_Per_Hour,
                        Facility_Guidelines = f.Facility_Guidelines ?? "",
                        Facility_Aminities = f.Facility_Aminities ?? "",
                        Facility_Status = f.Facility_Status ?? "Available",
                        OperatingHours = f.OperatingHours ?? new List<FacilityOperatingHour>()
                    })
                    .ToListAsync();

                // Get the facility with the highest numeric ID
                var lastFacility = facilities
                    .Where(f => f.Facility_Id.StartsWith("FAC-"))
                    .OrderByDescending(f =>
                    {
                        var idPart = f.Facility_Id.Replace("FAC-", "");
                        return int.TryParse(idPart, out int num) ? num : 0;
                    })
                    .FirstOrDefault();

                return lastFacility;
            }
            catch (Exception ex)
            {
                // Log errors and return null
                Console.WriteLine($"Error fetching last facility: {ex.Message}");
                return null;
            }
        }
    }
}
