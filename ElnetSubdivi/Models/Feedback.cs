using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ElnetSubdivi.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [StringLength(255)]
        public string UserID { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string Type { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public Feedback()
        {
            FeedbackID = 0; // Default value for FeedbackID
            UserID = string.Empty;
            Rating = 1; // Default value for Rating
            Title = string.Empty;
            Description = string.Empty;
            SubmittedAt = DateTime.Now; // Default value for SubmittedAt
            Type = string.Empty;
            Status = string.Empty;
        }
    }
}
