public class FacilityViewModel
{
    public string ImagePath { get; set; }
    public string FacilityName { get; set; }
    public string ActionName { get; set; }
    public string ButtonText { get; set; }
    public string DTime { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public Dictionary<string, string> Icons { get; set; }

    // New property to check if the card will display two buttons
    public bool ShowEditCancelButtons { get; set; }

    // Constructor to initialize properties
    public FacilityViewModel()
    {
        ImagePath = string.Empty;
        FacilityName = string.Empty;
        DTime = string.Empty;
        Type = string.Empty;
        Status = string.Empty;
        ActionName = string.Empty;
        ButtonText = string.Empty;
        Icons = new Dictionary<string, string>();
    }
}
