using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ElnetSubdivi.Models
{
    public class ServiceRequest
    {
        [Key]
        public string Request_Id { get; set; }

        public string User_Id { get; set; }
        public string Request_Type { get; set; }
        public string Request_Subject { get; set; }
        public DateTime Request_Date { get; set; }
        public string Request_Time { get; set; }
        public string Request_Description { get; set; }
        public DateTime Request_Creation { get; set; }
        public string Request_Status { get; set; }

        public ServiceRequest()
        {
            Request_Id = string.Empty;
            User_Id = string.Empty;
            Request_Type = string.Empty;
            Request_Subject = string.Empty;
            Request_Date = DateTime.MinValue;
            Request_Time = string.Empty;
            Request_Description = string.Empty;
            Request_Creation = DateTime.MinValue;
            Request_Status = string.Empty;
            Request_Status = string.Empty;
        }
    }

    public class SummaryCard
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public string Icon { get; set; }
        public string BorderColor { get; set; }
    }
}
