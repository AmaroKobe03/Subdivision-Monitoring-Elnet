using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace ElnetSubdivi.Models
{
    public class Reservation
    {
        [Key]
        public string reservation_id { get; set; }

        public string facility_id { get; set; } = "FAC-0001";
        public string user_id { get; set; }
        public DateTime reservation_date { get; set; }

        public DateTime time_in { get; set; }

        public DateTime time_out { get; set; }
        public string reservation_purpose { get; set; }


        public Reservation()
        {
            reservation_id = string.Empty;
            facility_id = string.Empty;
            user_id = string.Empty;
            reservation_date = DateTime.MinValue;
            time_in = DateTime.MinValue;
            time_out = DateTime.MinValue;
            reservation_purpose = string.Empty;
        }
    }
}