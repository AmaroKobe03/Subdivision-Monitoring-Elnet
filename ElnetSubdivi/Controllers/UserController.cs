using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using System.Threading.Tasks;
using ElnetSubdivi.Services;
using System.Security.Claims;
using ElnetSubdivi.Views.Shared;

namespace ElnetSubdivi.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // Display all users
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers() ?? new List<Users>(); // Ensures it's not null
            return View(users);
        }

        public async Task<IActionResult> UserManagement()
        {
            var users = await _userService.GetAllUsers() ?? new List<Users>(); // Handle null
            return View(users);
        }


        // Show user details
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserDetailsById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Show form for creating a user
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission to create a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Show form for editing a user
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _userService.GetUserDetailsById(id);
            if (user == null) return NotFound();

            return View(user); // This expects Views/Users/Edit.cshtml
        }

        [HttpPost("Users/Edit/{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] Users user)
        {
            if (id != user.user_id)
                return NotFound();

            var existingUser = await _userService.GetUserDetailsById(id);
            if (existingUser == null)
                return NotFound();

            // Preserve type_of_user and role_id
            user.type_of_user = existingUser.type_of_user;

            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(user);
                return Ok(new { success = true });
            }

            return BadRequest(ModelState);
        }




        // Show confirmation page for deleting a user
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetUserDetailsById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Handle delete confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Register(string first_name, string middle_name, string last_name, string date_of_birth, string gender, string email, string address, string phone, int type_of_user)
        {
            // Get the last user_id and increment by 1
            var lastUser = await _userService.GetLastUserId(type_of_user);
            int newUserId = 1; // Default ID if no users exist
            string userPrefix = "USR"; // Default prefix (you can change based on type_of_user if needed)

            if (lastUser != null && lastUser.user_id.Contains('-'))
            {
                var parts = lastUser.user_id.Split('-'); // "ADM-0001" => ["ADM", "0001"]
                userPrefix = parts[0]; // Take the prefix (ADM, RES, STF)

                if (parts.Length == 2 && int.TryParse(parts[1], out int lastId))
                {
                    newUserId = lastId + 1; // Increment safely
                }
            }

            // Format new user_id properly: e.g., ADM-0002
            string formattedUserId = $"{userPrefix}-{newUserId.ToString("D4")}";

            var user = new Users
            {
                user_id = newUserId.ToString(),
                type_of_user = type_of_user,
                first_name = first_name,
                middle_name = middle_name,
                last_name = last_name,
                date_of_birth = DateOnly.TryParse(date_of_birth, out var dob) ? dob : DateOnly.MinValue,
                gender = gender,
                email = email,
                phone = phone,
                address = address,
                created_at = DateTime.Now
            };

            bool success = await _userService.CreateUser(user);

            return Json(new { success, message = success ? "User and account registered successfully!" : "Registration failed." });
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWithPicture(IFormCollection form, IFormFile profile_picture)
        {
            var userId = form["user_id"];
            var user = await _userService.GetUserDetailsById(userId);
            if (user == null) return NotFound();

            // Update fields
            user.first_name = form["first_name"];
            user.middle_name = form["middle_name"];
            user.last_name = form["last_name"];
            user.date_of_birth = DateOnly.TryParse(form["date_of_birth"], out var dob) ? dob : user.date_of_birth;
            user.gender = form["gender"];
            user.email = form["email"];
            user.phone = form["phone"];
            user.address = form["address"];

            // If picture was uploaded, handle and save it
            if (profile_picture != null && profile_picture.Length > 0)
            {
                using var ms = new MemoryStream();
                await profile_picture.CopyToAsync(ms);
                user.profile_picture = ms.ToArray(); // assuming this is a byte[] in DB
            }

            await _userService.UpdateUser(user);
            return Ok(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetProfilePicture(string id)
        {
            var user = await _userService.GetUserDetailsById(id);
            if (user == null || user.profile_picture == null)
                return File("~/Images/pfp.svg", "image/svg+xml"); // fallback image

            return File(user.profile_picture, "image/jpeg"); // or image/png depending on your storage
        }

        [HttpGet("/User/ProfilePicture/{userId}")]
        public async Task<IActionResult> ProfilePicture(string userId)
        {
            var user = await _userService.GetUserDetailsById(userId);
            if (user?.profile_picture == null || user.profile_picture.Length == 0)
            {
                return PhysicalFile("wwwroot/Images/pfp.svg", "image/svg+xml"); // fallback
            }

            return File(user.profile_picture, "image/jpeg"); // or "image/png" based on your image type
        }


    }
}
