using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using System.Threading.Tasks;
using ElnetSubdivi.Services;

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

        // Show edit form for a user
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userService.GetUserDetailsById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // Handle form submission to edit a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Users user)
        {
            if (id != user.user_id) return NotFound();

            if (ModelState.IsValid)
            {
                await _userService.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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


    }
}
