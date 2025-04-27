// ServiceRequestManagementViewModel.cs
namespace ElnetSubdivi.Models
{
    public class ServiceRequestManagementViewModel
    {
        public List<ServiceRequest> ServiceRequests { get; set; } = new();
        public List<SummaryCard> SummaryCards { get; set; } = new();
    }
}