using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElnetSubdivi.Services
{
    public class IncidentReportsService : IIncidentReportsService
    {
        private readonly ApplicationDbContext _context;
        public IncidentReportsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<IncidentReports>> GetAllIncidentReportsAsync()
        {
            return await _context.Incident_Reports.ToListAsync();
        }
        public async Task AddIncidentReportAsync(IncidentReports report)
        {
            if (report == null)
                throw new ArgumentNullException(nameof(report));
            await _context.Incident_Reports.AddAsync(report);
            await _context.SaveChangesAsync();
        }
    }
}
