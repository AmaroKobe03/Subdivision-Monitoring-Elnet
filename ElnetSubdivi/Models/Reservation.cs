using ElnetSubdivi.ViewModels;
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

        public TimeSpan time_in { get; set; }

        public TimeSpan time_out { get; set; }
        public string reservation_purpose { get; set; }
        public string reservation_status { get; set; }


        public Reservation()
        {
            reservation_id = string.Empty;
            facility_id = string.Empty;
            user_id = string.Empty;
            reservation_date = DateTime.MinValue;
            time_in = TimeSpan.MinValue;
            time_out = TimeSpan.MinValue;
            reservation_purpose = string.Empty;
            reservation_status = "Pending";
        }
    }
}