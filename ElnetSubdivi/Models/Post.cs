using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Numerics;
using System.Reflection;

namespace ElnetSubdivi.Models
{
    public class Post
    {
        [Key]
        public string post_id { get; set; }

        public string user_id { get; set; }

        public DateTime post_date { get; set; }

        public string post_text { get; set; }

        public string post_media { get; set; }

        public int likes { get; set; }

        public int comments { get; set; }

        public Post()
        {
            post_id = string.Empty;
            user_id = string.Empty;
            post_date = DateTime.MinValue;
            post_text = string.Empty;
            post_media = string.Empty;
            likes = 0;
            comments = 0;
        }
    }
}