namespace ElnetSubdivi.Models
{
    public class IncidentReportsViewModel
    {
        public List<IncidentReports> IncidentReports { get; set; } = new();
        public List<Users> Users { get; set; }
        public ElnetSubdivi.Models.Users User { get; set; }
    }
}
