﻿using ElnetSubdivi.Models;
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
                FacilityStatus = f.Facility_Status,
                OperatingHours = f.OperatingHours.Select(h => new FacilityViewModel.FacilityOperatingHours
                {
                    Facility_Id = h.Facility_Id,
                    Day_Of_Week = h.Day_Of_Week,
                    Opening_Time = h.Opening_Time,
                    Closing_Time = h.Closing_Time,
                    Facility = h.Facility
                }).ToList() // Ensure this is a List<FacilityViewModel.FacilityOperatingHours>
            }).ToList();

            return View("FacilityManagement", viewModels); // View name matches your cshtml
        }
    }

}
