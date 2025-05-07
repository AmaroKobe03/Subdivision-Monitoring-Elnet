using ElnetSubdivi.Services;
using System.Collections.Generic;

namespace ElnetSubdivi.Models
{
    public class BillingPaymentViewModel
    {
        public BillingPaymentModel Billing { get; set; } = new(); // This is for the form data
        public List<BillingPaymentModel> BillStatements { get; set; } = new(); // Existing list of bills
        public List<BillingPaymentService.UserBasicInfoDto> Homeowners { get; set; } = new(); // Optional, if needed in dropdown
        public List<Users> Users { get; set; }
    }
}