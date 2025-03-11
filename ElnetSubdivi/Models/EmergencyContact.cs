using Microsoft.AspNetCore.Mvc;

namespace ElnetSubdivi.Models
{
    public class EmergencyContact : Controller
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IconPath { get; set; }
        public string PhoneIconPath { get; set; }
        public string Description { get; set; }
        public string BorderColor { get; set; }
        public string BgColor { get; set; }
        public string TextColor { get; set; }
    }
}
