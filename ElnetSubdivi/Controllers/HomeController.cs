using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ElnetSubdivi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;


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
            ViewData["PageTitle"] = "Profile Settings";
            ViewData["HideBtn"] = true;
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

        public IActionResult viewProfile()
        {
            return View();
        }
        public IActionResult AdminDash()
        {
            var data = new List<dynamic>
                {
                    new { Title = "User Count", Icon = "user.svg", Count = "1,234", Label1 = "Homeowners", Value1 = "876", Label2 = "Staffs", Value2 = "358" },
                    new { Title = "Events", Icon = "calen.svg", Count = "45", Label1 = "Pending", Value1 = "30", Label2 = "Completed", Value2 = "15" },
                    new { Title = "Forum Post", Icon = "msg.svg", Count = "40", Label1 = "+14% from last month", Value1 = "", Label2 = "", Value2 = "" },
                    new { Title = "Requests", Icon = "tool.svg", Count = "10", Label1 = "Ongoing", Value1 = "30", Label2 = "Completed", Value2 = "15" },
                    new { Title = "Feedback", Icon = "str.svg", Count = "11", Label1 = "Avg Rating", Value1 = "4.8", Label2 = "", Value2 = "" },
                    new { Title = "Financial", Icon = "pe.svg", Count = "", Label1 = "Paid", Value1 = "₱25,000", Label2 = "Overdue", Value2 = "₱5,000" }
                };

            return View(data);
        }
        public IActionResult UserManagement()
        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true;
            return View();
        }

        public IActionResult Billing()
        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true;
            return View();
        }

        public IActionResult serviceRequest()
        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true;
            return View();
        }

        public IActionResult Reports()
        {
            ViewData["Title"] = "Reports";
            return View();
        }
        public IActionResult ContactDirectory()
        {
            return View();
        }
        public IActionResult FacilityManagement()
        {
            ViewData["HideSearch"] = true;

            var facilities = new List<dynamic>
                   {
                new { Name = "Community Park", Type = "Recreation", Rating = 4.8, Time = "6:00 AM - 10:00 PM", Price = "Free", Available = true, Image = "~/Images/facimg1.png" },
                new { Name = "Function Hall", Type = "Events", Rating = 4.8, Time = "6:00 AM - 10:00 PM", Price = "₱25/hr", Available = true, Image = "~/Images/facimg2.png" },
                new { Name = "Tennis Court", Type = "Sports", Rating = 4.8, Time = "6:00 AM - 10:00 PM", Price = "$15/hr", Available = false, Image = "~/Images/tennis.png" }
            };


            var reservations = new List<dynamic>
            {
                new { Title = "Total Facilities", Count = 10, Icon = "~/Images/dafac.svg", BorderColor = "border-b-[3px] border-blue-500" },
                new { Title = "Currently Booked", Count = 3, Icon = "~/Images/pendi.svg", BorderColor = "border-b-[3px] border-yellow-400" },
                new { Title = "Maintenance", Count = 4, Icon = "~/Images/canc.svg", BorderColor = "border-b-[3px] border-red-400" },
                new { Title = "Available Facilities", Count = 3, Icon = "~/Images/apr.svg", BorderColor = "border-b-[3px] border-green-400" }
            };

                    ViewBag.Reservations = reservations;

            return View(facilities);
        }
        public IActionResult ReservationsManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var reservations = new List<dynamic>
            {
                new { Title = "Total Bookings", Count = 248, Icon = "booking.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approvals", Count = 3, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Cancelled Reservations", Count = 15, Icon = "canc.svg", BorderColor = "border-red-400 borber-b-2" },
                new { Title = "Approved Reservations", Count = 23, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" }
            };

            return View(reservations);

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Landing", "Home"); // Redirect to the landing page
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "a")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("AdminDash", "Home");
            }
            else if (username == "user" && password == "u")
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
            else if (username == "staff" && password == "s")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "staff")
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
