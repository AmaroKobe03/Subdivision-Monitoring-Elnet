using ElnetSubdivi.Services;
using ElnetSubdivi.ViewModels;

namespace ElnetSubdivi.Models
{
    public class StatisticsViewModel
    {
        public BillingPaymentModel Billing { get; set; } = new(); // This is for the form data
        public List<BillingPaymentModel> BillStatements { get; set; } = new(); // Existing list of bills
        public List<Users> Users { get; set; }
        public List<Payments> Payments { get; set; } = new();
        public List<Feedback> Feedback { get; set; } = new();
        public List<IncidentReports> IncidentReports { get; set; } = new();
        public List<VisitorList> VisitorList { get; set; } = new();
        public List<Vehicle> Vehicle { get; set; } = new();
        public List<ServiceRequest> ServiceRequests { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
        public List<ElnetSubdivi.Models.Post> Posts { get; set; }
        public List<Facility> Facilities { get; set; } = new();
        public List<dynamic> DashboardData { get; set; }
    }
}
