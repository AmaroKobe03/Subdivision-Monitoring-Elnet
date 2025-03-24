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
        public IActionResult VehicleManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var manageVehicle = new List<dynamic>
            {
                new { Title = "Total Vehicles", Count = 123, Icon = "vehicle.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approval", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Approved Today", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Declined", Count = 10, Icon = "outv.svg", BorderColor = "border-red-400 borber-b-2" }

            };

            return View(manageVehicle);
        }
        public IActionResult VisitorsPassManagement() 
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var reservations = new List<dynamic>
            {
                new { Title = "Total Visitor Requests", Count = 123, Icon = "totalv.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approval", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Active Visitors", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Checked-Out Visitors", Count = 10, Icon = "outv.svg", BorderColor = "border-red-400 borber-b-2" }

            };

            return View(reservations);
        }
        public IActionResult PaymentHistory()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var billing = new List<dynamic>
            {
                new { Title = "Total Amount Due", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Payments", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Overdue Payments", Count = 15, Icon = "overd.svg", BorderColor = "border-orange-400 borber-b-2" },
                new { Title = "Total Amount Paid (2025) ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },

            };

            return View(billing);
        }
        public IActionResult BillingManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var billing = new List<dynamic>
            {
                new { Title = "Total Amount Due", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Payments", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Overdue Payments", Count = 15, Icon = "overd.svg", BorderColor = "border-orange-400 borber-b-2" },
                new { Title = "Total Amount Paid (2025) ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },

            };

            return View(billing);
        }
        public IActionResult ServiceRequestManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var reservations = new List<dynamic>
            {
                new { Title = "Total Requests", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending ", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Ongoing ", Count = 15, Icon = "ongo.svg", BorderColor = "border-orange-400 borber-b-2" },
                new { Title = "Completed ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Canceled Service ", Count = 10, Icon = "canc.svg", BorderColor = "border-red-400 borber-b-2" }

            };

            return View(reservations);
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

        public IActionResult UserVisitors()
        {
            ViewData["HideSearch"] = true;
            return View();

        }

        public IActionResult UserVehicle()
        {
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
        public IActionResult Calendar()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
        public IActionResult Reports()
        {
            ViewData["Title"] = "Reports";
            return View();
        }
        public IActionResult UserFeedback()
        {
            ViewData["HideSearch"] = true;

            var feedback = new List<dynamic>
            {
                new {Icon = "ratings.svg", Title = "Great User Experience", Type = "Feedback", Description = "The Interface is intuitive and easy to navigate. I particularly enjoyedthe smooth transitions and..", Clock = "clock.svg", Duration = "2 Days ago"},
                new {Icon = "ratings.svg", Title = "Great User Exponent", Type = "Feedback", Description = "The Interface is intuitive and easy to navigate. I particularly enjoyedthe smooth transitions and..", Clock = "clock.svg", Duration = "2 Days ago" },

            };

            var complaint = new List<dynamic>
            {
                new {Status = "Pending", Title = "Noise Complaint in Block A", Type = "Complaint", Description = "Continuous loud music from unit 302 during quiet hours...", Clock = "clock.svg", Duration = "2 Days ago"},
                new {Status = "Resolved", Title = "Noise Complaint in Block B", Type = "Complaint", Description = "Continuous loud music from unit 302 during quiet hours...", Clock = "clock.svg", Duration = "2 Days ago" },
            };
                 ViewBag.Complaints = complaint;
            return View(feedback);

        }
        public IActionResult feedback()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var manageReports = new List<dynamic>
            {
                new { Title = "Total Feedback", Count = 123, Icon = "mailfeed.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Total Complaints", Count = 34, Icon = "complaints.svg", BorderColor = "border-red-400 borber-b-2" },
                new { Title = "Total Reports", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Average Satisfaction", Count = 10, Icon = "satisfaction.svg", BorderColor = "border-yellow-400 borber-b-2" }

            };

            return View(manageReports);
        }
        public IActionResult ContactDirectory()
        {
            var emergencies = new List<dynamic>
            {
                new { Title = "Medical Emergency", Number = "911", Description = "24/7 Emergency Response", Icon = "medic.svg", CallIcon = "phone-red.svg", BgColor = "bg-red-50", BorderColor = "border-red-500", TextColor = "text-red-700" },
                new { Title = "Fire Department", Number = "+1 234 567 891", Description = "Fire Emergency Response", Icon = "fire-exting.svg", CallIcon = "phone-orange.svg", BgColor = "bg-orange-50", BorderColor = "border-orange-500", TextColor = "text-orange-700" },
                new { Title = "Police Station", Number = "+1 234 567 890", Description = "24/7 Security Hotline", Icon = "protec.svg", CallIcon = "phone-blue.svg", BgColor = "bg-blue-50", BorderColor = "border-blue-500", TextColor = "text-blue-700" }
            };

            return View(emergencies);
        }
        public JsonResult GetEvents()
        {
            var events = new List<object>
        {
            new { title = "Meeting", start = "2025-03-20" },
            new { title = "Conference", start = "2025-03-25" },
            new { title = "Workshop", start = "2025-03-28" }
        };

            return Json(events);
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

            var operatingHours = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            ViewBag.OperatingHours = operatingHours;

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
