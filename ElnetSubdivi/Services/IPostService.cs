using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPost();
        Task<List<Post>> GetPostsByUserAsync(string userId);
    }
}