using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElnetSubdivi.Services
{
    public class ServiceRequestService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceRequestManagementViewModel> GetServiceRequestViewModelAsync()
        {
            var requests = await _context.Service_Request
                .OrderByDescending(r => r.Request_Creation)
                .ToListAsync();

            var summaryCards = new List<SummaryCard>
            {
                new() {
                    Title = "Pending",
                    Count = requests.Count(r => r.Request_Status == "Pending"),
                    Icon = "pending.svg",
                    BorderColor = "border-yellow-500"
                },
                new() {
                    Title = "Approved",
                    Count = requests.Count(r => r.Request_Status == "Approved"),
                    Icon = "approved.svg",
                    BorderColor = "border-green-500"
                },
                new() {
                    Title = "Declined",
                    Count = requests.Count(r => r.Request_Status == "Declined"),
                    Icon = "declined.svg",
                    BorderColor = "border-red-500"
                }
            };

            return new ServiceRequestManagementViewModel
            {
                ServiceRequests = requests,
                SummaryCards = summaryCards
            };
        }
    }
}