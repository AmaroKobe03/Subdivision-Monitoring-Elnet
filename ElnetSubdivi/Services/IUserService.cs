using System.Collections.Generic;
using System.Threading.Tasks;
using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<UserAccount> GetUserByUsername(string username);
        Task<Users> GetUserDetailsById(int userId);
        Task CreateUser(Users user);
        Task UpdateUser(Users user);
        Task DeleteUser(int userId);
    }
}
