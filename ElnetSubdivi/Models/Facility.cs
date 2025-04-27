using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class Facility
    {
        [Key]
        [BindNever]
        public string Facility_Id { get; set; }
        public string Facility_Name { get; set; }
        public string Facility_Category { get; set; }
        public string Facility_Description { get; set; }
        public string Facility_Img { get; set; }
        public decimal Service_Fee_Per_Hour { get; set; }
        public string Facility_Guidelines { get; set; }
        public string Facility_Aminities { get; set; }
        public int TypeOf_Status { get; set; }

        public ICollection<FacilityOperatingHour> OperatingHours { get; set; }
    }
}
