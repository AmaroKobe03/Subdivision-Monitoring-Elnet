using ElnetSubdivi.Models;
using Microsoft.AspNetCore.Http; // Ensure this namespace is included for IFormFile

namespace ElnetSubdivi.Models
{
    public class FacilityViewModel
    {
        // UI Properties
        public string ImagePath { get; set; }
        public string FacilityName { get; set; }
        public string ActionName { get; set; }
        public string ButtonText { get; set; }
        public string DTime { get; set; }
        public string Type { get; set; }
        public bool ShowEditCancelButtons { get; set; }

        // Facility Data Properties
        public string FacilityId { get; set; }
        public string FacilityCategory { get; set; }
        public string FacilityDescription { get; set; }
        public decimal ServiceFeePerHour { get; set; }
        public string FacilityGuidelines { get; set; }
        public string FacilityAminities { get; set; }
        public string FacilityStatus { get; set; } = "Available";


        // For form binding
        public List<string> SelectedDays { get; set; } = new();

        // Operating hours
        public List<FacilityOperatingHours> OperatingHours { get; set; }

        public IFormFile ImageFile { get; set; }

        // Constructor
        public FacilityViewModel()
        {
            ImagePath = string.Empty;
            FacilityName = string.Empty;
            DTime = string.Empty;
            Type = string.Empty;
            ActionName = string.Empty;
            ButtonText = string.Empty;
            OperatingHours = new List<FacilityOperatingHours>(); // Fixed type mismatch
        }

        public class FacilityOperatingHours
        {
            public string Facility_Id { get; set; }
            public string Day_Of_Week { get; set; }
            public TimeSpan? Opening_Time { get; set; }
            public TimeSpan? Closing_Time { get; set; }

            public Facility Facility { get; set; }
        }
    }
}

