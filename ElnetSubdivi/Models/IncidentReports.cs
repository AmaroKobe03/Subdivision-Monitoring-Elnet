using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElnetSubdivi.Models
{
    public class IncidentReports
    {
        [Key]
        [Required]
        public string report_id { get; set; }

        [Required]
        public string user_id { get; set; }

        [Required]
        [StringLength(50)]
        public string report_title { get; set; }

        [Required]
        [StringLength(50)]
        public string report_description { get; set; }

        public byte[] report_image { get; set; }

        [Required]
        public DateTime creation_date { get; set; }

        [Required]
        [StringLength(50)]
        public string report_status { get; set; }

        public IncidentReports()
        {
            report_id = string.Empty;
            user_id = string.Empty;
            report_title = string.Empty;
            report_description = string.Empty;
            report_image = null;
            creation_date = DateTime.Now;
            report_status = "Pending"; // Default status
        }
    }
}
