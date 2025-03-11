using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Net;
using System.Reflection;

namespace ElnetSubdivi.Models
{
    public class UserAccount
    {
        [Key]
        public string user_id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        [ForeignKey("user_id")]
        public virtual Users User { get; set; }

        public UserAccount()
        {
            user_id = string.Empty;
            username = string.Empty;
            password = string.Empty;
        }
    }
}
