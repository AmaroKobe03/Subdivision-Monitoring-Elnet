using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElnetSubdivi.Models
{
    public class BillingPaymentModel
    {
        [Key]
        public string bill_no { get; set; }
        [NotMapped]
        [BindNever]
        public string description { get; set; } = "Default description";
        [NotMapped]
        public string amount { get; set; } = "0.00";
        [NotMapped]
        public Dictionary<string, string> Icons { get; set; } = new Dictionary<string, string>();
        [NotMapped]
        public string PageType { get; set; } = "";
        [NotMapped]
        public string ButtonText => PageType == "PayBills" ? "Pay Selectable Bills" : "Add Request";

        // Properties matching database fields
        public string user_id { get; set; }
        public string user_name { get; set; } // Optional: for display purposes
        public string bill_type { get; set; }
        public DateTime billing_period { get; set; }
        public int amount_due { get; set; }
        public DateTime due_date { get; set; }
        public string bill_status { get; set; } // 'Overdue', 'Pending', 'Paid'

        public BillingPaymentModel()
        {
            bill_no = string.Empty;
            user_id = string.Empty;
            user_name = string.Empty; // Optional: for display purposes
            bill_type = string.Empty;
            billing_period = DateTime.MinValue;
            amount_due = 0;
            due_date = DateTime.MinValue;
            bill_status = string.Empty;
        }
    }
}
