using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IRequestService
    {
        Task<List<ServiceRequest>> GetAllRequest();
        //Task<List<Post>> GetPostsByUserAsync(string userId);
    }
}
