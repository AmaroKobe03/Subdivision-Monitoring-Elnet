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
            return await _context.Users.ToListAsync();
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
        public async Task CreateUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
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
    }
}
