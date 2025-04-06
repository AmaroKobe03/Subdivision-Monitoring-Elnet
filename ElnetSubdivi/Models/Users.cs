using System.ComponentModel.DataAnnotations; // ✅ Required for [Key]

namespace ElnetSubdivi.Models
{
    public class Users
    {
        [Key] // ✅ This explicitly marks user_id as the primary key
        public string user_id { get; set; }

        public string? first_name { get; set; }
        public string? middle_name { get; set; }
        public string? last_name { get; set; }
        public DateOnly date_of_birth { get; set; }
        public string? gender { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public string? profile_picture { get; set; }
        public int? type_of_user { get; set; }
        public DateTime? created_at { get; set; }

        public Users()
        {
            user_id = string.Empty;
            first_name = string.Empty;
            middle_name = string.Empty;
            last_name = string.Empty;
            date_of_birth = DateOnly.MinValue;
            gender = string.Empty;
            email = string.Empty;
            address = string.Empty;
            phone = string.Empty;
            profile_picture = string.Empty;
            type_of_user = null;
            created_at = DateTime.MinValue;
        }
    }
}
