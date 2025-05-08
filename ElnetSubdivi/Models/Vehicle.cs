using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElnetSubdivi.Models
{
    public class Vehicle
    {
        [Key]
        [Column("plate_number")]
        [StringLength(50)]
        public string plate_number { get; set; }

        [Required]
        [Column("user_id")]
        [StringLength(50)]
        public string user_id { get; set; }

        [Required]
        [Column("vehicle_type")]
        [StringLength(50)]
        public string vehicle_type { get; set; }

        [Required]
        [Column("vehicle_brand")]
        [StringLength(50)]
        public string vehicle_brand { get; set; }

        [Required]
        [Column("vehicle_model")]
        [StringLength(50)]
        public string vehicle_model { get; set; }

        [Required]
        [Column("vehicle_color")]
        [StringLength(50)]
        public string vehicel_color { get; set; }

        [Required]
        [Column("vehicle_status")]
        [StringLength(50)]
        public string vehicle_status { get; set; }

        [Required]
        [Column("vehicle_document")]
        public byte[] vehicle_document { get; set; }

        public Vehicle()
        {
            plate_number = string.Empty;
            user_id = string.Empty;
            vehicle_type = string.Empty;
            vehicle_brand = string.Empty;
            vehicle_model = string.Empty;
            vehicel_color = string.Empty;
            vehicle_status = string.Empty;
            vehicle_document = new byte[0];
        }
    }
}
