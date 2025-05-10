using Microsoft.EntityFrameworkCore;
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

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
            try
            {
                var users = await _context.Users
                    .Select(u => new Users
                    {
                        user_id = u.user_id ?? "",
                        first_name = u.first_name ?? "",
                        middle_name = u.middle_name ?? "",
                        last_name = u.last_name ?? "",
                        date_of_birth = u.date_of_birth,
                        gender = u.gender ?? "",
                        phone = u.phone ?? "",
                        email = u.email ?? "",
                        address = u.address ?? "",
                        type_of_user = u.type_of_user ?? 0,
                        profile_picture = u.profile_picture ?? null,
                        created_at = u.created_at ?? DateTime.MinValue
                    })
                    .ToListAsync();

                return users; // Always returns a List<Users> (never null)
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Users>();
            }
        }

        public async Task<List<Users>> GetAllHomeowners()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.type_of_user == 2) // Filter by type_of_user = 2
                    .Select(u => new Users
                    {
                        user_id = u.user_id ?? "",
                        first_name = u.first_name ?? "",
                        middle_name = u.middle_name ?? "",
                        last_name = u.last_name ?? "",
                        date_of_birth = u.date_of_birth,
                        gender = u.gender ?? "",
                        phone = u.phone ?? "",
                        email = u.email ?? "",
                        address = u.address ?? "",
                        type_of_user = u.type_of_user ?? 0,
                        profile_picture = u.profile_picture ?? null,
                        created_at = u.created_at ?? DateTime.MinValue
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Users>();
            }
        }

        public async Task<List<Users>> GetAllStaff()
        {
            try
            {
                var validTypes = new List<int?> { 3, 4, 5 };

                var users = await _context.Users
                    .Where(u => validTypes.Contains(u.type_of_user))
                    .Select(u => new Users
                    {
                        user_id = u.user_id ?? "",
                        first_name = u.first_name ?? "",
                        middle_name = u.middle_name ?? "",
                        last_name = u.last_name ?? "",
                        date_of_birth = u.date_of_birth,
                        gender = u.gender ?? "",
                        phone = u.phone ?? "",
                        email = u.email ?? "",
                        address = u.address ?? "",
                        type_of_user = u.type_of_user ?? 0,
                        profile_picture = u.profile_picture ?? null,
                        created_at = u.created_at ?? DateTime.MinValue
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Users>();
            }
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


        public async Task<string> GetUserType(int type_of_user)
        {
            string prefix = type_of_user switch
            {
                1 => "Admin",
                2 => "Homeowner",
                3 => "Housekeeping",
                4 => "Maintenance",
                5 => "Security",
                _ => "User"
            };
            return prefix;
        }


        public async Task<bool> CreateUser(Users user)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Step 1: Generate user_id based on type_of_user
                string prefix = user.type_of_user switch
                {
                    1 => "ADM",
                    2 => "RES",
                    3 => "HSK",
                    4 => "MTN",
                    5 => "SCT",
                    _ => "USR"
                };

                var lastUser = await _context.Users
                    .Where(u => u.type_of_user == user.type_of_user && u.user_id.StartsWith(prefix))
                    .OrderByDescending(u => u.user_id)
                    .FirstOrDefaultAsync();

                int nextId = 1;
                if (lastUser != null)
                {
                    var parts = lastUser.user_id.Split('-');
                    if (parts.Length == 2 && int.TryParse(parts[1], out var id))
                    {
                        nextId = id + 1;
                    }
                }

                user.user_id = $"{prefix}-{nextId:D4}";
                user.created_at = DateTime.UtcNow; // Optional: make sure created_at is set

                // Step 2: Save user with generated ID
                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                // Step 3: Create corresponding user account
                var userAccount = new UserAccount
                {
                    user_id = user.user_id,
                    username = user.email,
                    password = "123" // Ideally hash this
                };

                _context.User_Accounts.Add(userAccount);
                await _context.SaveChangesAsync();

                // Step 4: Commit transaction
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Users updatedUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == updatedUser.user_id);
            if (existingUser == null) return false;

            // Only update fields that are meant to be changed
            existingUser.first_name = updatedUser.first_name;
            existingUser.middle_name = updatedUser.middle_name;
            existingUser.last_name = updatedUser.last_name;
            existingUser.date_of_birth = updatedUser.date_of_birth;
            existingUser.gender = updatedUser.gender;
            existingUser.email = updatedUser.email;
            existingUser.phone = updatedUser.phone;
            existingUser.address = updatedUser.address;

            await _context.SaveChangesAsync();
            return true;
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
        public async Task<Users?> GetLastUserId(int typeOfUser)
        {
            return await _context.Users
                .Where(u => u.type_of_user == typeOfUser)
                .OrderByDescending(u => u.user_id)
                .FirstOrDefaultAsync();
        }

        public string GetCurrentUserId(ClaimsPrincipal user)
        {
            return user.FindFirst("UserId")?.Value;
        }
    }
}
