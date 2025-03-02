public class FacilityViewModel
{
    public string ImagePath { get; set; }
    public string FacilityName { get; set; }
    public string ActionName { get; set; }
    public string ButtonText { get; set; }
    public Dictionary<string, string> Icons { get; set; }

    // Constructor to initialize properties
    public FacilityViewModel()
    {
        ImagePath = string.Empty;
        FacilityName = string.Empty;
        ActionName = string.Empty;
        ButtonText = string.Empty;
        Icons = new Dictionary<string, string>();
    }
}
