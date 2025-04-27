namespace ElnetSubdivi.Models
{
    public class ServiceRequestViewModel
    {
        public string Request_Id { get; set; }
        public string User_Id { get; set; }
        public DateTime Request_Creation { get; set; }
        public string Request_Type { get; set; }
        public string Request_Subject { get; set; }
        public DateTime Request_Date { get; set; }
        public string Request_Time { get; set; }
        public string Request_Description { get; set; }
        public string Request_Status { get; set; } = "Pending";
    }

}
