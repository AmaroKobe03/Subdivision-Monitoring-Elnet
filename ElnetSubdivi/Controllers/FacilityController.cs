using ElnetSubdivi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElnetSubdivi.Controllers
{
    // Controllers/FacilityController.cs
    public class FacilityController : Controller
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        public async Task<IActionResult> Index()
        {
            var facilities = await _facilityService.GetAllFacilitiesWithHoursAsync();

            var viewModels = facilities.Select(f => new FacilityViewModel
            {
                FacilityId = f.Facility_Id,
                FacilityName = f.Facility_Name,
                FacilityCategory = f.Facility_Category,
                FacilityDescription = f.Facility_Description,
                ImagePath = f.Facility_Img,
                ServiceFeePerHour = f.Service_Fee_Per_Hour,
                FacilityGuidelines = f.Facility_Guidelines,
                FacilityAminities = f.Facility_Aminities,
                TypeOfStatus = f.TypeOf_Status,
                OperatingHours = f.OperatingHours.Select(h => new FacilityViewModel.OperatingHourDto
                {
                    DayOfWeek = h.DayOfWeek,
                    OpeningTime = h.OpeningTime,
                    ClosingTime = h.ClosingTime
                }).ToList()
            }).ToList();

            return View("FacilityManagement", viewModels); // View name matches your cshtml
        }
    }

}
