using Microsoft.AspNetCore.Mvc;

namespace FacilityDetails.Models
{
    public class FacilityModel
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsDisabled { get; set; }
    }
}
