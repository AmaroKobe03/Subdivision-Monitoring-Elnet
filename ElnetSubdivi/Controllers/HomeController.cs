using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElnetSubdivi.Models;

namespace ElnetSubdivi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Landing()
        {
            ViewBag.HideNav = true;
            return View();
        }
        public IActionResult UserDash()
        {
            return View();
        }
        public IActionResult UserProfile()
        {
            return View();
        }
        public IActionResult FacilityReservation()
        {

            //var model = new FacilityViewModel
            //{
            //    ShowReservation = false // Default to not showing reservation
            //};
            return View();
        }
        //[HttpPost]
        //public IActionResult ShowReservation()
        //{
        //    var model = new FacilityViewModel
        //    {
        //        ShowReservation = true // Show reservation when button is clicked
        //    };
        //    return View("FacilityReservation", model);
        //}
        public IActionResult MyReservation()
        {
            return View();
        }
        public IActionResult Billing()

        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true; // This will hide the search bar and filter
            return View();
        }
        public IActionResult serviceRequest()
        {

            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true; // This will hide the search bar and filter
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
