using System.ComponentModel.DataAnnotations;

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
}
