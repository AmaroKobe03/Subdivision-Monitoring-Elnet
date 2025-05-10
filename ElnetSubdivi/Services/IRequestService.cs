using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IRequestService
    {
        Task<List<ServiceRequest>> GetAllRequest();
        Task<List<ServiceRequest>> GetAllPendingRequest();
        Task<List<ServiceRequest>> GetAllCompleteRequest();
        //Task<List<Post>> GetPostsByUserAsync(string userId);
    }
}
