using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class FacilityOperatingHour
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Facility")]
        public string FacilityId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }

        public Facility Facility { get; set; }
    }
}
