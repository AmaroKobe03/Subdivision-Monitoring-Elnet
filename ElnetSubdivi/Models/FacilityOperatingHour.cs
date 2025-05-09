using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class FacilityOperatingHour
    {
        [ForeignKey("Facility")]
        public string Facility_Id { get; set; }
        public string Day_Of_Week { get; set; }
        public TimeSpan Opening_Time { get; set; }
        public TimeSpan Closing_Time { get; set; }

        public Facility Facility { get; set; }
    }
}
