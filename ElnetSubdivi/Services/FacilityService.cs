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

        public async Task AddFacilityAsync(Facility facility)
        {
            if (facility == null)
                throw new ArgumentNullException(nameof(facility));

            // Ensure OperatingHours collection is initialized
            if (facility.OperatingHours != null && facility.OperatingHours.Any())
            {
                foreach (var hour in facility.OperatingHours)
                {
                    hour.Facility = facility; // Link back if needed
                }
            }

            await _context.Facilities.AddAsync(facility);
            await _context.SaveChangesAsync();
        }

        public async Task<Facility> GetLastFacilityAsync()
        {
            var facilities = await _context.Facilities
                .Where(f => f.Facility_Id.Trim().StartsWith("FAC-"))
                .ToListAsync(); // fetches from DB into memory

            return facilities
                .OrderByDescending(f =>
                {
                    var idPart = f.Facility_Id.Replace("FAC-", "");
                    return int.TryParse(idPart, out int num) ? num : 0;
                })
                .FirstOrDefault(); // parses and sorts in memory
        }



    }
}
