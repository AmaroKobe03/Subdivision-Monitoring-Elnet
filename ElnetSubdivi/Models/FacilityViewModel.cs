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
    public int TypeOfStatus { get; set; }

    // Operating hours
    public List<OperatingHourDto>? OperatingHours { get; set; }


    // Constructor
    public FacilityViewModel()
    {
        ImagePath = string.Empty;
        FacilityName = string.Empty;
        DTime = string.Empty;
        Type = string.Empty;
        ActionName = string.Empty;
        ButtonText = string.Empty;
        OperatingHours = new List<OperatingHourDto>();
    }

    public class OperatingHourDto
    {
        public string DayOfWeek { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}

