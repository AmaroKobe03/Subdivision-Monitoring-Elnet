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
            var users = await _userService.GetAllUsers();
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
    }
}
