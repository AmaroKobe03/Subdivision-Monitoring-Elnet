using Microsoft.EntityFrameworkCore;
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElnetSubdivi.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all users
        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users
                .Select(u => new Users
                {
                    user_id = u.user_id,
                    first_name = u.first_name ?? "",
                    middle_name = u.middle_name ?? "",
                    last_name = u.last_name ?? "",
                    date_of_birth = u.date_of_birth,
                    gender = u.gender ?? "",
                    phone_number = u.phone_number ?? "",
                    email = u.email ?? "",
                    valid_id = u.valid_id ?? "",
                    address = u.address ?? "",
                    role = u.role ?? "",
                    profile_picture = u.profile_picture ?? "",
                    created_at = u.created_at
                })
                .ToListAsync();
        }


        // Get user from user_accounts by username (for login)
        public async Task<UserAccount> GetUserByUsername(string username)
        {
            return await _context.User_Accounts
                .Where(u => u.username == username)
                .FirstOrDefaultAsync();
        }

        // Get user details from users table by user_id
        public async Task<Users> GetUserDetailsById(string userId)
        {
            return await _context.Users
                .Where(u => u.user_id == userId)
                .FirstOrDefaultAsync();
        }

        // Create a new user
        public async Task<bool> CreateUser(Users user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true; // Return true if the user is added successfully
            }
            catch
            {
                return false; // Return false if an error occurs
            }
        }


        // Update an existing user
        public async Task UpdateUser(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Delete a user by ID
        public async Task DeleteUser(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Users?> GetLastUserId()
        {
            return await _context.Users
                .OrderByDescending(u => EF.Functions.Like(u.user_id, "%[0-9]%") ? u.user_id : "0") // Ensures numeric sorting
                .FirstOrDefaultAsync();
        }

    }
}
