using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class Facility
    {
        [Key]
        public string Facility_Id { get; set; }
        public string Facility_Name { get; set; }
        public string Facility_Category { get; set; }
        public string Facility_Description { get; set; }
        public string Facility_Img { get; set; }
        public decimal Service_Fee_Per_Hour { get; set; }
        public string Facility_Guidelines { get; set; }
        public string Facility_Aminities { get; set; }
        public string Facility_Status { get; set; }

        public ICollection<FacilityOperatingHour> OperatingHours { get; set; }

        public Facility()
        {
            Facility_Id = string.Empty;
            Facility_Name = string.Empty;
            Facility_Category = string.Empty;
            Facility_Description = string.Empty;
            Facility_Img = string.Empty;
            Service_Fee_Per_Hour = 0m;
            Facility_Guidelines = string.Empty;
            Facility_Aminities = string.Empty;
            Facility_Status = "Available"; // Default status
            OperatingHours = new List<FacilityOperatingHour>();
        }
    }
}
