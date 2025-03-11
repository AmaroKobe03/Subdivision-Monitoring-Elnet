using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ElnetSubdivi.Models;
using ElnetSubdivi.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ElnetSubdivi.Services;

namespace ElnetSubdivi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Use UserService to fetch user from the database
            var userAccount = await _userService.GetUserByUsername(username);

            if (userAccount != null && userAccount.password == password) // (Replace with password hashing later)
            {
                string userId = userAccount.user_id;

                // Fetch user details from users table using user_id
                var userDetails = await _userService.GetUserDetailsById(userId);

                if (userDetails != null)
                {
                    // Store user details in Users model
                    Users loggedInUser = new Users
                    {
                        user_id = userDetails.user_id,
                        first_name = userDetails.first_name,
                        last_name = userDetails.last_name,
                        email = userDetails.email,
                        phone_number = userDetails.phone_number,
                        role = userDetails.role
                    };

                    UserAccount loggedInAccount = new UserAccount
                    {
                        user_id = userAccount.user_id,
                        username = userAccount.username,
                        password = userAccount.password
                    };

                    // Create authentication claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, loggedInAccount.username),
                        new Claim(ClaimTypes.Role, loggedInUser.role), // Store role
                        new Claim("UserId", loggedInUser.user_id.ToString()) // Store user_id
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties
                    );

                    return RedirectToAction("UserDash", "Home");
                }
            }

            TempData["Error"] = "Invalid Credentials!";
            return RedirectToAction("Landing");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
