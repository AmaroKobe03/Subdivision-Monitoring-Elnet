using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElnetSubdivi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

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
            return View();
        }
        public IActionResult MyReservation()
        {
            return View();
        }
        public IActionResult UserManagement()
        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true; // This will hide the search bar and filter
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

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "admin")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("UserDash", "Home");
            }
            else if (username == "user" && password == "user123")
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "user")
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("UserDash", "Home");
            }
            else
            {
                TempData["Error"] = "Invalid Credentials!";
                return RedirectToAction("Landing");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
