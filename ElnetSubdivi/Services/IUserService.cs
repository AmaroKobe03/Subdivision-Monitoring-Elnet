using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<List<Users>> GetAllHomeowners();
        Task<List<Users>> GetAllStaff();
        Task<UserAccount> GetUserByUsername(string username);
        Task<Users> GetUserDetailsById(string userId);
        Task<bool> CreateUser(Users user);
        Task<bool> UpdateUser(Users user);
        Task DeleteUser(string userId);
        Task<Users> GetLastUserId(int type_of_user);
        string GetCurrentUserId(ClaimsPrincipal user);
    }
}
