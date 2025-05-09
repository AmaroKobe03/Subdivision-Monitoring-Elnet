namespace ElnetSubdivi.Models
{
    public class VehicleViewModel
    {
        public List<Vehicle> Vehicle { get; set; } = new();
        public List<Users> Users { get; set; }
        public ElnetSubdivi.Models.Users User { get; set; }
    }
}
