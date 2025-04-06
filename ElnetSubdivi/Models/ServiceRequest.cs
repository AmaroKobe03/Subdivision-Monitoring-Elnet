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
    }

}
