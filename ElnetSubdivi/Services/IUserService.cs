using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<UserAccount> GetUserByUsername(string username);
        Task<Users> GetUserDetailsById(string userId);
        Task<bool> CreateUser(Users user);
        Task UpdateUser(Users user);
        Task DeleteUser(string userId);
        Task<Users> GetLastUserId(int type_of_user);
        string GetCurrentUserId(ClaimsPrincipal user);
    }
}
