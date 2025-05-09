using ElnetSubdivi.Services;
using System.Collections.Generic;

namespace ElnetSubdivi.Models
{
    public class FeedbackViewModel
    {
        public List<Feedback> Feedback { get; set; } = new();
        public List<Users> Users { get; set; }
        public ElnetSubdivi.Models.Users User { get; set; }
    }
}
