using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class VisitorList
    {
        [Key]
        public int visitor_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
        public DateTime visit_date { get; set; }
        public TimeSpan visit_time { get; set; }
        public string vehicle_plate { get; set; }
        public string visit_reason { get; set; }
        public string visit_status { get; set; }

        public VisitorList()
        {
            visitor_id = 0; // Default value for visitor_id
            first_name = string.Empty;
            last_name = string.Empty;
            phone_number = string.Empty;
            visit_date = DateTime.MinValue;
            visit_time = TimeSpan.Zero;
            vehicle_plate = string.Empty;
            visit_reason = string.Empty;
            visit_status = "Pending"; // Default status
        }
    }
}
