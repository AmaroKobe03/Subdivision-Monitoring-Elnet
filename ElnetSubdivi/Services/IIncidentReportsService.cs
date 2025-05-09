using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IIncidentReportsService
    {
        Task<List<IncidentReports>> GetAllIncidentReportsAsync();
        Task AddIncidentReportAsync(IncidentReports report);
    }
}