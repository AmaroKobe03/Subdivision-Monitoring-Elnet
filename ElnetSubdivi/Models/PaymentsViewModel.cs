using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using ElnetSubdivi.ViewModels;
using Microsoft.AspNetCore.Http; // Ensure this namespace is included fo

namespace ElnetSubdivi.Models
{
    public class PaymentsViewModel
    {
        public List<Payments> Payments { get; set; } = new();
        public List<Users> Users { get; set; }
        public BillingPaymentModel Billing { get; set; } = new(); // This is for the form data
        public List<BillingPaymentModel> BillStatements { get; set; } = new(); // Existing list of bills
        public List<BillingPaymentService.UserBasicInfoDto> Homeowners { get; set; } = new(); // Optional, if needed in dropdown
    }
}
