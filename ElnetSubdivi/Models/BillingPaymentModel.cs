using Microsoft.AspNetCore.Mvc;

namespace ElnetSubdivi.Models
{
    public class BillingPaymentModel : Controller
    {
        public string description { get; set; }
        public string amount { get; set; }
        public Dictionary<string, string> Icons { get; set; } = new Dictionary<string, string>();

        public string PageType { get; set; } // Identify which page is using the shared component

        public string ButtonText => PageType == "PayBills" ? "Pay Selectable Bills" : "Add Request";



    }
}
