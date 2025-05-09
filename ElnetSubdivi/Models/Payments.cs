using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElnetSubdivi.Models
{
    public class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int payment_id { get; set; } // Auto-generated in DB via trigger

        [Required]
        [StringLength(50)]
        public string user_id { get; set; }

        [Required]
        [StringLength(50)]
        public string bill_no { get; set; }

        [Required]
        [StringLength(100)]
        public string bill_type { get; set; }

        [Required]
        public DateTime bill_period { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal amount { get; set; }

        [Required]
        [StringLength(50)]
        public string payment_method { get; set; }

        public DateTime payment_date { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string? card_number { get; set; }

        [StringLength(100)]
        public string? name { get; set; }

        [StringLength(10)]
        public string? expiry_date { get; set; }

        [StringLength(5)]
        public string? cvv { get; set; }

        public Payments()
        {
            payment_id = 0;
            user_id = string.Empty;
            bill_no = string.Empty;
            bill_type = string.Empty;
            bill_period = DateTime.MinValue;
            amount = 0.0m;
            payment_method = string.Empty;
            payment_date = DateTime.Now;
            card_number = null;
            name = null;
            expiry_date = null;
            cvv = null;
        }
    }
}
