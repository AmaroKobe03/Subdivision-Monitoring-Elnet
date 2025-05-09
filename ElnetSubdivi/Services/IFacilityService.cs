using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IFacilityService
    {
        Task<List<Facility>> GetAllFacilitiesWithHoursAsync();
        Task AddFacilityAsync(Facility facility, List<FacilityOperatingHour> operatingHours);
        Task<Facility> GetLastFacilityAsync();
    }
}

